using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private MapManager mapManager;
    private TowerSpawn towerManager;
    private SelectionPopupManager selectionPopupManager;

    private PlayerStatManager playerStatus = new PlayerStatManager(); // 차후 Player의 저장된 데이터를 PlayerStatManager에 넣어주는 기능 구현해야 함
    private EnemySpawn enemySpawn;
    private ObjectSelection objectSelection = new ObjectSelection();


    public MapManager MapManager => mapManager;
    public TowerSpawn TowerManager => towerManager;
    public SelectionPopupManager SelectionPopupManager => selectionPopupManager;
    public PlayerStatManager PlayerStatus => playerStatus;
    public EnemySpawn EnemySpawner => enemySpawn;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        Init();
    }

    void Start()
    {
        objectSelection.Init();

        enemySpawn.GetStartMonsterJsonData();
        enemySpawn = GetComponentInChildren<EnemySpawn>();
        //2D라면 사용을 하지만 현재 컨셉이 3D로 변경 되어 스크라이트를 만들 필요가 없습니다 
        //mapManager.MapMaking();
        towerManager.GetStartJsonData();

        selectionPopupManager.selectionPopup.StartInfo();
    }

    private void Update()
    {
        objectSelection.UpdateSelection();
    }

    private void Init()
    {
        Transform parent = transform.parent;
        mapManager = parent.GetComponentInChildren<MapManager>();
        towerManager = parent.GetComponentInChildren<TowerSpawn>();
        selectionPopupManager = parent.GetComponentInChildren<SelectionPopupManager>();
        enemySpawn = parent.GetComponentInChildren<EnemySpawn>();

        towerManager.SetEnemySpawn(enemySpawn);
    }
}
