using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField]
    private bool isClear = false;
    private int stageID;

    public bool IsClear => isClear;
}
