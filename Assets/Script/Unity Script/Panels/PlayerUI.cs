using UnityEngine;
using TMPro;
using Catan.Unity.Helpers;
using Catan.Shared.Data;
using Catan.Unity.Visuals;
using Catan.Application.Snapshots;

namespace Catan.Unity.Panels
{
    public class PlayerUI : MonoBehaviour
    {
        public TextMeshProUGUI PlayerNameText;
        public TextMeshProUGUI PlayerBuildingsText;
        public TextMeshProUGUI PlayerPointsText;

        public Transform ResourceCardsPanel;

        public FactoryResourceCards ResourceCardFactory;

        public void UpdatePlayerInfo(PlayerDataSnapshot dataSnapshot, PlayerResourcesSnapshot resourcesSnapshot)
        {
            UpdateTexts(dataSnapshot);
            UpdateResourceCards(resourcesSnapshot);
        }

        public void UpdateTexts(PlayerDataSnapshot dataSnapshot)
        {
            PlayerNameText.text = $"{dataSnapshot.Name}";

            string buildingsInfo = "";

            foreach (var (key, value) in dataSnapshot.BuildingsLeft)
            {;
                buildingsInfo += $"{key}: {value} available\n";
            }

            PlayerBuildingsText.text = buildingsInfo;
            PlayerPointsText.text = $"{dataSnapshot.Name}: {dataSnapshot.Points} points, {dataSnapshot.Knights} knights, {dataSnapshot.VictoryPoints + dataSnapshot.ExtraPoints} extra points";
        }

        public void UpdateResourceCards(PlayerResourcesSnapshot resourcesSnapshot)
        {
            VisualsUI.ClearContainer(ResourceCardsPanel);

            foreach (var entry in resourcesSnapshot.PlayerResources)
            {
                EnumResourceTypes type = entry.Key;
                int count = entry.Value;

                for (int i = 0; i < count; i++)
                {
                    ResourceCardFactory.DrawResourceCard(type, EnumResourceCardLocation.PlayerHand, ResourceCardsPanel);
                }
            }
        }
    }
}