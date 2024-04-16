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
        inventoryPopupBtn.onClick.AddListener(() => SetInventoryUI());
    }

    // InventoryPopupBtn 클릭 시 실행할 함수
    private void SetInventoryUI()
    {
        if(playerInventoryUI == null)
        {
            playerInventoryUI = GetComponentInChildren<PlayerInventoryUI>(true);
            playerInventoryUI.Init();
        }

        playerInventoryUI.gameObject.SetActive(true);
    }
}
