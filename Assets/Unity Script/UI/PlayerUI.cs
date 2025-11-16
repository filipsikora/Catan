using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections.Generic;
using Catan.Catan;

namespace Catan
{
    public class PlayerUI : MonoBehaviour
    {
        public TextMeshProUGUI PlayerNameText;
        public TextMeshProUGUI PlayerBuildingsText;
        public TextMeshProUGUI PlayerPointsText;

        public Transform ResourceCardsPanel;

        public ResourceCardFactory CardFactory;

        public void UpdatePlayerInfo(Player player)
        {
            UpdateTexts(player);
            UpdateResourceCards(player);
        }

        public void UpdateTexts(Player player)
        {
            PlayerNameText.text = $"{player.Name}";

            string buildingsInfo = "";
            foreach (var buildingType in BuildingDataRegistry.MaxPerPlayer.Keys)
            {
                int buildingCount = player.Buildings.Count(b => b.GetType() == buildingType);
                int maxCount = BuildingDataRegistry.MaxPerPlayer[buildingType];
                int available = maxCount - buildingCount;

                string buildingName = BuildingDataRegistry.Name[buildingType];
                buildingsInfo += $"{buildingName}: {available} available\n";
            }

            PlayerBuildingsText.text = buildingsInfo;
            PlayerPointsText.text = $"{player.Name}: {player.Points} points, {player.KnightsUsed} knights, {player.VictoryPointsCardsUsed} extra points";
        }

        public void UpdateResourceCards(Player player)
        {
            VisualsUI.ClearContainer(ResourceCardsPanel);

            foreach (var entry in player.Resources.ResourceDictionary)
            {
                EnumResourceTypes type = entry.Key;
                int count = entry.Value;

                for (int i = 0; i < count; i++)
                {
                    VisualsUI.DrawResourceCard(CardFactory, ResourceCardsPanel, type, visible: true);
                }
            }
        }
    }
}