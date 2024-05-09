using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapMaker : MonoBehaviour
{
    private readonly string mapTag = "Map";

    private Transform mapRoot;
    private short[,] mapArray;
    private GameObject[,] tileArray;
    private TileFactory tileFactory = new TileFactory();
    private short tileSize = 10;

    // 임시로 수동 연결, 차후 자동화 필요
    [SerializeField]
    private MovableCamera movableCamera;

    public Transform MapRoot => mapRoot;
    public GameObject[,] TileArray => tileArray;

    // 차후 맵 데이터 외부에서 받아서 만들수있도록 변경
    // 임시로 하드코딩해놓은 맵 배열
    private short[,] testArray =
    {
        { 0,0,0,0,1,0,0,0,1,1,1,1,1},
        { 0,0,0,0,1,0,0,0,1,0,0,0,1},
        { 0,0,0,0,1,0,0,0,1,0,0,0,1},
        { 0,0,0,0,1,0,0,0,1,0,0,0,1},
        { 1,1,1,1,1,1,1,1,1,1,1,1,1},
        { 0,0,0,0,1,0,0,0,1,0,0,0,0},
        { 0,0,0,0,1,0,0,0,1,0,0,0,0},
        { 0,0,0,0,1,0,0,0,1,0,0,0,0},
        { 1,1,1,1,1,1,1,1,1,1,1,1,1},
        { 1,0,0,0,1,0,0,0,1,0,0,0,1},
        { 1,0,0,0,1,0,0,0,1,0,0,0,1},
        { 1,0,0,0,1,0,0,0,1,0,0,0,1},
        { 1,1,1,1,1,0,0,0,1,1,1,1,1}
    };

    void Start()
    {
        mapArray = testArray;
        SpawnMap();

        movableCamera.Init(mapRoot);
    }

    private void SpawnMap()
    {
        if (mapRoot == null)
        {
            GameObject newMapRoot = new GameObject();
            newMapRoot.tag = mapTag;
            newMapRoot.name = mapTag;
            mapRoot = newMapRoot.transform;
        }
        else
        {
            mapRoot = GameObject.FindWithTag(mapTag).transform;
        }

        if (mapArray == null)
        {
            Debug.Log("mapArray is null");
            return;
        }

        int row = mapArray.GetLength(0);
        int col = mapArray.GetLength(1);

        tileArray = new GameObject[row, col];

        for (int z = 0; z < row; z++)
        {
            for (int x = 0; x < col; x++)
            {
                string path = tileFactory.GetTilePath(mapArray[z, x]);
                GameObject tile = Utills.Utility.InstantiateObject(path, mapRoot);
                tile.name = $"{z},{x}";

                tileArray[z, x] = tile;

                // 0,0을 중앙으로 좌상단부터 우하단으로 타일을 생성하기 위해 x값은 증가, z값은 최댓값에서 점점 감소하게 설정
                Vector3 position = new Vector3(x - col / 2, 0, row - z - row / 2) * tileSize;
                tile.transform.position = position;
            }
        }

        BoxCollider collider = mapRoot.AddComponent<BoxCollider>();
        collider.size = new Vector3(100, 150, 100);
    }
}
