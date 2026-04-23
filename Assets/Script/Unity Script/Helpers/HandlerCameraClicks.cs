using UnityEngine;
using UnityEngine.InputSystem;
using Catan.Unity.Visuals.Models;
using Catan.Unity.InternalUIEvents;

namespace Catan.Unity.Helpers
{
    public class HandlerCameraClicks : MonoBehaviour
    {
        private EventBus _bus;
        private Camera _cam;

        private void Awake()
        {
            _cam = Camera.main;
        }

        public void Initialize(EventBus bus)
        {
            _bus = bus;
        }

        private void Update()
        {
            if (!Mouse.current.leftButton.wasPressedThisFrame)
                return;

            Vector2 mousePos = Mouse.current.position.ReadValue();
            Ray ray = _cam.ScreenPointToRay(mousePos);
            int mask = LayerMask.GetMask("VertexLayer", "EdgeLayer", "HexLayer");

            if (Physics.Raycast(ray, out var hit, 100f, mask))
            {
                if (hit.collider.TryGetComponent<VisualVertex>(out var v))
                {
                    UnityEngine.Debug.Log($"Vertex");
                    _bus.Publish(new VertexClickedUIEvent(v.VertexId));
                }

                else if (hit.collider.TryGetComponent<VisualEdge>(out var e))
                    _bus.Publish(new EdgeClickedUIEvent(e.EdgeId));

                else if (hit.collider.TryGetComponent<VisualHex>(out var h))
                {
                    UnityEngine.Debug.Log($"Hex {h.HexId}");
                    _bus.Publish(new HexClickedUIEvent(h.HexId));
                }
            }
        }
    }
}