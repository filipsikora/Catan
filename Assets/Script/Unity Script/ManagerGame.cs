#nullable enable
using Catan.Application.Queries.Board;
using Catan.Application.Queries.DevCards;
using Catan.Application.Queries.Players;
using Catan.Application.Queries.Resources;
using Catan.Application.Queries.Turns;
using Catan.Core.Engine;
using Catan.Core.Phases.Controllers;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Events;
using Catan.Shared.Data;
using Catan.Unity.Communication.InternalUIEvents;
using Catan.Unity.Data;
using Catan.Unity.Panels;
using Catan.Unity.Phases.Adapters;
using Catan.Unity.Phases.Controllers;
using Catan.Unity.Visuals;
using Catan.Unity.Visuals.Controllers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vatan.Unity.Visuals.Controllers;

namespace Catan.Unity
{

    public class ManagerGame : MonoBehaviour
    {
        public static ManagerGame Instance { get; private set; }

        public GameState? Game { get; set; }
        public LogicPhaseTransition LogicPhaseTransition;
        public LogicGameFlow LogicGameFlow; 
        public CommandRouter CommandRouter { get; set; }

        public Transform Board;
        private BuilderMap? Builder;
        public VisualsBoard BoardVisuals;
        public ManagerUI UIManager;

        public EventBus EventBus { get; private set; }

        public AdapterPhaseTransition? AdapterPhaseTransition;
        public AdapterGameFlow AdapterGameFlow;
        public ControllerResourceCards ControllerResourceCardsUI { get; private set; }
        public ControllerLogMessagesUI ControllerLogMessagesUI { get; private set; }
        public ControllerPlayerUI ControllerPlayerUI { get; private set; }
        public ControllerPlacingBuildings ControllerPlacingBuildings { get; private set; }
        public ControllerPlacingRobber ControllerPlacingRobber { get; private set; }
        public ControllerBoardVisuals ControllerBoardVisuals { get; private set; }

        public IDevCardsQueryService DevCardsQueryService { get; private set; }
        public IResourcesQueryService ResourcesQueryService { get; private set; }
        public IPlayersQueryService PlayersQueryService { get; private set; }
        public ITurnsQueryService TurnsQueryService { get; private set; }
        public IBoardQueryService BoardsQueryService { get; private set; }
        public ITradeQueryService TradeQueryService { get; private set; }

        public float Size = 1f;
        public Material WaterMaterial;
        public Material IdleGridMaterial;

        public GameObject? CubeVillagePrefab;
        public GameObject? CubeRoadPrefab;
        public GameObject? CubeTownPrefab;
        public GameObject? CubeRobberPrefab;
        public GameObject HexNumberPrefab;
        public GameObject HexTilePrefab;
        public GameObject? CubePortPrefab;

        public List<FieldTypeMaterial> FieldMaterialsList;
        public List<RegistryDataResource> ResourceList;
        public Dictionary<EnumResourceTypes, Color> PortColorLookup { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            PortColorLookup = ResourceList.ToDictionary(r => r.Type, r => r.Color);

            EventBus = new EventBus();

            LogicPhaseTransition = new LogicPhaseTransition(EventBus);
            CommandRouter = new CommandRouter(EventBus, LogicPhaseTransition);

            AdapterPhaseTransition = new AdapterPhaseTransition();
            AdapterGameFlow = new AdapterGameFlow(EventBus, AdapterPhaseTransition);

            EventBus.Subscribe<StartGameRequestedEvent>(OnStartGameRequested);
        }

        void Start()
        {
            AdapterPhaseTransition.TransitionTo(new AdapterPlayerSetup());
        }

        private void OnStartGameRequested(StartGameRequestedEvent signal)
        {
            StartGame(signal.PlayerCount);

            EventBus.Publish(new GameInitializedEvent());
        }

        public void StartGame(int playerCount)
        {
            Game = new GameState(new HexMap());
            Game.InitializeNewGame(playerCount, Size);

            InitializeHelpers();
            InitializeBuilderMap();
        }

        public void InitializeBuilderMap()
        {
            Builder = new BuilderMap
            {
                HexTilePrefab = HexTilePrefab,
                Board = Board,
                FieldMaterialsList = FieldMaterialsList,
                IdleGridMaterial = IdleGridMaterial,
                Size = Size,
                HexNumberPrefab = HexNumberPrefab,
                CubeRobberPrefab = CubeRobberPrefab,
                CubePortPrefab = CubePortPrefab,
                WaterMaterial = WaterMaterial
            };

            var boardData = BoardsQueryService.GetBoardData();
            Builder.BuildMap(boardData);

            BoardVisuals.Initialize(Builder, IdleGridMaterial);

            var desertHex = Game.Map.HexList.Find(h => h.FieldType == EnumFieldTypes.Desert);
            EventBus.Publish(new RobberMovedUIEvent(desertHex.Id));
        }

        public void InitializeHelpers()
        {
            LogicGameFlow = new LogicGameFlow(Game, LogicPhaseTransition, EventBus);

            DevCardsQueryService = new InMemoryDevCardQueryService(Game);
            ResourcesQueryService = new InMemoryResourcesQueryService(Game);
            PlayersQueryService = new InMemoryPlayersQueryServices(Game);
            TurnsQueryService = new InMemoryTurnsQueryService(Game);
            BoardsQueryService = new InMemoryBoardQueryServices(Game);
            TradeQueryService = new InMemoryTradeQueryServices(Game);

            ControllerResourceCardsUI = new ControllerResourceCards(EventBus);
            ControllerLogMessagesUI = new ControllerLogMessagesUI(EventBus, UIManager.LogsPanel);
            ControllerPlayerUI = new ControllerPlayerUI(PlayersQueryService, UIManager.PlayerUIPanel, EventBus);
            ControllerPlacingBuildings = new ControllerPlacingBuildings(EventBus, BoardVisuals);
            ControllerPlacingRobber = new ControllerPlacingRobber(EventBus, BoardVisuals);
            ControllerBoardVisuals = new ControllerBoardVisuals(EventBus, BoardVisuals);
        }

        void Update()
        {

        }

        /*
        przesuwanie kart/tasowanie

        wybor imienia i koloru

        5. auto register auto binder auto subscribe

        nullable cleanup

        slow with many dev cards

        2. normal round onexit resetselection check

        3. make safeguards into core not ui check

        6. merge unity

        7. branch backend

        8. split gamestate, add dtos and snapshots

        dev cards internal event

        9. end game check

        log for current player, aggregate

        remove publish from visualdevcard

        expand results ok/fail

        remove eventbus from core

        split rollandserve

        generic vs type - buildingregistry + Type

        move rejection/logs to mapper with switch based on result in adapter

        make game private and initialized

        add none to enums

        remove models from logic rounds

        iscurrentplayercheck

        remove data from shared, add queries

        controllers discarding

        controller highlighting

        remove models from data

        dev cards lists in game -> dev cards id lists in game

        determine random and extract it from game

        location burdel

        phasecontext gamestate
        */
    }
}
