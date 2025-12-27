using Catan.Shared.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using Catan.Unity.Data;
using Catan.Unity.Communication.InternalUIEvents;

namespace Catan.Unity.Visuals.Models
{
    public class VisualResourceCard : MonoBehaviour, IPointerClickHandler
    {
        public EnumResourceCardLocation Location;
        public int VisualResourceCardId;
        public EnumResourceTypes Type;
        public EnumResourceCardVisualState State = EnumResourceCardVisualState.None;
        public bool IsToggled = false;

        public void Initialize(EnumResourceCardLocation location, int visualResourceCardId, EnumResourceTypes type)
        {
            Location = location;
            VisualResourceCardId = visualResourceCardId;
            Type = type;
        }

        public void ToggleCard()
        {
            IsToggled = !IsToggled;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            bool isLeftClicked = eventData.button == PointerEventData.InputButton.Left;

            ManagerGame.Instance.EventBus.Publish(new ResourceCardClickedUIEvent(VisualResourceCardId, Type, Location, isLeftClicked));
        }

        private void OnDestroy()
        {
            if (ManagerGame.Instance != null)
            {
                ManagerGame.Instance.ControllerResourceCardsUI.Unregister(this);
            }
        }

        public override string ToString()
        {
            return $"Card: {Type}, {VisualResourceCardId}";
        }
    }
}