using UnityEngine;
using TMPro;
using Catan.Unity.Helpers;
using Catan.Shared.Data;
using Catan.Unity.Visuals;
using Catan.Shared.Dtos;

namespace Catan.Unity.Panels
{
    public class PlayerUI : MonoBehaviour
    {
        public TextMeshProUGUI PlayerNameText;
        public TextMeshProUGUI PlayerBuildingsText;
        public TextMeshProUGUI PlayerPointsText;

        public Transform ResourceCardsPanel;

        public FactoryResourceCards ResourceCardFactory;

        public void UpdatePlayerInfo(PlayerDataDto dataDto, PlayerCardsDto cardsDto)
        {
            UpdateTexts(dataDto);
            UpdateResourceCards(cardsDto);
        }

        public void UpdateTexts(PlayerDataDto dataDto)
        {
            PlayerNameText.text = $"{dataDto.Name}";

            string buildingsInfo = "";

            foreach (var (key, value) in dataDto.BuildingsLeft)
            {
                buildingsInfo += $"{key}: {value} available\n";
            }

            PlayerBuildingsText.text = buildingsInfo;
            PlayerPointsText.text = $"{dataDto.Name}: {dataDto.Points} points, {dataDto.Knights} knights, {dataDto.VictoryPoints + dataDto.ExtraPoints} extra points";
        }

        public void UpdateResourceCards(PlayerCardsDto cardsDto)
        {
            VisualsUI.ClearContainer(ResourceCardsPanel);

            foreach (var entry in cardsDto.PlayerResources)
            {
                EnumResourceType type = Mappers.MapStringResourcesToEnum(entry.Key);
                int count = entry.Value;

                for (int i = 0; i < count; i++)
                {
                    ResourceCardFactory.DrawResourceCard(type, EnumResourceCardLocation.PlayerHand, ResourceCardsPanel);
                }
            }
        }
    }
}