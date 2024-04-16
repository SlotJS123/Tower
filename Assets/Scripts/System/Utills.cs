using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Utills
{
    private static Utills utility;

    public static Utills Utility
    {
        get
        {
            if (utility == null)
                utility = new Utills();

            return utility;
        }
    }

    public void InstantiateObject(string path, Transform parent = null)
    {
        GameObject obj = Resources.Load<GameObject>(path);

        if (obj == null)
        {
            Debug.Log($"{path} 경로가 잘못되었습니다");
            return;
        }

        Object.Instantiate(obj, parent);
    }
}
