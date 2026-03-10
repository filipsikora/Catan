#nullable enable
using Catan.Core.Snapshots;
using Catan.Unity.Helpers;
using Catan.Shared.Data;
using Catan.Unity.Data;
using Catan.Unity.Visuals.Models;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Helpers;
using UnityEngine;

namespace Catan.Unity
{
    internal class BuilderMap
    {

        public float Size;
        public List<FieldTypeMaterial> FieldMaterialsList;
        public Material IdleGridMaterial;
        public Material WaterMaterial;

        public Transform Board;

        public GameObject HexTilePrefab;
        public GameObject HexNumberPrefab;
        public GameObject CubeRobberPrefab;
        public GameObject CubePortPrefab;

        public Dictionary<int, GameObject> VertexObjects = new();
        public Dictionary<int, GameObject> EdgeObjects = new();
        public Dictionary<int, GameObject> HexObjects = new();

        public Dictionary<int, VertexSnapshot> vertexLookup = new();
        public Dictionary<int, EdgeSnapshot> edgeLookup = new();

        public void BuildMap(BoardSnapshot board, EventBus bus)
        {
            vertexLookup = board.Vertices.ToDictionary(v => v.VertexId);
            edgeLookup = board.Edges.ToDictionary(e => e.EdgeId);

            DrawEdges(board);
            DrawVertices(board, bus);
            DrawHexes(board);
            DrawPorts(board);
        }

        public void DrawHexes(BoardSnapshot board)
        {
            foreach (var hex in board.Hexes)
            {
                Material mat = FieldMaterialsList.First(f => f.FieldType == hex.FieldType).Material;
                Vector3 pos = HexLayout.AxialToPixel(hex.Q, hex.R, Size);

                GameObject hexObject = GameObject.Instantiate(HexTilePrefab, pos, HexTilePrefab.transform.rotation, Board);

                hexObject.GetComponent<Renderer>().material = mat;
                hexObject.layer = LayerMask.NameToLayer("HexLayer");

                hexObject.AddComponent<Rigidbody>().isKinematic = true;
                var clickable = hexObject.AddComponent<VisualHex>();
                clickable.HexId = hex.HexId;

                HexObjects[hex.HexId] = hexObject;

                if (hex.FieldType != EnumFieldTypes.Desert)
                {
                    Vector3 numberPos = HexLayout.AxialToPixel(hex.Q, hex.R, Size) + Vector3.up * 0.1f;
                    GameObject numberObject = GameObject.Instantiate(HexNumberPrefab, numberPos, HexNumberPrefab.transform.rotation, Board);

                    var textMesh = numberObject.GetComponentInChildren<TextMeshPro>();
                    textMesh.text = hex.HexNumber.ToString();
                }
            }
        }

        public void DrawEdges(BoardSnapshot board)
        {
            foreach (var edge in board.Edges)
            {
                var (a, b, mid, dir, rotation) = GetEdgeVisualData(edge.EdgeId);

                GameObject edgeObject = new GameObject($"Edge_{edge.EdgeId}");

                edgeObject.transform.parent = Board;
                edgeObject.transform.position = mid;
                edgeObject.transform.rotation = rotation;
                edgeObject.layer = LayerMask.NameToLayer("EdgeLayer");

                LineRenderer lr = edgeObject.AddComponent<LineRenderer>();
                lr.positionCount = 2;
                lr.SetPosition(0, a);
                lr.SetPosition(1, b);
                lr.startWidth = 0.1f;
                lr.endWidth = 0.1f;

                if (IdleGridMaterial != null)
                    lr.material = new Material(IdleGridMaterial);

                lr.useWorldSpace = true;

                Rigidbody rb = edgeObject.AddComponent<Rigidbody>();
                rb.isKinematic = true;

                BoxCollider bc = edgeObject.AddComponent<BoxCollider>();
                float length = Vector3.Distance(a, b);
                float thickness = 0.15f;
                float height = 0.1f;

                bc.size = new Vector3(length, height, thickness);
                bc.center = new Vector3(0, height / 2f, 0);

                var clickable = edgeObject.AddComponent<VisualEdge>();
                clickable.EdgeId = edge.EdgeId;

                EdgeObjects[edge.EdgeId] = edgeObject;
            }
        }

