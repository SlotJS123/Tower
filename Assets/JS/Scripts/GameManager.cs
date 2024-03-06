using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public MapManager mapManager;
    public TowerSpawn towerSpawn;
    public MonsterManager monsterManager = new MonsterManager();
    private List<Tile> route;

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
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            OnTouchStartButton();
        }
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
