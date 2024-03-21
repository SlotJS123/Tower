using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public MapManager mapManager;
    public TowerSpawn towerSpawn;
    public MonsterManager monsterManager;
    public Tile selectTile = null;

    private List<Tile> route;

    // 차후 삭제 예정 변수
    public GameObject testMonstor;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;    
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mapManager.MapMaking();

        monsterManager.SetMonsterObject(testMonstor);

        mapManager.startButton.onClick.AddListener(OnTouchStartButton);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            OnTouchStartButton();
        }
    }


    public void MakeTower()
    {
        towerSpawn.JS_TowerInstallation(selectTile);
    }


    //  시작전 이동 경로 넣어주는 함수
    public void SetMapData(List<Tile> _route)
    {
        route = _route;
    }

    //  시작버튼 클릭시 실행, 현재는 수동으로 오브젝트에 연결한 상태
    public void OnTouchStartButton()
    {
        if (route == null)
        {
            Debug.Log("Route is null");
            return;
        }

        monsterManager.SetRoute(route);
        StartCoroutine(monsterManager.MonsterSpawnCoroutine(20));
    }
}
