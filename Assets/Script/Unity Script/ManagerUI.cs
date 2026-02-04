using Catan.Application.Snapshots;
using UnityEngine;

namespace Catan.Unity.Panels
{

    public class ManagerUI : MonoBehaviour
    {
        public MainUI MainUIPanel;
        public PlayerUI PlayerUIPanel;
        public CardTheftUI CardTheftPanel;
        public VictimSelectionUI VictimSelectorPanel;
        public GameObject PlayerSelectorPanel;
        public CardDiscardUI CardDiscardPanel;
        public TradeOfferUI TradeOfferPanel;
        public TradeRequestUI TradeRequestPanel;
        public BankTradeUI BankTradePanel;
        public DevelopmentCardsUI DevelopmentCardsPanel;
        public CardSelectorUI CardSelectorPanel;
        public LogsUI LogsPanel;

        public void UpdateTurnCounter(int turn) => MainUIPanel.UpdateTurnCounter(turn);
        public void UpdateRolledDice(int lastRoll) => MainUIPanel.UpdateRolledDice(lastRoll);

        public void ShowTradeOfferButton() => MainUIPanel.ShowTradeOfferButton();
        public void HideTradeOfferButton() => MainUIPanel.HideTradeOfferButton();

        public void UpdatePlayerInfo(PlayerDataSnapshot dataSnapshot, PlayerResourcesSnapshot resourcesSnapshot) => PlayerUIPanel.UpdatePlayerInfo(dataSnapshot, resourcesSnapshot);
    }
}
