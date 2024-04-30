using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    [SerializeField]
    private bool isClear = false;
    private int stageID;
    private Button stageButton;

    public int stageIndex;

    public bool IsClear => isClear;
    public Button StageButton => stageButton;

    private void Start()
    {
        stageButton = GetComponent<Button>();
    }

}
