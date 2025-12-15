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
        public EnumResourceCardLocation Location;
        public int VisualResourceCardId;
        public EnumResourceTypes Type;

        public void Initialize(EnumResourceCardLocation location, int visualResourceCardId, EnumResourceTypes type)
        {
            Location = location;
            VisualResourceCardId = visualResourceCardId;
            Type = type;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            bool isLeftClicked = eventData.button == PointerEventData.InputButton.Left;

            ManagerGame.Instance.EventBus.Publish(new ResourceCardClickedSignal(this.VisualResourceCardId, this.Type, this.Location, isLeftClicked));
        }

        private void OnDestroy()
        {
            if (ManagerGame.Instance != null)
            {
                ManagerGame.Instance.HandlerResourceCardsUI.Unregister(this);
            }
        }

        public override string ToString()
        {
            return $"Card: {Type}, {VisualResourceCardId}";
        }
    }
}