using Catan.Shared.Data;
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

        public VisualResourceCard DrawResourceCard(EnumResourceTypes type, EnumResourceCardLocation location, Transform parent, bool visible = true)
        {
            GameObject cardObject = Instantiate(ResourceCardPrefab, parent);
            var cardVisual = cardObject.GetComponent<VisualResourceCard>();

            cardVisual.Initialize(location, _nextVisualId++, type);
            ManagerGame.Instance.ControllerResourceCardsUI.Register(cardVisual);

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