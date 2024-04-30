using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIManager : MonoBehaviour
{
    [SerializeField]
    private Button inventoryPopupBtn;
    [SerializeField]
    private Button shopPopupBtn;
    [SerializeField]
    private GameObject stageLockUI;
    [SerializeField]
    private GameObject stageSelectUI;

    private PlayerInventoryUI playerInventoryUI;
    private ShopUI shopUI;
    private StageSwipeController swipeController;

    private void Start()
    {
        swipeController = GetComponentInChildren<StageSwipeController>();
        InitStageButtons();

        inventoryPopupBtn.onClick.RemoveAllListeners();
        inventoryPopupBtn.onClick.AddListener(() => OnClickInventoryBtn());

        shopPopupBtn.onClick.RemoveAllListeners();
        shopPopupBtn.onClick.AddListener(() => OnClickShopBtn());
    }

    // InventoryPopupBtn 클릭 시 실행할 함수
    private void OnClickInventoryBtn()
    {
        if (playerInventoryUI == null)
        {
            playerInventoryUI = GetComponentInChildren<PlayerInventoryUI>(true);
            playerInventoryUI.Init();
        }

        playerInventoryUI.gameObject.SetActive(true);
    }

    // shopBtn 클릭 시 실행
    private void OnClickShopBtn()
    {
        if (shopUI == null)
        {
            shopUI = GetComponentInChildren<ShopUI>(true);
            shopUI.Init();
        }

        shopUI.gameObject.SetActive(true);
    }

    private void InitStageButtons()
    {
        Stage[] uiArray = swipeController.StageLayoutParant.GetComponentsInChildren<Stage>();

        for (int i = 0; i < swipeController.StageLayoutParant.childCount; i++)
        {
            int index = i;
            uiArray[i].StageButton.onClick.RemoveAllListeners();
            uiArray[i].StageButton.onClick.AddListener(() => OnClickStageBtn(index));
        }
    }

    private void OnClickStageBtn(int index)
    {
        print(index);
        Stage stage = swipeController.StageLayoutParant.GetChild(index).GetComponent<Stage>();
        if (stage.IsClear)
        {
            // 선택 스테이지를 현재 스테이지로 설정
            ShowStageSelectUI();
        }
        else
        {
            // stage가 잠겨있을 경우
            StartCoroutine(ShowStageLockUI());
        }
    }

    private void ShowStageSelectUI()
    {
        stageSelectUI.SetActive(true);
    }

    private IEnumerator ShowStageLockUI()
    {
        stageLockUI.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        stageLockUI.SetActive(false);
    }
}
