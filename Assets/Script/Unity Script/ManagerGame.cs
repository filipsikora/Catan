#nullable enable
using Catan.Shared.Commands;
using Catan.Shared.Data;
using Catan.Unity.InternalUIEvents;
using Catan.Unity.Data;
using Catan.Unity.Helpers;
using Catan.Unity.Panels;
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
        public Facade Facade { get; set; }

        public Transform Board;
        private BuilderMap? Builder;
        public VisualsBoard BoardVisuals;
        public ManagerUI UIManager;

        public EventBus EventBus { get; private set; }
        public AdapterPhaseTransition? AdapterPhaseTransition;
        public AdapterGameFlow AdapterGameFlow;
        public HandlerCameraClicks ClickHandler;
        public HandlerEvents EventsHandler;
        public EventsTranslator EventsTranslator;

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
        public Dictionary<EnumResourceType, Color> PortColorLookup { get; private set; }

        public GameApplication GameApplication;

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
        }

 //       void Start() startgame(2);


        public void StartGame(int playerCount)
        {
            var random = new UnityRandomProvider();
            var game = new GameState(random, new HexMap(random));
            game.InitializeNewGame(playerCount, Size);

            Session = new GameSession(game);

            var boardQuery = new InMemoryBoardQueryServices(Session);
            var devCardsQuery = new InMemoryDevCardQueryService(Session);
            var playersQuery = new InMemoryPlayersQueryServices(Session);
            var resourcesQuery = new InMemoryResourcesQueryService(Session);
            var tradeQuery = new InMemoryTradeQueryServices(Session);
            var turnsQuery = new InMemoryTurnsQueryService(Session);

            Facade = new Facade(Session, boardQuery, devCardsQuery, playersQuery, resourcesQuery, tradeQuery, turnsQuery);
            GameApplication = new GameApplication(Facade);
            AdapterGameFlow = new AdapterGameFlow(UIManager, EventBus, Facade, AdapterPhaseTransition);
            EventsTranslator = new EventsTranslator();
            EventsHandler = new HandlerEvents(GameApplication, AdapterGameFlow, EventsTranslator, EventBus);

            AdapterGameFlow.Initialize(EventsHandler);

            var controllerResourceCards = InitializeHelpers(Facade);
            InitializeBuilderMap(Facade);
            UIManager.Initialize(EventBus, controllerResourceCards);

            EventsHandler.Execute(new StartGameCommand());
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

        public ControllerResourceCards InitializeHelpers()
        {
            var controllerResourceCards = new ControllerResourceCards(EventBus);
            new ControllerLogMessagesUI(EventBus, UIManager.LogsPanel);
            new ControllerPlayerUI(facade, UIManager.PlayerUIPanel, EventBus);
            new ControllerPlacingBuildings(EventBus, BoardVisuals, Board, CubeVillagePrefab, CubeRoadPrefab, CubeTownPrefab);
            new ControllerPlacingRobber(EventBus, BoardVisuals);
            new ControllerBoardVisuals(EventBus, BoardVisuals);
            new ControllerTurnVisuals(EventBus, UIManager.MainUIPanel);

            return controllerResourceCards;
        }

        void Update()
        {

        }

        /*
        przesuwanie kart/tasowanie

        5. auto register auto binder auto subscribe

        nullable cleanup

        slow with many dev cards

        2. normal round onexit resetselection check

        7. branch backend

        9. end game check

        move rejection/logs to mapper with switch based on result in adapter

        iscurrentplayercheck

        remove data from shared, add queries

        location burdel

        selectvictim failure check

        currentplayer fix + conditions revision

        unity: fix dependencies in controllers and adapters

        resetselection return send in gameresult

        visualsUI split

                determine random and extract it from game

        */
    }
}
