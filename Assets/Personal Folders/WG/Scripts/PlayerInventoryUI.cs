using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryUI : MonoBehaviour
{
    [SerializeField]
    private Button closeInventoryBtn;

    private GameObject itemContent;
    private readonly string itemPrefabPath = "Prefabs/UI/Inventory/";

    public void Init()
    {
        itemContent = GetComponentInChildren<GridLayoutGroup>().gameObject;
        closeInventoryBtn.onClick.RemoveAllListeners();
        closeInventoryBtn.onClick.AddListener(() => this.gameObject.SetActive(false));
    }
}
