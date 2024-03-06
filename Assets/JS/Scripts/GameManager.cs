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





    //  ������ �̵� ��� �־��ִ� �Լ�
    public void SetMapData(List<Tile> _route)
    {
        route = _route;
    }

    //  ���۹�ư Ŭ���� ����, ����� �������� ������Ʈ�� ������ ����
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
