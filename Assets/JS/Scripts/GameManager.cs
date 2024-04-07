using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public MapManager mapManager;
    public TowerSpawn towerSpawn;

    private Tower selectTower = null;

    private TowerSpawner towerSpawner = new TowerSpawner();
    private PlayerStatManager playerStatus = new PlayerStatManager();
    private WaveManager waveManager;

    public TowerSpawner TowerSpawner => towerSpawner;
    public PlayerStatManager PlayerStatus => playerStatus;
    public WaveManager WaveManager => waveManager;

    public MonsterManager monsterManager = new MonsterManager();

    public GameObject testMonstor;

    //public spawnTile 
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;    
        }
    }

    private void Start()
    {
        // 임시로 플레이어 초기 스탯값 하드코딩 
        playerStatus.SetPlayerStatus(3, 0);

        waveManager = GetComponentInChildren<WaveManager>();
        // mapManager.MapMaking();
        // towerSpawn.GetStartJsonData();
        // monsterManager.SetMonsterObject(testMonstor);
    }

    // 웨이브 끝나고 타워 선택 시 아래의 함수로 Tower Type을 넣어 주세요
    public void SetSelectTower(Tower tower)
    {
        selectTower = tower;
    }

    public GameObject InstantiateTower()
    {
        return towerSpawner.CreateTower(selectTower.name);
    }

    public bool IsCreateTower()
    {
        if (selectTower == null)
            return false;

        return true;
    }
}
