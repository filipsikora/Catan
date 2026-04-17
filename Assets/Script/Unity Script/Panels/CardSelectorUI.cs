using Catan.Unity.Data;
using Catan.Unity.Helpers;
using Catan.Unity.Visuals.Models;
using Catan.Unity.Visuals;
using Catan.Shared.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Catan.Unity.Panels
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

        public void Show(string description)
        {
            DescriptionText.text = description;
            gameObject.SetActive(true);

            VisualsUI.ClearContainer(DesiredCardsContainer);

            foreach (EnumResourceType type in Enum.GetValues(typeof(EnumResourceType)))
            {
                CardFactory.DrawResourceCard(type, EnumResourceCardLocation.DesiredTrade, DesiredCardsContainer);
            }
        }
    }
}
