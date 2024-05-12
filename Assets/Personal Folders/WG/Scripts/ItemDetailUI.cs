using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class ItemDetailUI : MonoBehaviour
{
    [SerializeField]
    private Button closeBtn;

    public Button equipButton;

    //특정 타워를 덱에 추가 할 때 사용하는 이벤트 함수입니다 
    public Action<Tower> OnAddTowerDack;

    //이미 덱에 있는 타워를 제거하기 위해 동작하는 이벤트 함수입니다 
    public Action OnRemoveTowerDack;

    private Tower towerData;
    public void Open(Tower tower)
    {
        this.gameObject.SetActive(true);
        towerData = tower;
    }


    public void Init()
    {
        closeBtn.onClick.RemoveAllListeners();
        closeBtn.onClick.AddListener(() => this.gameObject.SetActive(false));
    }

    public void OnEquip()
    {
        OnAddTowerDack.Invoke(towerData);
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
    }
}
