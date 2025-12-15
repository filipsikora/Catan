using Catan;
using Catan.Catan;
using Catan.Core;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Catan
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
            ManagerGame.Instance.HandlerResourceCardsUI.Register(cardVisual);

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