using Catan.Shared.Data;
using Catan.Shared.Dtos;
using Catan.Unity.Caches;
using Catan.Unity.Helpers;
using Catan.Unity.InternalUIEvents;
using Catan.Unity.Mappers;
using Catan.Unity.Models;
using Catan.Unity.Networking;
using Catan.Unity.Panels;
using Catan.Unity.Phases.Controllers;
using Catan.Unity.Visuals;
using Catan.Unity.Visuals.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.Helpers;
using UnityEngine;

namespace Catan.Unity.Bootstrap
{
    public class GameBootstrap : MonoBehaviour
    {
        public static GameBootstrap Instance { get; private set; }

        [SerializeField] private BoardManager _boardManager;
        [SerializeField] private ManagerUI _uiManager;

        [SerializeField] private VisualsBoard _visualsBoard;

        [SerializeField] private HandlerCameraClicks _clickHandler;

        private EventBus _bus;
        private HandlerEvents _eventsHandler;
        private EventsTranslator _eventsTranslator;
        private DomainEventDispatcher _dispatcher;

        private GameClient _client;

        private AdapterGameFlow _gameFlow;
        private AdapterPhaseTransition _phaseTransition;
        public Dictionary<EnumResourceType, Color> PortColorLookup { get; private set; }

        public GameCache GameCache;
        public ConnectionCache ConnectionCache;

        private GameSocket _socket;


        private async void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            PortColorLookup = _boardManager.ResourceList.ToDictionary(r => r.Type, r => r.Color);
        }

        async void Start()
        {
            CreateInfrastructure();

            var initialState = await JoinGame();

            InitializeCache(initialState);

            _gameFlow = new AdapterGameFlow(_uiManager, _bus, _phaseTransition, GameCache);
            _dispatcher = new DomainEventDispatcher(GameCache);
            _eventsHandler = new HandlerEvents(_eventsTranslator, _bus, _client, ConnectionCache.GameId.Value, _gameFlow); // gameid will be removed and this will be moved to create infrastructure
            _socket = new GameSocket();

            await _socket.Connect(ConnectionCache.GameId.Value, ConnectionCache.PlayerToken.Value, _dispatcher);

            var controllerResourceCards = InitializeRendering(initialState);

            InitializeInfrastructure(controllerResourceCards);

            ApplyInitialState();
        }

        private async Task<GameStatePerPlayerDto> JoinGame()
        {
            var joinGameResponse = await _client.JoinGame();

            ConnectionCache = new ConnectionCache(joinGameResponse.PlayerToken, joinGameResponse.GameId);

            return joinGameResponse.Payload.ToObject<GameStatePerPlayerDto>();
        }

        private void ApplyInitialState()
        {
            _bus.Publish(new RobberMovedUIEvent(GameCache.Board.BlockedHexId));
            _bus.Publish(new GameFlowReceivedUIEvent(GameCache.GameFlow));
            _bus.Publish(new PlayerStateReceivedUIEvent(GameCache.MyPlayer));
            _bus.Publish(new OtherPlayersReceivedUIEvent(GameCache.OtherPlayers)); // those events need to be made while reworking domainevents
        }

        private ControllerResourceCards InitializeRendering(GameStatePerPlayerDto initialState)
        {
            InitializeBuilderMap(GameCache.Board);

            var controllerResourceCards = InitializeVisualControllers();

            return controllerResourceCards;
        }

        private void InitializeInfrastructure(ControllerResourceCards controllerResourceCards)
        {
            _clickHandler.Initialize(_bus);
            _uiManager.Initialize(_bus, controllerResourceCards, _boardManager);
            _gameFlow.Initialize(_eventsHandler);
        }

        private void InitializeBuilderMap(BoardModel boardModel)
        {
            var builderMap = new BuilderMap
            {
                HexTilePrefab = _boardManager.HexTilePrefab,
                HexNumberPrefab = _boardManager.HexNumberPrefab,
                CubeRobberPrefab = _boardManager.CubeRobberPrefab,
                CubePortPrefab = _boardManager.CubePortPrefab,
                Board = _boardManager.Board,
                FieldMaterialsList = _boardManager.FieldMaterialsList,
                IdleGridMaterial = _boardManager.IdleGridMaterial,
                WaterMaterial = _boardManager.WaterMaterial,
                Size = 1f
            };

            builderMap.BuildMap(boardModel);
            _visualsBoard.Initialize(builderMap, _boardManager.IdleGridMaterial);
        }

        private void CreateInfrastructure()
        {
            _bus = new EventBus();
            _client = new GameClient();
            _phaseTransition = new AdapterPhaseTransition();
            _gameFlow = new AdapterGameFlow(_uiManager, _bus, _phaseTransition, GameCache);
            _eventsTranslator = new EventsTranslator();
        }

        private ControllerResourceCards InitializeVisualControllers()
        {
            var controllerResourceCards = new ControllerResourceCards(_bus);
            new ControllerLogMessagesUI(_bus, _uiManager.LogsPanel);
            new ControllerPlayerUI(_eventsHandler, _uiManager.PlayerUIPanel, _bus);
            new ControllerPlacingBuildings(_bus, _visualsBoard, _boardManager.Board, _boardManager.CubeVillagePrefab, _boardManager.CubeRoadPrefab, _boardManager.CubeTownPrefab);
            new ControllerPlacingRobber(_bus, _visualsBoard, _boardManager);
            new ControllerBoardVisuals(_bus, _visualsBoard);
            new ControllerTurnVisuals(_bus, _uiManager.MainUIPanel);

            return controllerResourceCards;
        }

        private void InitializeCache(GameStatePerPlayerDto joinState)
        {
            var gameFlow = joinState.GameFlow;
            GameCache = new GameCache(BoardMappers.MapBoardStateToModel(joinState), PlayerMappers.MapPlayerDtoToModel(joinState), PlayerMappers.MapOtherPlayersDtoToModel(joinState.OtherPlayers),
                new GameFlowModel(gameFlow.TurnNumber, gameFlow.RolledNumber, gameFlow.CurrentPlayerId, gameFlow.KnightChampionId, gameFlow.RoadChampionId, gameFlow.CurrentPhase, gameFlow.Bank, 
                gameFlow.PlayersToMove));
        }
    }
}