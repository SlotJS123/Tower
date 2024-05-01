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
    private WaveManager waveManager;

    public MapManager MapManager => mapManager;
    public TowerSpawn TowerManager => towerManager;
    public SelectionPopupManager SelectionPopupManager => selectionPopupManager;
    public PlayerStatManager PlayerStatus => playerStatus;
    public EnemySpawn EnemySpawner => enemySpawn;

    public WaveManager EaveManager => waveManager;


    //---------------------------------테스트 코드입니다 
    public Camera camera;
    public float rayLength = 100f; // 레이케스트 길이
    public Color gizmoColor = Color.red; // 기즈모 색상

    private void Awake()
    {
        // Make the game run as fast as possible
        //모바일 환경에서 프레임을 30에서 강제로 60으로 고정하는 코드입니다 
        Application.targetFrameRate = -1;
        // Limit the framerate to 60
        Application.targetFrameRate = 60;
        if (Instance == null)
        {
            Instance = this;
        }

        Init();
    }

    void Start()
    {
        objectSelection.Init();

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
        waveManager = parent.GetComponentInChildren<WaveManager>();
        //enemySpawn = GetComponentInChildren<EnemySpawn>();

        towerManager.SetEnemySpawn(enemySpawn);
    }


    void OnDrawGizmos()
    {
        // 기즈모 색상 설정
        Gizmos.color = gizmoColor;

        // 현재 마우스 위치에서 레이 생성
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        // 레이 길이까지 기즈모 그리기
        Gizmos.DrawRay(ray.origin, ray.direction * rayLength);
    }
}
