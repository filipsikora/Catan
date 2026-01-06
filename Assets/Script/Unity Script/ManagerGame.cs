#nullable enable
using Catan.Shared.Communication;
using Catan.Shared.Data;
using Catan.Application.Queries.DevCards;
using Catan.Application.Queries.Resources;
using Catan.Core.Engine;
using Catan.Core.Phases.Controllers;
using Catan.Unity.Phases.Controllers;
using Catan.Unity.Data;
using Catan.Unity.Visuals;
using Catan.Unity.Visuals.Controllers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Catan.Core.Routing;
using Catan.Shared.Communication.Events;
using Catan.Unity.Phases.Adapters;
using Catan.Unity.Panels;

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
        public ControllerResourceCardsUI ControllerResourceCardsUI { get; private set; }
        public ControllerLogMessagesUI ControllerLogMessagesUI { get; private set; }

        public IDevCardsQueryService DevCardsQueryService { get; private set; }
        public IResourcesQueryService ResourcesQueryService { get; private set; }


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

            ControllerResourceCardsUI = new ControllerResourceCardsUI(EventBus);
            ControllerLogMessagesUI = new ControllerLogMessagesUI(EventBus, UIManager.LogsPanel);

            EventBus.Subscribe<StartGameRequestedEvent>(OnStartGameRequested);
        }

        void Start()
        {
            AdapterPhaseTransition.TransitionTo(new AdapterPlayerSetup());
        }

        private void OnStartGameRequested(StartGameRequestedEvent signal)
        {
            InitializeGame(signal.PlayerCount);

            EventBus.Publish(new GameInitializedEvent());
        }

        public void BuildMap()
        {
            Builder = new BuilderMap
            {
                HexTilePrefab = HexTilePrefab,
                Board = Board,
                FieldMaterialsList = FieldMaterialsList,
                IdleGridMaterial = IdleGridMaterial,
                Size = Size,
                Game = Game,
                HexNumberPrefab = HexNumberPrefab,
                CubeRobberPrefab = CubeRobberPrefab,
                CubePortPrefab = CubePortPrefab,
                PortColorLookup = PortColorLookup,
                WaterMaterial = WaterMaterial
            };

            Builder.BuildMap(Game.Map);

            BoardVisuals.Initialize(Builder, IdleGridMaterial, Game);
            BoardVisuals.PlaceRobberObject();

            Game.PrepareDevelopmentDeck();
        }

        public void InitializeGame(int playerNumber)
        {
            Game = new GameState(new HexMap());
            Game.ReadyPlayer(playerNumber);
            Game.ReadyBoard();

            LogicGameFlow = new LogicGameFlow(Game, LogicPhaseTransition, EventBus);

            DevCardsQueryService = new InMemoryDevCardQueryService(Game);
            ResourcesQueryService = new InMemoryResourcesQueryService(Game);
            

            BuildMap();
        }

        void Update()
        {

        }

        /*
        przesuwanie kart/tasowanie

        wybor imienia i koloru

        rozmiar kart na panelach

        refaktor unity

        dodatkowy złodziej

        5. auto register auto binder auto subscribe

        nullable cleanup

        slow with many dev cards

        2. normal round onexit resetselection check

        3. make safeguards into core not ui check

        4. board controller

        6. merge unity

        7. branch backend

        8. split gamestate, add dtos and snapshots

        dev cards internal event

        9. end game check

        log for current player, aggregate

        remove publish from visualdevcard

        expand results ok/fail

        remove eventbus from core
        */
    }
}
