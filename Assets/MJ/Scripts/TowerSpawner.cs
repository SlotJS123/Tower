using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner
{
    public void CreateTower(int towerID)
    {
        string towerName = "";
        switch (towerID)
        {
            case 1:
                towerName = "1";
                break;
        }

        if (string.IsNullOrEmpty(towerName))
            return;

        InstantiateTower(towerName);
    }

    private void InstantiateTower(string towerName)
    {
        string towerPass = $"Prefabs/Towers/{towerName}";
        Object tower = Resources.Load(towerPass);

        if (tower != null)
            Object.Instantiate(tower);
        else
            Debug.Log($"{towerPass} 는 잘못된 경로입니다");
    }

}
