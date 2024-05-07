using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class MapManager : MonoBehaviour
{
    //타워 설치 준비가 되었다는 신호를 보내기 위한 bool값입니다 
    public bool towerSetState;

    public Action OnNextTower;


    int towerInstallationCounter = 1;

    public int TowerInstallCounter
    {
        get
        {
            return towerInstallationCounter;
        }
        set { towerInstallationCounter = value; }
    }

}
