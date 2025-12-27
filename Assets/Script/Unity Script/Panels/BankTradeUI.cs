using Catan.Unity.Data;
using Catan.Unity.Helpers;
using Catan.Unity.Visuals.Models;
using Catan.Unity.Visuals;
using Catan.Shared.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Catan.Unity.Panels
{
    public class BankTradeUI : VisualButton<EnumBankTradeUIButtons>
    {
        public Transform OfferedCardsContainer;
        public Transform DesiredCardsContainer;

        public TextMeshProUGUI TextRatio;

        public FactoryResourceCards ResourceCardFactory;

        public Button CancelTradeButton;


        public void Awake()
        {
            RegisterButton(EnumBankTradeUIButtons.CancelBankTrade, CancelTradeButton);
        }

        public void Show(Dictionary<EnumResourceTypes, bool> resourcesAvailability)
        {
            gameObject.SetActive(true);

            VisualsUI.ClearContainer(OfferedCardsContainer);
            VisualsUI.ClearContainer(DesiredCardsContainer);

            foreach (var (key, value) in resourcesAvailability)
            {
                ResourceCardFactory.DrawResourceCard(key, EnumResourceCardLocation.OfferedTrade, OfferedCardsContainer);

                if (value == true)
                {
                    ResourceCardFactory.DrawResourceCard(key, EnumResourceCardLocation.DesiredTrade, DesiredCardsContainer);
                }
            }
        }

        public void UpdateTradeRatio(int ratio, bool possible, EnumResourceTypes? type)
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
