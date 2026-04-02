using Catan.Shared.Data;
using Catan.Unity.Visuals.Controllers;
using Catan.Unity.Visuals.Models;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Catan.Unity.Helpers
{
    public class FactoryResourceCards : MonoBehaviour
    {
        public GameObject ResourceCardPrefab;
        private int _nextVisualId = 0;
        private EventBus _bus;
        private ControllerResourceCards _controllerResourceCards;

        public void Initialize(EventBus bus, ControllerResourceCards controllerResourceCards)
        {
            _bus = bus;
            _controllerResourceCards = controllerResourceCards;
        }

        public VisualResourceCard DrawResourceCard(EnumResourceType type, EnumResourceCardLocation location, Transform parent, bool visible = true)
        {
            GameObject cardObject = Instantiate(ResourceCardPrefab, parent);
            var cardVisual = cardObject.GetComponent<VisualResourceCard>();

            cardVisual.Initialize(location, _nextVisualId++, type, _bus, _controllerResourceCards);

            var image = cardObject.transform.Find("ImageColorCard")?.GetComponent<Image>();
            
            if (image != null)
            {
                if (visible)
                {
                    var data = ManagerGame.Instance.ResourceList.First(r => r.Type == type);
                    image.color = data.Color;
                }

                else
                {
                    image.color = Color.gray;
                }
            }

            return cardVisual;
        }

        public void ResetIds()
        {
            _nextVisualId = 0;
        }
    }
}