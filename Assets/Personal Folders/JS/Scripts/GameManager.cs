using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public MapManager mapManager;
    public TowerSpawn towerManager;
    public SelectionPopupManager selectionPopupManager;
    public EnemySpawn enemySpawn;
    public PlayerStatManager PlayerStatus;

    public GameObject testMonstor;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;    
        }
    }

    void Start()
    {
        //2D라면 사용을 하지만 현재 컨셉이 3D로 변경 되어 스크라이트를 만들 필요가 없습니다 
        //mapManager.MapMaking();
        towerManager.GetStartJsonData();

        selectionPopupManager.selectionPopup.StartInfo();
    }
}
