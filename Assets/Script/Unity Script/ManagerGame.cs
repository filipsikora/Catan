#nullable enable
using Catan.Communication;
using Catan.Catan;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


namespace Catan
{

    public class ManagerGame : MonoBehaviour
    {
        public static ManagerGame Instance { get; private set; }
        public GameState? Game { get; set; }
        public Transform Board;
        private BuilderMap? Builder;
        public VisualsBoard BoardVisuals;
        public ManagerUI UIManager;
        public HandlerPhases? PhaseHandler { get; private set; }
        public EventBus EventBus { get; private set; }
        public ControllerResourceCardsUI HandlerResourceCardsUI { get; private set; }


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
        public List<ResourceDataRegistry> ResourceList;

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
            HandlerResourceCardsUI = new ControllerResourceCardsUI(EventBus);
        }

        void Start()
        {
            PhaseHandler = new HandlerPhases();
            PhaseHandler.TransitionTo(new PlayerSetup());
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

            BuildMap();
        }

        void Update()
        {

        }

        /*
        przesuwanie kart/tasowanie

        wybor imienia i koloru

        zaznaczane vertices przechodza nextturn

        rozmiar kart na panelach

        refaktor unity

        exceptions

        dodatkowy złodziej

        signals -> logs

        check reosurces max after roll + extract to a method - resourcecost.addcards/addsingletype

        signals remove visuals

        data separation

        auto register auto binder auto subscribe
        */


    }
}
