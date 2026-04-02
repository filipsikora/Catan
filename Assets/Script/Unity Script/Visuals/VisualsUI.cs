#nullable enable
using UnityEngine;
using Catan.Unity.Visuals.Models;
using Catan.Unity.Data;
using Catan.Unity.Panels;

namespace Catan.Unity.Visuals
{
    public static class VisualsUI
    {
        public static void ClearContainer(Transform panel)
        {
            foreach (Transform child in panel)
            {
                Object.Destroy(child.gameObject);
            }
        }

        public static void ResetResourceCardsInParent(Transform container)
        {
            foreach (Transform child in container)
            {
                var card = child.GetComponent<VisualResourceCard>();

                if (card == null)
                    return;

                card.Reset();
            }
        }

        public static void ShowNextTurnUI(MainUI ui)
        {
            ui.Show(EnumMainUIButtons.NextTurn);
            ui.Show(EnumMainUIButtons.RolledNumber);
            ui.Hide(EnumMainUIButtons.RollDice);
            SetConstantChoicesVisibility(ui, true);
        }

        public static void ShowRollDiceUI(MainUI ui)
        {
            ui.Hide(EnumMainUIButtons.NextTurn);
            ui.Hide(EnumMainUIButtons.RolledNumber);
            ui.Show(EnumMainUIButtons.RollDice);
            SetConstantChoicesVisibility(ui, false);
            SetFreeBuildingsVisibility(ui, false);
            SetNormalBuildingsVisibility(ui, false);
        }

        public static void SetConstantChoicesVisibility(MainUI ui, bool visible)
        {
            ui.SetButtonVisibility(EnumMainUIButtons.BankTrade, visible);
            ui.SetButtonVisibility(EnumMainUIButtons.DevelopmentCards, visible);
            ui.SetButtonVisibility(EnumMainUIButtons.BuyDevelopmentCard, visible);
        }

        public static void SetNormalBuildingsVisibility(MainUI ui, bool visible)
        {
            ui.SetButtonVisibility(EnumMainUIButtons.BuildVillage, visible);
            ui.SetButtonVisibility(EnumMainUIButtons.BuildRoad, visible);
            ui.SetButtonVisibility(EnumMainUIButtons.UpgradeVillage, visible);
        }

        public static void SetFreeBuildingsVisibility(MainUI ui, bool visible)
        {
            ui.SetButtonVisibility(EnumMainUIButtons.BuildFreeVillage, visible);
            ui.SetButtonVisibility(EnumMainUIButtons.BuildFreeRoad, visible);
        }

        public static void SetMainAndPlayerUIVisibility(bool visible, MainUI mainUI, PlayerUI playerUI)
        {
            playerUI.gameObject.SetActive(visible);
            mainUI.gameObject.SetActive(visible);
        }

        public static void SetParentVisibility(Component parent, bool visible)
        {
            parent.gameObject.SetActive(visible);
        }

        public static void MakeAllChildrenVisible(Transform container, bool visible)
        {
            foreach (Transform child in container)
            {
                child.gameObject.SetActive(visible);
            }
        }
    }
}