using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField]
    private Button exitBtn;

    public void Init()
    {
        exitBtn.onClick.RemoveAllListeners();
        exitBtn.onClick.AddListener(() => this.gameObject.SetActive(false));
    }
}
