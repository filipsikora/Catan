using Catan.Unity.Helpers;
using Catan.Unity.Visuals.Controllers;
using Unity.Catan.Panels;
using UnityEngine;

namespace Catan.Unity.Panels
{
    public class ManagerUI : MonoBehaviour
    {
        public MainUI MainUIPanel;
        public PlayerUI PlayerUIPanel;
        public CardTheftUI CardTheftPanel;
        public VictimSelectionUI VictimSelectorPanel;
        public CardDiscardUI CardDiscardPanel;
        public TradeOfferUI TradeOfferPanel;
        public TradeRequestUI TradeRequestPanel;
        public BankTradeUI BankTradePanel;
        public DevelopmentCardsUI DevelopmentCardsPanel;
        public CardSelectorUI CardSelectorPanel;
        public LogsUI LogsPanel;
        public InformationUI InformationPanel;
        public AwaitingUI AwaitingPanel;

        public FactoryResourceCards factoryResourceCards;
        public FactoryDevelopmentCards factoryDevCards;
            
        public void Initialize(EventBus bus, ControllerResourceCards controller, BoardManager boardManager, LogQueue logQueue)
        {
            factoryResourceCards.Initialize(bus, boardManager);

            BankTradePanel.Initialize(controller);

            LogsPanel.Initialize(logQueue);

            MainUIPanel.gameObject.SetActive(true);
            PlayerUIPanel.gameObject.SetActive(true);
            InformationPanel.gameObject.SetActive(true);
            LogsPanel.gameObject.SetActive(true);
        }

        public void ShowTradeOfferButton() => MainUIPanel.ShowTradeOfferButton();
        public void HideTradeOfferButton() => MainUIPanel.HideTradeOfferButton();
    }
}
