using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Catan
{
    public class HandlerCameraClicks : MonoBehaviour
    {
        private Camera _cam;

        private void Awake()
        {
            _cam = Camera.main;
        }

        private void Update()
        {
            if (!Mouse.current.leftButton.wasPressedThisFrame)
                return;

            Vector2 mousePos = Mouse.current.position.ReadValue();
            Ray ray = _cam.ScreenPointToRay(mousePos);

            int vertexMask = LayerMask.GetMask("VertexLayer");
            int edgeMask = LayerMask.GetMask("EdgeLayer");
            int hexMask = LayerMask.GetMask("HexLayer");

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, vertexMask))
            {
                hit.collider.GetComponent<VisualVertex>()?.OnVertexClicked();
                return;
            }

            if (Physics.Raycast(ray, out hit, 100f, edgeMask))
            {
                hit.collider.GetComponent<VisualEdge>()?.OnEdgeClicked();
                return;
            }

            if (Physics.Raycast(ray, out hit, 100f, hexMask))
            {
                hit.collider.GetComponent<VisualHex>()?.OnHexClicked();
                return;
            }
        }
    }
}