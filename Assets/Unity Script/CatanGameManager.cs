#nullable enable
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

    public class CatanGameManager : MonoBehaviour
    {

        public static CatanGameManager Instance { get; private set; }
        public GameState? Game { get; set; }
        public Transform Board;
        private MapBuilder? Builder;
        public VisualsBoard BoardVisuals;
        public PhaseHandler? PhaseHandler { get; private set; }
        public DevelopmentCardHandler? DevelopmentCardHandler { get; set; }

        public float Size = 1f;

        public Material IdleGridMaterial;

        public MainUI MainUIPanel;
        public PlayerUI PlayerUIPanel;
        public CardSelectorTheftUI CardSelectorTheftPanel;
        public VictimSelectionUI VictimSelectorPanel;
        public GameObject PlayerSelectorPanel;
        public CardStealing CardStealingPanel;
        public CardSelectorRobberUI CardSelectorRobberPanel;
        public TradeOfferUI TradeOfferPanel;
        public TradeRequestUI TradeRequestPanel;
        public BankTradeUI BankTradePanel;
        public DevelopmentCardsUI DevelopmentCardsPanel;

        public GameObject? CubeVillagePrefab;
        public GameObject? CubeRoadPrefab;
        public GameObject? CubeTownPrefab;
        public GameObject? CubeRobberPrefab;
        public GameObject HexNumberPrefab;
        public GameObject HexTilePrefab;
        public GameObject? CubePortPrefab;

        public List<FieldTypeMaterial> FieldMaterialsList;
        public List<ResourceDataRegistry> ResourceList;

        public Material WaterMaterial;

        public bool RobberPlaced = false;

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
        }


        void Start()
        {
            PhaseHandler = new PhaseHandler();
            PhaseHandler.TransitionTo(new PlayerSetup());
        }


        public void BuildMap()
        {
            Builder = new MapBuilder
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

        public void StartGame()
        {
            MainUIPanel.Bind(EnumMainUIButtons.RollDice, OnRollDiceClicked);
            MainUIPanel.Bind(EnumMainUIButtons.NextTurn, OnNextTurnClicked);
            MainUIPanel.Bind(EnumMainUIButtons.BuildFreeVillage, OnBuildFreeVillageClicked);
            MainUIPanel.Bind(EnumMainUIButtons.BuildFreeRoad, OnBuildFreeRoadClicked);
            MainUIPanel.Bind(EnumMainUIButtons.BuildVillage, OnBuildVillageClicked);
            MainUIPanel.Bind(EnumMainUIButtons.BuildRoad, OnBuildRoadClicked);
            MainUIPanel.Bind(EnumMainUIButtons.UpgradeVillage, OnUpgradeVillageClicked);
            MainUIPanel.Bind(EnumMainUIButtons.OfferTrade, () => OnTradeOffered(new ResourceCostOrStock()));
            MainUIPanel.Bind(EnumMainUIButtons.BankTrade, () => OnBankTradeClicked());
            MainUIPanel.Bind(EnumMainUIButtons.DevelopmentCards, () => OnDevelopmentCardsClicked());
            MainUIPanel.Bind(EnumMainUIButtons.BuyDevelopmentCard, () => OnDevelopmentCardBought());

            MainUIPanel.gameObject.SetActive(true);
            PlayerUIPanel.gameObject.SetActive(true);
        }

        public void OnRollDiceClicked()
        {
            PhaseHandler.CurrentPhase?.OnRollDiceClicked();

            if (Game.LastRoll == 7)
            {
                Debug.Log("chuj7");
                PhaseHandler.TransitionTo(new CardDiscarding());
            }

            else
            {
                PhaseHandler.TransitionTo(new NormalRound());
            }

        }

        public void OnCardsDiscarded()
        {
            PhaseHandler.TransitionTo(new RobberPlacing());
        }

        public void OnNextTurnClicked()
        {
            PhaseHandler.CurrentPhase?.OnNextTurnClicked();

            if (Game.Turn <= Game.PlayerList.Count * 2)
            {
                PhaseHandler.TransitionTo(new FirstRoundsBuilding());
            }

            else
            {
                PhaseHandler.TransitionTo(new BeforeRoll());
            }
        }

        public void OnVictimChosen(Player victim)
        {
            Debug.Log($"GameManager: Victim chosen → {victim.PlayerColor}");

            PhaseHandler.TransitionTo(new NormalRound());
        }

        public void OnPlayersSetupCompleted()
        {
            PhaseHandler.TransitionTo(new FirstRoundsBuilding());
        }

        public void OnRobberPlaced(Player? victim)
        {
            if (victim != null)
            {
                PhaseHandler.TransitionTo(new CardStealing(victim));
            }

            else
            {
                Debug.Log("No victim to steal from");
                PhaseHandler.TransitionTo(new NormalRound());
            }
        }

        public void OnTradeOffered(ResourceCostOrStock cardsOffered)
        {
            PhaseHandler.TransitionTo(new TradeOffer(cardsOffered));
        }

        public void OnBankTradeClicked()
            {
            PhaseHandler.TransitionTo(new BankTrade());
            }

        public void OnCardStolen()
        {
            PhaseHandler.TransitionTo(new NormalRound());
        }

        public void OnBuildFreeVillageClicked()
        {
            PhaseHandler.CurrentPhase?.OnBuildFreeVillageClicked();
        }

        public void OnBuildFreeRoadClicked()
        {
            PhaseHandler.CurrentPhase?.OnBuildFreeRoadClicked();
        }

        public void OnBuildVillageClicked()
        {
            PhaseHandler.CurrentPhase?.OnBuildVillageClicked();
        }

        public void OnBuildRoadClicked()
        {
            PhaseHandler.CurrentPhase?.OnBuildRoadClicked();
        }

        public void OnUpgradeVillageClicked()
        {
            PhaseHandler.CurrentPhase?.OnUpgradeVillageClicked();
        }

        public void OnDevelopmentCardBought()
        {
            PhaseHandler.CurrentPhase?.OnDevelopmentCardBought();
        }

        public void OnDevelopmentCardsClicked()
        {
            PhaseHandler.TransitionTo(new DevelopmentCards());
        }

        public void OnTradeFinished()
        {
            PhaseHandler.TransitionTo(new NormalRound());
        }

        public void OnTradeRequested(Player player, ResourceCostOrStock cardsOffered, ResourceCostOrStock cardsDesired)
        {
            PhaseHandler.TransitionTo(new TradeRequest(player, cardsOffered, cardsDesired));
        }

        public void NotifyPlayerCountSelected(int count)
        {
            if (PhaseHandler.CurrentPhase is PlayerSetup setup)
                setup.OnPlayerNumberSelected(count);
        }

        public void OnClickedOnNothing()
        {
            PhaseHandler.CurrentPhase?.OnClickedOnNothing();
            
        }

        void Update()
        {

        }

        /*
        
        refactor

        surowce za druga wioske

        przesuwanie kart/tasowanie

        wybor imienia i koloru

        zaznaczane vertices przechodza nextturn

        rozmiar kart na panelach

        przejscia faz

        delegaty

        refaktor unity

        handel odwrotny
        */  
        

    }
}
