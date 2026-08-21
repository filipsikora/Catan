using Catan.Shared.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using Catan.Unity.Data;
using Catan.Unity.InternalUIEvents;
using Catan.Unity.Helpers;

namespace Catan.Unity.Visuals.Models
{
    public class VisualResourceCard : MonoBehaviour, IPointerClickHandler
    {
        public EnumResourceCardLocation Location;
        public int VisualResourceCardId;
        public EnumResourceType Type;
        public EnumResourceCardVisualState State = EnumResourceCardVisualState.None;
        public bool IsSelected = false;

        private EventBus _bus;

        public void Initialize(EnumResourceCardLocation location, int visualResourceCardId, EnumResourceType type, EventBus bus)
        {
            Location = location;
            VisualResourceCardId = visualResourceCardId;
            Type = type;
            _bus = bus;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            bool isLeftClicked = eventData.button == PointerEventData.InputButton.Left;

            _bus.Publish(new ResourceCardClickedUIEvent(VisualResourceCardId, Type, Location, isLeftClicked, IsSelected));
        }

        public void MoveUp()
        {
            Debug.Log($"{this} up");
        }

        public void Highlight()
        {
            Debug.Log($"{this} highlighted");
        }

        public void Reset()
        {
            Debug.Log($"{this} reset");
        }

        public override string ToString()
        {
            return $"Card: {Type}, {VisualResourceCardId}";
        }   
    }
}