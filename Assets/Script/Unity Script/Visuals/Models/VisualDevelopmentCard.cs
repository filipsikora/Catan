using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Catan.Shared.Data;
using Catan.Unity.Helpers;
using Catan.Unity.InternalUIEvents;
using Catan.Shared.Dtos;
using Catan.Unity.Models;

namespace Catan.Unity.Visuals.Models
{
    public class VisualDevelopmentCard : MonoBehaviour
    {

        [SerializeField] private Sprite KnightSprite;
        [SerializeField] private Sprite VictoryPointSprite;

        [SerializeField] private Image CardBackground;
        [SerializeField] public UnityEngine.UI.Image Icon;
        public TextMeshProUGUI Name;
        [SerializeField] public TMPro.TextMeshProUGUI Label;

        private EventBus _bus;

        private int _id { get; set; }

        public void Initialize(DevCardModel devCard, EventBus bus)
        {
            _id = devCard.Id;
            _bus = bus;

            SetupVisuals(devCard.Type, devCard.IsPlayable);
        }

        public void OnCardClicked()
        {
            _bus.Publish(new DevelopmentCardClickedUIEvent(_id));
        }

        public void SetupVisuals(EnumDevelopmentCardTypes type, bool isPlayable)
        {
            Icon.gameObject.SetActive(false);
            Icon.sprite = null;
            Name.text = "";
            Label.text = "";
            CardBackground.color = Color.white;

            switch (type)
            {
                case EnumDevelopmentCardTypes.Knight:
                    Icon.sprite = KnightSprite;
                    Icon.gameObject.SetActive(true);
                    Name.text = "Knight";
                    Label.text = "";
                    break;

                case EnumDevelopmentCardTypes.VictoryPoint:
                    Icon.sprite = VictoryPointSprite;
                    Icon.gameObject.SetActive(true);
                    Name.text = "Victory point";
                    Label.text = "";
                    break;

                case EnumDevelopmentCardTypes.Monopoly:
                    Icon.gameObject.SetActive(false);
                    Name.text = "Monopoly";
                    Label.text = "Steal all copies of a selected resource from all players";
                    break;

                case EnumDevelopmentCardTypes.RoadBuilding:
                    Icon.gameObject.SetActive(false);
                    Name.text = "Road Building";
                    Label.text = "Build two roads for free";
                    break;

                case EnumDevelopmentCardTypes.YearOfPlenty:
                    Icon.gameObject.SetActive(false);
                    Name.text = "Year of Plenty";
                    Label.text = "Get two resources from the bank";
                    break;
            }
         
            if (!isPlayable)
            {
                CardBackground.color = Color.red;
            }
        }
    }
}