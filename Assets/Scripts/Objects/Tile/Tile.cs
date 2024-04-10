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
                return;

            case TileType.WayTile:
                Debug.Log("click WayTile");
                return;
        }
    }
}
