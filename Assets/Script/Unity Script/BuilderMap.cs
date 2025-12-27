#nullable enable
using Catan.Unity.Data;
using Catan.Unity.Visuals.Models;
using Catan.Shared.Data;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Catan.Core.Engine;
using Catan.Core.Models;

namespace Catan.Unity
{
    internal class BuilderMap
    {

        public float Size;
        public List<FieldTypeMaterial> FieldMaterialsList;
        public Material IdleGridMaterial;
        public Material WaterMaterial;

        public GameState Game;
        public Transform Board;

        public GameObject HexTilePrefab;
        public GameObject HexNumberPrefab;
        public GameObject CubeRobberPrefab;
        public GameObject CubePortPrefab;

        public Dictionary<int, GameObject> VertexObjects = new();
        public Dictionary<int, GameObject> EdgeObjects = new();
        public Dictionary<int, GameObject> HexObjects = new();

        public Dictionary<EnumResourceTypes, Color>? PortColorLookup;

        public void BuildMap(HexMap map)
        {
            map.GenerateHexesInAxial();
            map.ConvertHexToPixel(Size);
            map.GenerateVerticesInPixel(Size);
            map.AddVerticesToHex(Size);
            map.SortAndIDVertices();
            map.GenerateEdgesInPixels(Size);
            map.SortAndIDEdges();
            map.GetPortsEdges();
            map.SetPorts();

            Game.ReadyFieldList();
            Game.GiveHexesData(map.HexList);

            DrawEdges(map);
            DrawVertices(map);
            DrawHexes(map);
            DrawPorts(map);
        }

        public void DrawHexes(HexMap map)
        {
            foreach (var hex in map.HexList)
            {
                Material mat = FieldMaterialsList.First(f => f.FieldType == hex.FieldType).Material;
                Vector3 pos = new Vector3(hex.X, 0, hex.Y);

                GameObject hexObject = GameObject.Instantiate(HexTilePrefab, pos, HexTilePrefab.transform.rotation, Board);

                hexObject.GetComponent<Renderer>().material = mat;
                hexObject.layer = LayerMask.NameToLayer("HexLayer");

                hexObject.AddComponent<Rigidbody>().isKinematic = true;
                var clickable = hexObject.AddComponent<VisualHex>();
                clickable.HexId = hex.Id;

                HexObjects[hex.Id] = hexObject;

                if (hex.FieldType != EnumFieldTypes.Desert)
                {
                    Vector3 numberPos = new Vector3(hex.X, 0.1f, hex.Y);
                    GameObject numberObject = GameObject.Instantiate(HexNumberPrefab, numberPos, HexNumberPrefab.transform.rotation, Board);

                    var textMesh = numberObject.GetComponentInChildren<TextMeshPro>();
                    textMesh.text = hex.FieldNumber.ToString();
                }
            }
        }

        public void DrawEdges(HexMap map)
        {
            foreach (var edge in map.Edges)
            {
                var (start, end, mid) = GetEdgePositions(edge);

                GameObject edgeObject = new GameObject($"Edge_{edge.Id}");

                edgeObject.transform.parent = Board;
                edgeObject.transform.position = mid;
                edgeObject.transform.rotation = GetEdgeRotation(edge);
                edgeObject.transform.Rotate(0, 90f, 0);
                edgeObject.layer = LayerMask.NameToLayer("EdgeLayer");

                LineRenderer lr = edgeObject.AddComponent<LineRenderer>();
                lr.positionCount = 2;
                lr.SetPosition(0, start);
                lr.SetPosition(1, end);
                lr.startWidth = 0.1f;
                lr.endWidth = 0.1f;

                if (IdleGridMaterial != null)
                    lr.material = new Material(IdleGridMaterial);

                lr.useWorldSpace = true;

                Rigidbody rb = edgeObject.AddComponent<Rigidbody>();
                rb.isKinematic = true;

                BoxCollider bc = edgeObject.AddComponent<BoxCollider>();
                float length = Vector3.Distance(start, end);
                float thickness = 0.15f;
                float height = 0.1f;

                bc.size = new Vector3(length, height, thickness);
                bc.center = new Vector3(0, height / 2f, 0);

                var clickable = edgeObject.AddComponent<VisualEdge>();
                clickable.EdgeId = edge.Id;

                EdgeObjects[edge.Id] = edgeObject;
            }
        }

        public void DrawVertices(HexMap map)
        {
            float vertexHeight = 0.15f;

            foreach (var vertex in map.VertexList)
            {
                GameObject vertexObject;

                vertexObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                vertexObject.transform.position = new Vector3(vertex.X, 0 + vertexHeight / 2f, vertex.Y);
                vertexObject.transform.localScale = new Vector3(0.2f, vertexHeight / 2f, 0.2f);
                vertexObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                vertexObject.transform.parent = Board;
                vertexObject.name = $"Vertex_{vertex.Id}";

                vertexObject.layer = LayerMask.NameToLayer("VertexLayer");

                vertexObject.AddComponent<Rigidbody>().isKinematic = true;
                var clickable = vertexObject.AddComponent<VisualVertex>();
                clickable.VertexId = vertex.Id;

                if (IdleGridMaterial != null)
                {
                    Renderer renderer = vertexObject.GetComponent<Renderer>();
                    renderer.material = IdleGridMaterial;
                }

                VertexObjects[vertex.Id] = vertexObject;
            }
        }

        public void DrawPorts(HexMap map)
        {
            foreach (var port in map.PortList)
            {
                var (start, end, mid) = GetEdgePositions(port.Edge);
                Quaternion rotation = GetEdgeRotation(port.Edge);

                Vector3 direction = end - start;
                Vector3 perpendicular = Vector3.Cross(direction, Vector3.up).normalized;
                Vector3 portPos = mid + perpendicular * -0.30f;

                GameObject portObject = GameObject.Instantiate(CubePortPrefab, portPos, rotation, Board);

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

        public GameObject? FindVertexObjectById(int id)
        {
            if (VertexObjects.TryGetValue(id, out var obj))
                return obj;

            Debug.LogWarning($"[MapBuilder] Vertex object with ID {id} not found!");
            return null;
        }

        public GameObject? FindEdgeObjectById(int id)
        {
            if (EdgeObjects.TryGetValue(id, out var obj))
                return obj;

            Debug.LogWarning($"[MapBuilder] Edge object with ID {id} not found!");
            return null;
        }

        public GameObject? FindHexObjectById(int id)
        {
            if (HexObjects.TryGetValue(id, out var obj))
                return obj;

            Debug.LogWarning($"[MapBuilder] Hex object with ID {id} not found!");
            return null;
        }

        public Quaternion GetEdgeRotation(Edge edge)
        {
            Vector3 start = new Vector3(edge.VertexA.X, 0.05f, edge.VertexA.Y);
            Vector3 end = new Vector3(edge.VertexB.X, 0.05f, edge.VertexB.Y);

            Vector3 direction = end - start;
            direction.Normalize();

            return Quaternion.LookRotation(direction);
        }
        public (Vector3 start, Vector3 end, Vector3 mid) GetEdgePositions(Edge edge)
        {
            Vector3 start = new Vector3(edge.VertexA.X, 0.05f, edge.VertexA.Y);
            Vector3 end = new Vector3(edge.VertexB.X, 0.05f, edge.VertexB.Y);
            Vector3 mid = (start + end) / 2f;

            return (start, end, mid);
        }
    }
}
