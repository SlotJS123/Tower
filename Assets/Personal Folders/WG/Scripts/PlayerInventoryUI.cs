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
        for (int i = 0; i < 5; i++)
        {
            Button item = Utills.Utility.InstantiateObject($"{itemPrefabPath}item", itemContent.transform).GetComponent<Button>();
            item.onClick.AddListener(() => itemDetailUI.gameObject.SetActive(true));
        }
    }
}
