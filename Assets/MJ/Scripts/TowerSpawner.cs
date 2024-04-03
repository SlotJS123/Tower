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

        InstantiateTower(towerName);
    }

    public GameObject CreateTower(string towerName)
    {
        return InstantiateTower(towerName);
    }

    private GameObject InstantiateTower(string towerName)
    {
        if (string.IsNullOrEmpty(towerName))
        {
            Debug.Log($"{towerName} is null");
            return null;
        }

        string towerPass = $"Prefabs/Towers/{towerName}";
        Object tower = Resources.Load(towerPass);

        if (tower != null)
        {
            return (GameObject)Object.Instantiate(tower);
        }
        else
        {
            Debug.Log($"{towerPass} 는 잘못된 경로입니다");
            return null;
        }
    }

}
