#nullable enable
using Catan.Catan;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Catan
{
    public static class VisualsUI
    {
        public static void ClearContainer(Transform panel)
        {
            foreach (Transform child in panel)
                Object.Destroy(child.gameObject);
        }

        public static void DrawResourceCard(ResourceCardFactory factory, Transform panel, EnumResourceTypes type, bool visible = true)
        {
            GameObject card = factory.CreateResourceCard(type, panel);

            var image = card.transform.Find("ImageColorCard")?.GetComponent<Image>();

            if (image != null)
            {
                if (visible)
                {
                    var data = CatanGameManager.Instance.ResourceList.First(r => r.Type == type);
                    image.color = data.Color;
                }
                else
                {
                    image.color = Color.black;
                }
            }
        }

        public static void DrawDevelopmentCard(DevelopmentCardFactory factory, Transform container, EnumDevelopmentCardTypes type)
        {
            GameObject card = factory.CreateDevelopmentCard(type, container);

            var visual = card.GetComponent<VisualDevelopmentCard>();
            visual.SetupVisuals();
        }

        public static void MoveResourceCardUp(VisualResourceCard card)
        {
            Debug.Log($"{card} up");
        }

        public static void MoveResourceCardDown(VisualResourceCard card)
        {
            Debug.Log($"{card} down");
        }

        public static void DeselectAllResourceCards(IEnumerable<VisualResourceCard> cards)
        {
            foreach (var card in cards)
            {
                if (card.IsSelected)
                {
                    card.ToggleSelection();
                }
            }
        }
    }
}