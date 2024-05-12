using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerInventory : MonoBehaviour
{
    // todo js0510::일단 임시로 타워 매니저를 할당해서 사용하지만 추우헤는 GameManager 등에서 할당하여 사용하게 해야합니다 
    public TowerManager  manager;
    public ItemDetailUI itemDetailUI;
    public TowerInventoryItem prefabs;

    private GameObject itemContent;


    private readonly string itemPrefabPath = "Prefabs/UI/Inventory/";

    private void OnEnable()
    {

        //tower dack으로 추가를 하는 이벤트 함수를 연결해줘야합니다 
        itemDetailUI.OnAddTowerDack += AddTowerDack;
        Debug.Log("실행이 되는지 확인을 하기 위한로그입니다");
        TypetowerInventoryButton();
    }

    private void OnDisable()
    {
        //초기화 작업을 해줍니다 
        //만약에 다른 곳에서 동일하게 사용이 될 수도 있다 판단이 되어 이전에 연결되어 있던 이벤트를 끊어줍니다 
        itemDetailUI.OnAddTowerDack-= AddTowerDack;
    }

    //상단에 있는 타워 버튼을 클릭하면 실행하는 함수입니다 
    public void TypetowerInventoryButton()
    {
        itemContent = GetComponentInChildren<GridLayoutGroup>().gameObject;
        itemDetailUI = GetComponentInChildren<ItemDetailUI>(true);

        itemDetailUI.Init();

        List<Tower> towerLiat = manager.GetTowerList();
        //아직 초기화 부분이 제대로 준비가 되어 있지 않아서 그 부분 수정이 필요합니다 
        for (int i = 0; i < towerLiat.Count; i++)
        {
            Tower tower = towerLiat[i];

            Debug.Log("프리팹의 이름을 확인하기 위한 로그입니다 " + itemPrefabPath);

            //Button item = Utills.Utility.InstantiateObject($"{itemPrefabPath}item", itemContent.transform).GetComponent<Button>();

            TowerInventoryItem item = Instantiate(prefabs, itemContent.transform);

            item.Tower = tower;
            //일단 임시로 버튼 이미지에 스프라이트를 변경하여 할당을 해줍니다 
            item.itemButton.image.sprite = tower.GetMainSprite();

            if(tower.rootTowerData.a6 == 1)
            {
                //만약에 a6값이 1이라면 획득한 타워이고 
                item.itemButton.interactable = true;
            }
            else
            {
                //아니라면 획득하지 못한 타워이기 때문에 버튼 기능을 비
                item.itemButton.interactable = false;

            }


            item.itemButton.onClick.AddListener(() => itemDetailUI.Open(item.Tower));
        }
    }

    private void AddTowerDack(Tower tower)
    {
        Debug.Log("덱에 추가 되는지 확인을 하기 위한 로그입니다 ");

        if(tower !=null)
        {
            Debug.Log("전달 받은 데이터가 존재합니다 ");
            itemDetailUI.Close();

        }
    }

    private void Close()
    {
        this.gameObject.SetActive(false);   
    }

}
