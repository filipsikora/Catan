using Catan.Catan;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Catan
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

            CardFactory.ResetIds();

            gameObject.SetActive(true);
            ConfirmDiscardButton.gameObject.SetActive(false);
        }

        public void ShowForPlayer(Player player)
        {
            VisualsUI.ClearContainer(CardsContainer);
            ConfirmDiscardButton.gameObject.SetActive(false);

            foreach (var entry in player.Resources.ResourceDictionary)
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