using Catan.Catan;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Catan
{
    public class VisualDevelopmentCard : MonoBehaviour
    {
        public EnumDevelopmentCardTypes Type;

        [SerializeField] private Sprite KnightSprite;
        [SerializeField] private Sprite VictoryPointSprite;

        [SerializeField] public UnityEngine.UI.Image Icon;
        public TextMeshProUGUI Name;
        [SerializeField] public TMPro.TextMeshProUGUI Label;

        public void Start()
        {
            SetupVisuals();
        }

        public void SetupVisuals()
        {
            switch (Type)
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
        }

        public void OnCardClicked()
        {
            CatanGameManager.Instance.PhaseHandler.CurrentPhase?.OnDevelopmentCardClicked(this);
        }
    }
}