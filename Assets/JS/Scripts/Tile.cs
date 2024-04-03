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

    private void Start()
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
        }

        // 타워가 이미 설치되어 있을 때
        // 현재는 임시로 타워 바로 제거
        Debug.Log("타워 제거");
        Destroy(tower.gameObject);
        tower = null;
    }
}
