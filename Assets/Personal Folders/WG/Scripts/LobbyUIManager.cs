using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIManager : MonoBehaviour
{
    [SerializeField]
    private Button inventoryPopupBtn;
    private PlayerInventoryUI playerInventoryUI;

    private void Start()
    {
        inventoryPopupBtn.onClick.RemoveAllListeners();
        inventoryPopupBtn.onClick.AddListener(() => playerInventoryUI.gameObject.SetActive(true));

        playerInventoryUI = GetComponentInChildren<PlayerInventoryUI>();
        playerInventoryUI.Init();
    }
}
