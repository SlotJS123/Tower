using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public Tile prefab_Tile;

    public GameObject canvas;
    [Header("맵 제작의 x, y 축의 값")]
    public int map_X;
    public int map_Y;
    public int map_H;

    [Header("타일의 생성 기본 위치값이랑 각축의 서로 간격")]
    public float interval_X;
    public float interval_Y;
    public float start_X;
    public float start_Y;

    public List<Tile> tileList = new List<Tile>();  
    public List<Tile> wayTileList = new List<Tile>(); // 굳이 공개를 할 필요가 있나?
    [Range(0, 10)]
    [Header("타일 오브젝트 프리팹")]
    public GameObject tile;

    public Button startButton;

    public Action OnButtonTouchEventHander;

    int sizeX, sizeY;
    public Vector2Int bottomLeft, topRight, startPos, targetPos;
    public List<Tile> FinalNodeList;
    public bool allowDiagonal, dontCrossCorner;


    public Tile[,] NodeArray;
    public Tile StartNode, TargetNode, CurNode;
    public List<Tile> OpenList;
    List<Tile> ClosedList = new List<Tile>();

    //타워 설치 준비가 되었다는 신호를 보내기 위한 bool값입니다 
    public bool towerSetState;

    // Start is called before the first frame update
    void Start()
    {
        bottomLeft =new Vector2Int(0,0);
        topRight = new Vector2Int(9, 9);
        startPos = new Vector2Int(0, 0);
        targetPos = new Vector2Int(9,9);

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ReSetWayTile();
        }
    }

    public void PathFinding()
    {
        // NodeArray의 크기 정해주고, isWall, x, y 대입
    

        //for (int i = 0; i < sizeX; i++)
        //{
        //    for (int j = 0; j < sizeY; j++)
        //    {
        //        bool isWall = false;
        //        foreach (Collider2D col in Physics2D.OverlapCircleAll(new Vector2(i + bottomLeft.x, j + bottomLeft.y), 50f))
        //            if (col.gameObject.layer == LayerMask.NameToLayer("Wall")) isWall = true;

        //        NodeArray[i, j] = new Node(isWall, i + bottomLeft.x, j + bottomLeft.y);
        //    }
        //}


        // 시작과 끝 노드, 열린리스트와 닫힌리스트, 마지막리스트 초기화
        StartNode = NodeArray[startPos.x - bottomLeft.x, startPos.y - bottomLeft.y];
        TargetNode = NodeArray[targetPos.x - bottomLeft.x, targetPos.y - bottomLeft.y];

        OpenList = new List<Tile>() { StartNode };
        ClosedList = new List<Tile>();
        FinalNodeList = new List<Tile>();

        //if(OpenList.Count == 0)
        //{
        //    for (int i = 0; i < wayTileList.Count; i++)
        //    {
        //        Tile tile = wayTileList[i];
        //        OpenList.Add(tile);
        //    }
        //}
        //TargetNode.H = map_H - (10 * OpenList.Count);
        //TargetNode.G = TargetNode.G + (10 * OpenList.Count);
        ////TargetNode.
        //OpenList.Remove(TargetNode);
        //OpenList.Add(TargetNode);
        //OpenList = wayTileList;
        while (OpenList.Count > 0)
        {
            // 열린리스트 중 가장 F가 작고 F가 같다면 H가 작은 걸 현재노드로 하고 열린리스트에서 닫힌리스트로 옮기기
            CurNode = OpenList[0];
            for (int i = 1; i < OpenList.Count; i++)
            {
                if (OpenList[i].F <= CurNode.F && OpenList[i].H < CurNode.H)
                {
                    CurNode = OpenList[i];

                }


            }

            OpenList.Remove(CurNode);
            ClosedList.Add(CurNode);


            // 마지막
            if (CurNode == TargetNode)
            {
                Tile TargetCurNode = TargetNode;
                while (TargetCurNode != StartNode)
                {
                    FinalNodeList.Add(TargetCurNode);
                    TargetCurNode = TargetCurNode.ParentNode;
                }
                FinalNodeList.Add(StartNode);
                FinalNodeList.Reverse();


                towerSetState = true;
                startButton.interactable = true;    

                //맵 세팅이 다 끝났기 때문에 데이터를 SetMapData 함수에 전달을 해줍니다 
                GameManager.Instance.SetMapData(FinalNodeList);

                for (int i = 0; i < FinalNodeList.Count; i++)
                    print(i + "번째는 " + FinalNodeList[i].x + ", " + FinalNodeList[i].y);
                return;
            }


            // ↗↖↙↘
            if (allowDiagonal)
            {
                OpenListAdd(CurNode.x + 1, CurNode.y + 1);
                OpenListAdd(CurNode.x - 1, CurNode.y + 1);
                OpenListAdd(CurNode.x - 1, CurNode.y - 1);
                OpenListAdd(CurNode.x + 1, CurNode.y - 1);
            }

            // ↑ → ↓ ←
            OpenListAdd(CurNode.x, CurNode.y + 1);
            OpenListAdd(CurNode.x + 1, CurNode.y);
            OpenListAdd(CurNode.x, CurNode.y - 1);
            OpenListAdd(CurNode.x - 1, CurNode.y);

            //break;
        }
    }

    void OpenListAdd(int checkX, int checkY)
    {
        // 상하좌우 범위를 벗어나지 않고, 벽이 아니면서, 닫힌리스트에 없다면
        if (checkX >= bottomLeft.x && checkX < topRight.x + 1 && checkY >= bottomLeft.y && checkY < topRight.y + 1 && !NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y].isWall && !ClosedList.Contains(NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y]))
        {
            // 대각선 허용시, 벽 사이로 통과 안됨
            if (allowDiagonal) if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall && NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;

            // 코너를 가로질러 가지 않을시, 이동 중에 수직수평 장애물이 있으면 안됨
            if (dontCrossCorner) if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall || NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;


            // 이웃노드에 넣고, 직선은 10, 대각선은 14비용
            Tile NeighborNode = NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y];
            int MoveCost = CurNode.G + (CurNode.x - checkX == 0 || CurNode.y - checkY == 0 ? 10 : 14);


            // 이동비용이 이웃노드G보다 작거나 또는 열린리스트에 이웃노드가 없다면 G, H, ParentNode를 설정 후 열린리스트에 추가
            if (MoveCost < NeighborNode.G || !OpenList.Contains(NeighborNode))
            {
                NeighborNode.G = MoveCost;
                NeighborNode.H = (Mathf.Abs(NeighborNode.x - TargetNode.x) + Mathf.Abs(NeighborNode.y - TargetNode.y)) * 10;
                NeighborNode.ParentNode = CurNode;

                OpenList.Add(NeighborNode);
            }
        }
    }

    void OnDrawGizmos()
    {
        if (FinalNodeList.Count != 0)
        {
            for (int i = 0; i < FinalNodeList.Count - 1; i++)
            {
                float _x = FinalNodeList[i].GetComponent<Transform>().localPosition.x;
                float _y = FinalNodeList[i].GetComponent<Transform>().localPosition.y;

                float _xNext = FinalNodeList[i+1].GetComponent<Transform>().localPosition.x;
                float _yNext= FinalNodeList[i+1].GetComponent<Transform>().localPosition.y;
                //Gizmos.DrawLine(new Vector2(FinalNodeList[i].x, FinalNodeList[i].y), new Vector2(FinalNodeList[i + 1].x, FinalNodeList[i + 1].y));
                Gizmos.DrawLine(new Vector2(_x, _y), new Vector2(_xNext, _yNext));


            }

        }

        //if (OpenList.Count != 0)
        //{
        //    for (int i = 0; i < OpenList.Count - 1; i++)
        //    {
        //        Gizmos.DrawLine(new Vector2(OpenList[i].x, OpenList[i].y), new Vector2(OpenList[i + 1].x, OpenList[i + 1].y));

        //    }
        //}


    }


  

    /// <summary>
    /// GameManager에서 Map Manager에 맵 제작을 요청할 때 사용하기 위한 함수
    /// </summary>
    public void MapMaking()
    {
        //sizeX = topRight.x - bottomLeft.x + 1;
        //sizeY = topRight.y - bottomLeft.y + 1;
        NodeArray = new Tile[map_X, map_Y];
        for (int i = 0; i < map_X; i++)
        {
            for (int j = 0; j < map_Y; j++)
            {
            
                Tile tile = Instantiate(prefab_Tile, canvas.transform);
                tile.Info(i, j);


                tile.transform.position = new Vector3(start_X + i * interval_X, start_Y + j * interval_Y,0.1f);
                tileList.Add(tile);
                tile.OnTileClick += AddTileWayData;


                NodeArray[i, j] = tile;

                if (i == 0 && j == 0)
                {
                    //StartNode = tile.node;
                    tile.TargetTile();
                    wayTileList.Add(tile);
                    //OpenList.Add(tile);
                }

                //if (i == map_X - 1 && j == map_Y - 1)
                //{
                //    TargetNode = tile;
                //    //wayTileList.Add(tile);
                //    //OpenList.Add(tile);

                //    tile.TargetTile();

                //}
            }
        }
    }

    void AddTileWayData(Tile _tile)
    {
        _tile.H = map_H - (10 * OpenList.Count);
        _tile.G = _tile.G + (10 * OpenList.Count);
        //OpenList.Add(_tile);

        //OpenList.Add(_tile.node);

        PathFinding();
    }


    //길로 만들어둔 타일을 이전 데이터로 리셋 시키기 위한 기능입니다 
    public void ReSetWayTile()
    {
        //만약에 카운터가 0이라면 중지 시킵니다 
        if (wayTileList.Count == 0)
        {
            return;
        }


        Tile tile = wayTileList[0];
        tile.ResetData();
        List<Tile> tiles = new List<Tile>();

        wayTileList.Remove(tile);
        OpenList.Remove(tile);
    }

}
