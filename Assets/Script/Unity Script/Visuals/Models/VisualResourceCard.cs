using Catan.Shared.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using Catan.Unity.Data;
using Catan.Unity.Communication.InternalUIEvents;
using Catan.Unity.Helpers;
using Catan.Unity.Visuals.Controllers;

namespace Catan.Unity.Visuals.Models
{
    public class VisualResourceCard : MonoBehaviour, IPointerClickHandler
    {
        public EnumResourceCardLocation Location;
        public int VisualResourceCardId;
        public EnumResourceTypes Type;
        public EnumResourceCardVisualState State = EnumResourceCardVisualState.None;
        public bool IsToggled = false;

        private EventBus _bus;
        private ControllerResourceCards _controller;

        public void Initialize(EnumResourceCardLocation location, int visualResourceCardId, EnumResourceTypes type, EventBus bus, ControllerResourceCards controller)
        {
            Location = location;
            VisualResourceCardId = visualResourceCardId;
            Type = type;
            _bus = bus;
            _controller = controller;
            _controller.Register(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            bool isLeftClicked = eventData.button == PointerEventData.InputButton.Left;

            _bus.Publish(new ResourceCardClickedUIEvent(VisualResourceCardId, Type, Location, isLeftClicked, IsToggled));
        }

        private void OnDestroy()
        {
            _controller.Unregister(this);
        }

        public override string ToString()
        {
            return $"Card: {Type}, {VisualResourceCardId}";
        }   
    }
}