        public void DrawVertices(BoardSnapshot board, EventBus bus)
        {
            float vertexHeight = 0.15f;

            foreach (var vertex in board.Vertices)
            {
                Vector3 pos = ResolveVertexPosition(vertex);

                var vertexObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                vertexObject.transform.position = pos;
                vertexObject.transform.localScale = new Vector3(0.2f, vertexHeight / 2f, 0.2f);
                vertexObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                vertexObject.transform.parent = Board;
                vertexObject.name = $"Vertex_{vertex.VertexId}";

                vertexObject.layer = LayerMask.NameToLayer("VertexLayer");

                vertexObject.AddComponent<Rigidbody>().isKinematic = true;
                var clickable = vertexObject.AddComponent<VisualVertex>();
                clickable.VertexId = vertex.VertexId;

                if (IdleGridMaterial != null)
                {
                    Renderer renderer = vertexObject.GetComponent<Renderer>();
                    renderer.material = IdleGridMaterial;
                }

                VertexObjects[vertex.VertexId] = vertexObject;
            }
        }

        public void DrawPorts(BoardSnapshot board)
        {
            foreach (var port in board.Ports)
            {
                var (_, _, mid, dir, rotation) = GetEdgeVisualData(port.EdgeId);

                var perpendicular = Vector3.Cross(Vector3.down, dir).normalized;

                Vector3 portPos = mid + perpendicular * -0.30f;

                GameObject portObject = GameObject.Instantiate(CubePortPrefab, portPos, Quaternion.LookRotation(dir), Board);

                Material materialToUse;

                if (port.Type != null)
                {
                    materialToUse = FieldMaterialsList.First(f => f.FieldType == (EnumFieldTypes)port.Type).Material;
                }

                else
                {
                    materialToUse = WaterMaterial;
                }

                var renderer = portObject.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material = materialToUse;
                }
            }
        }

        private Vector3 ResolveVertexPosition(VertexSnapshot v)
        {
            var points = v.Corners.Select(c =>
                HexLayout.GetCorner(c.HexQ, c.HexR, c.CornerIndex, Size)
            );

            return points.Aggregate(Vector3.zero, (a, b) => a + b) / v.Corners.Count
                   + Vector3.up * 0.075f;
        }

        public (Vector3 start, Vector3 end, Vector3 mid, Vector3 dir, Quaternion rotation) GetEdgeVisualData(int edgeId)
        {
            var edgeObject = edgeLookup[edgeId];

            var start = ResolveVertexPosition(vertexLookup[edgeObject.VertexAId]);
            var end = ResolveVertexPosition(vertexLookup[edgeObject.VertexBId]);

            var mid = (start + end) / 2f;
            var dir = (end - start).normalized;
            var rotation = Quaternion.LookRotation(dir);

            return (start, end, mid, dir, rotation);

        }

        public GameObject? GetVertexObjectById(int id)
        {
            if (VertexObjects.TryGetValue(id, out var obj))
                return obj;

            Debug.LogWarning($"[MapBuilder] Vertex object with ID {id} not found!");
            return null;
        }

        public GameObject? GetEdgeObjectById(int id)
        {
            if (EdgeObjects.TryGetValue(id, out var obj))
                return obj;

            Debug.LogWarning($"[MapBuilder] Edge object with ID {id} not found!");
            return null;
        }

        public GameObject? GetHexObjectById(int id)
        {
            if (HexObjects.TryGetValue(id, out var obj))
                return obj;

            Debug.LogWarning($"[MapBuilder] Hex object with ID {id} not found!");
            return null;
        }

        public IEnumerable<int> GetVerticesIds()
        {
            return VertexObjects.Keys;
        }

        public IEnumerable<int> GetEdgesIds()
        {
            return EdgeObjects.Keys;
        }
    }
}
