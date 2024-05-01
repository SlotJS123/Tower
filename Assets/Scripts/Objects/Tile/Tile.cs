using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum TileType
{
    WayTile,
    GroundTile
}

public class Tile : MonoBehaviour, IInteractionable
{
    protected TileType tileType;

    public TileType TileType => tileType;

    private GameObject tower = null;


    public void InteractionObject()
    {
        TouchTile();
    }

    private void TouchTile()
    {
        switch (tileType)
        {
            case TileType.GroundTile:
                Debug.Log("click GroundTile");
                this.gameObject.SetActive(false);
                GameManager.Instance.TowerManager.JS_TowerInstallation(this);
                //다음 타워 설치에 대한 값을 실행여부를 확인하기 위한 이벤트입니다 
                MapManager mapManager = GameManager.Instance.MapManager;

                if(mapManager.TowerInstallCounter != 5)
                {
                    mapManager.TowerInstallCounter = mapManager.TowerInstallCounter + 1;
                    mapManager.OnNextTower.Invoke();
                }

               
                return;

            case TileType.WayTile:
                Debug.Log("click WayTile");
                return;
        }
    }
}
