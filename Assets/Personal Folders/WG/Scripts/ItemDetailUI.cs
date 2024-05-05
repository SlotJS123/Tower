using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailUI : MonoBehaviour
{
    [SerializeField]
    private Button closeBtn;

    public void Init()
    {
        closeBtn.onClick.RemoveAllListeners();
        closeBtn.onClick.AddListener(() => this.gameObject.SetActive(false));
    }
}
