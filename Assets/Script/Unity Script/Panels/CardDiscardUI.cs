using Catan.Shared.Data;
using Catan.Shared.Dtos;
using Catan.Unity.Data;
using Catan.Unity.Helpers;
using Catan.Unity.Visuals;
using Catan.Unity.Visuals.Controllers;
using Catan.Unity.Visuals.Models;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Catan.Unity.Panels
{
    public class CardDiscardUI : VisualButton<EnumCardSelectorDiscardUIButtons>
    {
        public Transform CardsContainer;
        public TextMeshProUGUI TitleText;
        public Button ConfirmDiscardButton;

        public FactoryResourceCards CardFactory;
        private ControllerResourceCards _resourceCardsController;


        public void Awake()
        {
            RegisterButton(EnumCardSelectorDiscardUIButtons.ConfirmDiscard, ConfirmDiscardButton);
        }

        public void Show(Dictionary<EnumResourceType, int> resources)
        {
            VisualsUI.ClearContainer(CardsContainer, _resourceCardsController);

            gameObject.SetActive(true);
            ConfirmDiscardButton.gameObject.SetActive(false);

            foreach (var entry in resources)
            {
                for (int i = 0; i < entry.Value; i++)
                    CardFactory.Create(entry.Key, EnumResourceCardLocation.VictimHand, CardsContainer, _resourceCardsController);
            }
        }
    }
}