#nullable enable
using Catan.Application.Controllers;
using Catan.Core;
using Catan.Core.Engine;
using Catan.Core.Queries.InMemory;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Events;
using Catan.Shared.Data;
using Catan.Unity.Communication.InternalUIEvents;
using Catan.Unity.Data;
using Catan.Unity.Helpers;
using Catan.Unity.Panels;
using Catan.Unity.Phases.Adapters;
using Catan.Unity.Phases.Controllers;
using Catan.Unity.Visuals;
using Catan.Unity.Visuals.Controllers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Catan.Unity
{
    public class ManagerGame : MonoBehaviour
    {
        public static ManagerGame Instance { get; private set; }

        public GameSession Session { get; set; }
        public PhaseTransitionController PhaseTransition { get; set; }
        public CommandReceiver CommandRouter { get; set; }
        public Facade Facade { get; set; }

        public Transform Board;
        private BuilderMap? Builder;
        public VisualsBoard BoardVisuals;
        public ManagerUI UIManager;

        public EventBus EventBus { get; private set; }

        public AdapterPhaseTransition? AdapterPhaseTransition;
        public AdapterGameFlow AdapterGameFlow;

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

        public HandlerCameraClicks ClickHandler;

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
            AdapterPhaseTransition = new AdapterPhaseTransition();
            ClickHandler.Initialize(EventBus);

            EventBus.Subscribe<StartGameRequestedEvent>(OnStartGameRequested);
        }

        void Start()
        {
            var setup = new AdapterPlayerSetup(UIManager, EventBus);
            setup.OnEnter();
        }

        private void OnStartGameRequested(StartGameRequestedEvent signal)
        {
            StartGame(signal.PlayerCount);
        }

        public void StartGame(int playerCount)
        {
            var game = new GameState(new HexMap());
            game.InitializeNewGame(playerCount, Size);

            Session = new GameSession(game);

            var boardQuery = new InMemoryBoardQueryServices(Session);
            var devCardsQuery = new InMemoryDevCardQueryService(Session);
            var playersQuery = new InMemoryPlayersQueryServices(Session);
            var resourcesQuery = new InMemoryResourcesQueryService(Session);
            var tradeQuery = new InMemoryTradeQueryServices(Session);
            var turnsQuery = new InMemoryTurnsQueryService(Session);

            Facade = new Facade(Session, boardQuery, devCardsQuery, playersQuery, resourcesQuery, tradeQuery, turnsQuery);
            PhaseTransition = new PhaseTransitionController(Facade, EventBus);
            AdapterGameFlow = new AdapterGameFlow(UIManager, EventBus, Facade, AdapterPhaseTransition, BoardVisuals);
            CommandRouter = new CommandReceiver(PhaseTransition, EventBus);

            var controllerResourceCards = InitializeHelpers(Facade);
            InitializeBuilderMap(Facade);
            UIManager.Initialize(EventBus, controllerResourceCards);

            PhaseTransition.ChangePhase(EnumGamePhases.FirstRoundsBuilding);
        }

        public void InitializeBuilderMap(Facade facade)
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

            var boardData = facade.GetBoardData();
            Builder.BuildMap(boardData, EventBus);

            BoardVisuals.Initialize(Builder, IdleGridMaterial);

            var desertHexId = facade.GetDesertHexId();
            EventBus.Publish(new RobberMovedUIEvent(desertHexId));
        }

        public ControllerResourceCards InitializeHelpers(Facade facade)
        {
            var controllerResourceCards = new ControllerResourceCards(EventBus);
            new ControllerLogMessagesUI(EventBus, UIManager.LogsPanel);
            new ControllerPlayerUI(facade, UIManager.PlayerUIPanel, EventBus);
            new ControllerPlacingBuildings(EventBus, BoardVisuals);
            new ControllerPlacingRobber(EventBus, BoardVisuals);
            new ControllerBoardVisuals(EventBus, BoardVisuals);

            return controllerResourceCards;
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

        adapter check

        spanshot in core, application queries through facade

        selectvictim failure check

        cant play year of plenty if bank has <2 cards

        currentplayer fix + conditions revision

        unity: fix dependencies in controllers and adapters

        delete wrapper from eventbus?
        */
    }
}
