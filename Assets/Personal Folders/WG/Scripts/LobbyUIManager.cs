using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIManager : MonoBehaviour
{
    [SerializeField]
    private Button inventoryPopupBtn;
    [SerializeField]
    private Button shopPopupBtn;
    private PlayerInventoryUI playerInventoryUI;

    private void Start()
    {
        inventoryPopupBtn.onClick.RemoveAllListeners();
        inventoryPopupBtn.onClick.AddListener(() => OnClickInventoryBtn());

        shopPopupBtn.onClick.RemoveAllListeners();
        shopPopupBtn.onClick.AddListener(() => OnClickShopBtn());
    }

    // InventoryPopupBtn 클릭 시 실행할 함수
    private void OnClickInventoryBtn()
    {
        if(playerInventoryUI == null)
        {
            playerInventoryUI = GetComponentInChildren<PlayerInventoryUI>(true);
            playerInventoryUI.Init();
        }

        playerInventoryUI.gameObject.SetActive(true);
    }

    // shopBtn 클릭 시 실행
    private void OnClickShopBtn()
    {

    }
}
