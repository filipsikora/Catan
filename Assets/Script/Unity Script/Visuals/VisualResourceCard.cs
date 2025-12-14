using Catan.Catan;
using Catan.Communication;
using UnityEngine;
using UnityEngine.EventSystems;
using Catan.Communication.Signals;
using Catan.Core;

namespace Catan
{
    public class VisualResourceCard : MonoBehaviour, IPointerClickHandler
    {
        public ResourceCard LinkedCard;
        public Transform Container;

        public bool IsSelected => LinkedCard.IsSelected;

        public void Initialize(ResourceCard card)
        {
            LinkedCard = card;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            bool isLeftClicked = eventData.button == PointerEventData.InputButton.Left;

            ManagerGame.Instance.EventBus.Publish(new ResourceCardClickedSignal(this, isLeftClicked));
        }

        public override string ToString()
        {
            return $"Card: {LinkedCard.Type}";
        }
    }
}