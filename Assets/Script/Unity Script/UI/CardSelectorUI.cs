using Catan.Catan;
using Catan.Core;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Catan
{
    public class CardSelectorUI : VisualButton<EnumCardSelectorDevelopmentUIButtons>
    {
        public Transform DesiredCardsContainer;
        public TextMeshProUGUI DescriptionText;
        public Button AcceptCardsButton;

        public FactoryResourceCards CardFactory;


        public void Awake()
        {
            RegisterButton(EnumCardSelectorDevelopmentUIButtons.AcceptCards, AcceptCardsButton);
        }


        public void Show(string description, bool yearOfPlenty)
        {
            DescriptionText.text = description;
            gameObject.SetActive(true);

            foreach (var key in ManagerGame.Instance.Game.Bank.ResourceDictionary.Keys)
            {
                if (yearOfPlenty && ManagerGame.Instance.Game.Bank.ResourceDictionary[key] <= 0)
                    continue;

                CardFactory.DrawResourceCard(key, EnumResourceCardLocation.DesiredTrade, DesiredCardsContainer);
            }
        }

        public void SetCardAvailability(EnumResourceTypes type, bool available)
        {
            foreach (Transform child in DesiredCardsContainer)
            {
                if (child.TryGetComponent(out VisualResourceCard visual))
                {
                    if (visual.LinkedCard.Type == type)
                    {
                        visual.GetComponent<Collider>().enabled = available;
                    }
                }
            }
        }
    }
}
