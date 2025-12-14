using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Catan
{
    public class VisualsBoard : MonoBehaviour
    {
        private BuilderMap _builder;
        public Material IdleGridMaterial;
        public GameState Game;
        private GameObject? _robberInstance;

        internal void Initialize(BuilderMap builder, Material idleMat, GameState game)
        {
            _builder = builder;
            IdleGridMaterial = idleMat;
            Game = game;

            PlaceRobberObject();
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
        public void ResetMarkedPositions()
        {
            foreach (var vertex in Game.Map.VertexList)
            {
                vertex.IsMarked = false;
                var obj = GetVertexObject(vertex.Id);

                if (obj != null)
                {
                    SetVertexVisual(obj, IdleGridMaterial.color);
                }
            }

            foreach (var edge in Game.Map.Edges)
            {
                edge.IsMarked = false;
                var obj = GetEdgeObject(edge.Id);

                if (obj != null)
                {
                    SetEdgeVisual(obj, IdleGridMaterial.color, 0.15f);
                }
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

        public void PlaceRobberObject()
        {
            HexTile desertHex = Game.Map.HexList.Find(h => h.FieldType == EnumFieldTypes.Desert);
            if (desertHex == null) return;

            GameObject desertTile = GetHexObject(desertHex.Id);

            Vector3 pos = desertTile.gameObject.transform.position;

            _robberInstance = PlaceObject(ManagerGame.Instance.CubeRobberPrefab, pos, null, null, ManagerGame.Instance.Board);
        }

        public void MoveRobberObject(HexTile hex)
        {
            if (_robberInstance == null) return;

            GameObject hexObj = GetHexObject(hex.Id);
            if (hexObj == null) return;

            MoveObject(_robberInstance, hexObj.transform.position);
        }

        public GameObject? GetVertexObject(int id) => _builder.FindVertexObjectById(id);

        public GameObject? GetEdgeObject(int id) => _builder.FindEdgeObjectById(id);

        public GameObject? GetHexObject(int id) => _builder.FindHexObjectById(id);

        public Quaternion GetEdgeRotation(Edge edge) => _builder.GetEdgeRotation(edge);

        public (Vector3 start, Vector3 end, Vector3 mid) GetEdgePositions(Edge edge) => _builder.GetEdgePositions(edge);
    }
}
