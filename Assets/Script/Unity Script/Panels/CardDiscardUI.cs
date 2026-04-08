using Catan.Unity.Data;
using Catan.Unity.Visuals;
using Catan.Unity.Visuals.Models;
using Catan.Shared.Data;
using Catan.Unity.Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Catan.Shared.Dtos;

namespace Catan.Unity.Panels
{
    public class CardDiscardUI : VisualButton<EnumCardSelectorDiscardUIButtons>
    {
        public Transform CardsContainer;
        public FactoryResourceCards CardFactory;
        public TextMeshProUGUI TitleText;
        public Button ConfirmDiscardButton;


        public void Awake()
        {
            RegisterButton(EnumCardSelectorDiscardUIButtons.ConfirmDiscard, ConfirmDiscardButton);
        }

        public void Show()
        {
            VisualsUI.ClearContainer(CardsContainer);

            gameObject.SetActive(true);
        }

        public void ShowForPlayer(PlayerCardsDto playerResources)
        {
            VisualsUI.ClearContainer(CardsContainer);
            ConfirmDiscardButton.gameObject.SetActive(false);

            foreach (var entry in playerResources.PlayerResources)
            {
                var type = entry.Key;
                var amount = entry.Value;

                for (int i = 0; i < amount; i++)
                {
                    CardFactory.DrawResourceCard(type, EnumResourceCardLocation.VictimHand, CardsContainer);
                }
            }
        }
    }
}