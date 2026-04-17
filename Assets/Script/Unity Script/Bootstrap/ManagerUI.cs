using Catan.Shared.Dtos;
using Catan.Unity.Helpers;
using Catan.Unity.Visuals.Controllers;
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

        public FactoryResourceCards factoryResourceCards;
        public FactoryDevelopmentCards factoryDevCards;
            
        public void Initialize(EventBus bus, ControllerResourceCards controller, BoardManager boardManager)
        {
            factoryResourceCards.Initialize(bus, controller, boardManager);
            factoryDevCards.Initialize(bus);
            VictimSelectorPanel.Initialize(bus);
            TradeOfferPanel.Initialize(bus);

        }

        public void UpdateTurnCounter(int turn) => MainUIPanel.UpdateTurnCounter(turn);
        public void UpdateRolledDice(int lastRoll) => MainUIPanel.UpdateRolledDice(lastRoll);

        public void ShowTradeOfferButton() => MainUIPanel.ShowTradeOfferButton();
        public void HideTradeOfferButton() => MainUIPanel.HideTradeOfferButton();

        public void UpdatePlayerInfo(PlayerDataDto dataSnapshot, PlayerCardsDto cardsSnapshot) => PlayerUIPanel.UpdatePlayerInfo(dataSnapshot, cardsSnapshot);
    }
}
