using System.Collections;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public enum TileType
{
    GroundTile,
    WayTile
}
public class Tile : MonoBehaviour
{
    protected TileType tileType;
    private EventTrigger clickTrigger;
    private GameObject tower = null;

    public Tile(bool _isWall, int _x, int _y) { isWall = _isWall; x = _x; y = _y; }

    public bool isWall;
    public Tile ParentNode;

    public GameObject tile_1;
    public GameObject tile_2;


    // G : 시작으로부터 이동했던 거리, H : |가로|+|세로| 장애물 무시하여 목표까지의 거리, F : G + H
    public int x, y, G, H;
    public int F { get { return G + H; } }
    public TileState state;

    public Action<Tile> OnTileClick;    

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void Init()
    {
        clickTrigger = GetComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;

        entry.callback.AddListener((eventData) => { TouchTile(); });
        clickTrigger.triggers.Add(entry);
    }

    private void TouchTile()
    {
        switch (tileType)
        {
            case TileType.GroundTile:
                Debug.Log("click GroundTile");
                CheckCanBuildTower();
                return;

            case TileType.WayTile:
                Debug.Log("click WayTile");
                return;
        }
    }

    private void CheckCanBuildTower()
    {
        // 타워 설치가 가능할 때
        if (tower == null)
        {
            if (!GameManager.Instance.IsCreateTower())
            {
                Debug.Log("설치 가능한 타워가 선택되지 않았습니다");
                return;
            }

            tower = GameManager.Instance.InstantiateTower();
            tower.transform.position = this.transform.position;
            return;

        //     state = TileState.Off;
        //     isWall = false;
        // 
        //     tile_1.gameObject.SetActive(false);
        //     tile_2.gameObject.SetActive(true);
        // 
        //     OnTileClick?.Invoke(this);
        // }
        // else
        // {
        //     Debug.Log("여기서 이제 타워를 생성해주는 로직을 생성해서 적용을 시켜야합니다 ");
        // 
        //     GameManager.Instance.towerSpawn.JS_TowerInstallation(this, GameManager.Instance.towerSpawn.GetUpTowerData());
        }

        // 타워가 이미 설치되어 있을 때
        // 현재는 임시로 타워 바로 제거
        Debug.Log("타워 제거");
        Destroy(tower.gameObject);
        tower = null;
    }
}
