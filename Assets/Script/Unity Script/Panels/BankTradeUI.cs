using Catan.Unity.Data;
using Catan.Unity.Helpers;
using Catan.Unity.Visuals.Models;
using Catan.Unity.Visuals;
using Catan.Shared.Data;
using Catan.Application.Snapshots;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

        public void Show(ResourcesAvailabilitySnapshot resourcesAvailabilitySnapshot)
        {
            gameObject.SetActive(true);

            VisualsUI.ClearContainer(OfferedCardsContainer);
            VisualsUI.ClearContainer(DesiredCardsContainer);

            foreach (var (key, value) in resourcesAvailabilitySnapshot.ResourcesAvailability)
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