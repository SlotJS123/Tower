using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryUI : MonoBehaviour
{
    [SerializeField]
    private Button closeInventoryBtn;

    private GameObject itemContent;
    private ItemDetailUI itemDetailUI;
    private readonly string itemPrefabPath = "Prefabs/UI/Inventory/";

    public void Init()
    {
        itemContent = GetComponentInChildren<GridLayoutGroup>().gameObject;
        itemDetailUI = GetComponentInChildren<ItemDetailUI>(true);

        itemDetailUI.Init();

        closeInventoryBtn.onClick.RemoveAllListeners();
        closeInventoryBtn.onClick.AddListener(() => this.gameObject.SetActive(false));

        // 임시로 item 오브젝트 5개 생성
        // js 임시로 생성하는 부분을 일시적으로 중지 시켰습니다 
        // 타워 데이터를 받아와서 리스트로 만들어야하는 부분에 대해서 테스트를 진행하기 위해서입니다 
        //for (int i = 0; i < 5; i++)
        //{
        //    Button item = Utills.Utility.InstantiateObject($"{itemPrefabPath}item", itemContent.transform).GetComponent<Button>();
        //    item.onClick.AddListener(() => itemDetailUI.gameObject.SetActive(true));
        //}
    }
}
