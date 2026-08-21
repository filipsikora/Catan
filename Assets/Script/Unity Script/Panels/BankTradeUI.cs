using Catan.Unity.Data;
using Catan.Unity.Helpers;
using Catan.Unity.Visuals.Models;
using Catan.Unity.Visuals;
using Catan.Shared.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Catan.Shared.Dtos;
using Catan.Unity.Visuals.Controllers;
using System.Collections.Generic;

namespace Catan.Unity.Panels
{
    public class BankTradeUI : VisualButton<EnumBankTradeUIButtons>
    {
        public Transform OfferedCardsContainer;
        public Transform DesiredCardsContainer;

        public TextMeshProUGUI TextRatio;

        public FactoryResourceCards ResourceCardFactory;
        private ControllerResourceCards _resourceCardsController;

        public Button CancelTradeButton;

        public void Awake()
        {
            RegisterButton(EnumBankTradeUIButtons.CancelBankTrade, CancelTradeButton);
        }

        public void Initialize(ControllerResourceCards resourceCardsController)
        {
            _resourceCardsController = resourceCardsController;
        }

        public void Show(Dictionary<EnumResourceType, int> resources)
        {
            VisualsUI.ClearContainer(OfferedCardsContainer, _resourceCardsController);
            VisualsUI.ClearContainer(DesiredCardsContainer, _resourceCardsController);

            foreach (var (key, value) in resources)
            {
                ResourceCardFactory.Create(key, EnumResourceCardLocation.OfferedTrade, OfferedCardsContainer, _resourceCardsController);

                if (value != 0)
                {
                    ResourceCardFactory.Create(key, EnumResourceCardLocation.DesiredTrade, DesiredCardsContainer, _resourceCardsController);
                }
            }
        }

        public void UpdateTradeRatio(int ratio, bool possible, EnumResourceType? type)
        {
            string text;

            if (possible)
            {
                text = $"Trading {ratio} cards of {type} for...";
            }

            else if (!possible)
            {
                text = $"You don't have enough {type} to trade";
            }

            else
            {
                text = "Choose cards to trade with the bank";
            }

            TextRatio.text = text;
        }
    }
}