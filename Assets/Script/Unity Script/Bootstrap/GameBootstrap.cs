using BGS.Shared.Dtos;
using Catan.Shared.Data;
using Catan.Shared.Dtos;
using Catan.Unity.Caches;
using Catan.Unity.Helpers;
using Catan.Unity.InternalUIEvents;
using Catan.Unity.Mappers;
using Catan.Unity.Networking;
using Catan.Unity.Panels;
using Catan.Unity.Phases.Controllers;
using Catan.Unity.Visuals;
using Catan.Unity.Visuals.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private GameClient _client;

        private AdapterGameFlow _gameFlow;
        private AdapterPhaseTransition _phaseTransition;
        public Dictionary<EnumResourceType, Color> PortColorLookup { get; private set; }

        public GameCache GameCache;
        public ConnectionCache ConnectionCache;


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
            _bus = new EventBus();
            _client = new GameClient();

            Guid gameId;
            JoinGameResponseDto joinGameResponse;

            try
            {
                joinGameResponse = await _client.JoinGame();
            }

            catch (Exception ex)
            {
                Debug.Log($"Error: {ex.Message}");
                return;
            }

            _phaseTransition = new AdapterPhaseTransition();
            _gameFlow = new AdapterGameFlow(_uiManager, _bus, _phaseTransition);

            _eventsTranslator = new EventsTranslator();

            _eventsHandler = new HandlerEvents(_eventsTranslator, _bus, _client, joinGameResponse.GameId, _gameFlow);

            var joinPayload = joinGameResponse.Payload.ToObject<GameStatePerPlayerDto>();

            CreateCaches(joinPayload);

            var desertHexId = InitializeBuilderMap(board);
            var controllerResourceCards = InitializeVisualControllers(gameId);

            ApplyInitialState(desertHexId, firstPlayerId);

            _clickHandler.Initialize(_bus);
            _uiManager.Initialize(_bus, controllerResourceCards, _boardManager);
            _gameFlow.Initialize(_eventsHandler);
        }

        private void ApplyInitialState(int desertHexId, int firstPlayerId) // this will need to be fixed later on with the controllers fix
        {
            _bus.Publish(new RobberMovedUIEvent(desertHexId));
            _bus.Publish(new TurnNumberChangedUIEvent(1));
            _bus.Publish(new PlayerStateChangedUIEvent(firstPlayerId));
        }

        private int InitializeBuilderMap(BoardDto boardDto)
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

            builderMap.BuildMap(boardDto);
            _visualsBoard.Initialize(builderMap, _boardManager.IdleGridMaterial);


            return boardDto.BlockedHexId;
        }

        private ControllerResourceCards InitializeVisualControllers(Guid gameId)
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

        private void CreateCaches(GameStatePerPlayerDto joinState)
        {
            var gameFlow = joinState.GameFlow;
            ConnectionCache = new ConnectionCache(joinState.PlayerToken, joinState.GameId);
            GameCache = new GameCache(BoardMappers.MapBoardStateToModel(joinState), PlayerMappers.MapPlayerDtoToModel(joinState), PlayerMappers.MapOtherPlayersDtoToModel(joinState.OtherPlayers), 
                gameFlow.TurnNumber, gameFlow.RolledNumber, gameFlow.CurrentPlayerId, gameFlow.KnightChampionId, gameFlow.RoadChampionId, gameFlow.CurrentPhase);
        }
    }
}