using System.Collections;
using UnityEngine;
using System;

public enum TileState
{
    On,  //타워 설치가 가능한 상태
    Off  //타워 설치가 불가능한 상태 
}
public class Tile : MonoBehaviour
{
    public TileState state;
    protected bool canBuildTower;
    public bool CanBuildTower => canBuildTower;

    public void TouchTile()
    {

    }
}
