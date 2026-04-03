using Catan.Unity.Data;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public Transform Board;

    public Material WaterMaterial;
    public Material IdleGridMaterial;

    public GameObject HexTilePrefab;
    public GameObject HexNumberPrefab;
    public GameObject CubeVillagePrefab;
    public GameObject CubeRoadPrefab;
    public GameObject CubeTownPrefab;
    public GameObject CubeRobberPrefab;
    public GameObject CubePortPrefab;

    public List<FieldTypeMaterial> FieldMaterialsList;
    public List<RegistryDataResource> ResourceList;
}