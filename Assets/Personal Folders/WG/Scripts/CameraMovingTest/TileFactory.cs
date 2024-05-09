using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFactory
{
    public string GetTilePath(int tileIndex)
    {
        string tileName = "";
        switch (tileIndex)
        {
            case 0:
                tileName = "GroundTile";
                break ;
            case 1:
                tileName = "WayTile";
                break;
        }

        return $"Prefabs/Tile/{tileName}";
    }
}
