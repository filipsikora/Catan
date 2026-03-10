using System.Collections.Generic;
using UnityEngine;

namespace Catan.Unity.Visuals
{
    public class VisualsBoard : MonoBehaviour
    {
        private BuilderMap _builder;
        public Material IdleGridMaterial;
        private GameObject? _robberInstance;

        internal void Initialize(BuilderMap builder, Material idleMat)
        {
            _builder = builder;
            IdleGridMaterial = idleMat;
        }

        public void SetVertexVisual(GameObject vertexObject, Color color)
        {
            var renderer = vertexObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = color;
            }
        }
        public void SetEdgeVisual(GameObject edgeObject, Color color, float width = 0.2f)
        {
            var lr = edgeObject.GetComponent<LineRenderer>();
            if (lr != null)
            {
                lr.startWidth = width;
                lr.endWidth = width;
                lr.startColor = color;
                lr.endColor = color;

                lr.material = new Material(Shader.Find("Unlit/Color"));
                lr.material.color = color;
            }
        }

        public void ResetMarkedVertex(GameObject vertexObject)
        {
            if (vertexObject != null)
            {
                SetVertexVisual(vertexObject, IdleGridMaterial.color);
            }
        }

        public void ResetMarkedEdge(GameObject edgeObject)
        {
            if (edgeObject != null)
            {
                SetEdgeVisual(edgeObject, IdleGridMaterial.color, 0.15f);
            }
        }

        public GameObject PlaceObject(GameObject prefab, Vector3 position, Quaternion? rotation = null, Color? color = null, Transform? parent = null)
        {
            position.y += 0.1f;
            GameObject obj = Instantiate(prefab, position, rotation ?? Quaternion.identity, parent);

            if (color.HasValue)
            {
                var renderer = obj.GetComponent<Renderer>();
                if (renderer != null) renderer.material.color = color.Value;
            }
            return obj;
        }

        public void MoveObject(GameObject obj, Vector3 position)
        {
            position.y += 0.1f;
            obj.transform.position = position;
        }

        public GameObject? GetVertexObject(int id) => _builder.GetVertexObjectById(id);

        public GameObject? GetEdgeObject(int id) => _builder.GetEdgeObjectById(id);

        public GameObject? GetHexObject(int id) => _builder.GetHexObjectById(id);

        public IEnumerable<int> GetVerticesIds() => _builder.GetVerticesIds();

        public IEnumerable<int> GetEdgesIds() => _builder.GetEdgesIds();

        public (Vector3 start, Vector3 mid, Vector3 end, Vector3 dir, Quaternion rotation) GetEdgeVisualData(int edgeId) => _builder.GetEdgeVisualData(edgeId);
    }
}
