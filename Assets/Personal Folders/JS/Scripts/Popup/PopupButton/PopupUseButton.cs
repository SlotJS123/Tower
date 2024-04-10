using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum SelcetionButtonType
{
    TOWER,
    TOWERLEVELUP,
    TRAP
}


public class PopupUseButton : MonoBehaviour
{
    public SelcetionButtonType selcetionButtonType;
    public Button popupUseButtonUI;
    public TextMeshProUGUI title;
    public Image thumbnail;
    public Action OnClickEventHander;

    SelcetionButtonInfo buttonInfo = new SelcetionButtonInfo();


    // Start is called before the first frame update
    void Start()
    {
        //버튼이 생성되고나면 가장 먼저 사용하게 될 이벤트를 할당해줍니다 
        popupUseButtonUI.onClick.AddListener(ClickEvent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetupTowerButtonData(Tower _towerData)
    {
        //_towerData.TowerAddCount();
        title.text = "Tower";
        buttonInfo.towerData = _towerData;
        thumbnail.sprite = _towerData.thumbnail.sprite;
    }
    public void SetupTrapButtonData(TrapData _trapData = null)
    {
        title.text = "Trap";
        buttonInfo.trapData = _trapData;    
    }

    public void SetupTowerLevelUpButtonData(TrapData _trapData = null)
    {
        title.text = "TowerLevelUp";
        //buttonInfo.trapData = _trapData;
    }
    
    //버튼이 클릭된다면 실행을 하게될 함수입니다 
    public void ClickEvent()
    {
        //일단 할당 받은 이벤트를 실행합니다 
        OnClickEventHander?.Invoke();

        switch (selcetionButtonType)
        {
            case SelcetionButtonType.TOWER:
                GameManager.Instance.towerManager.SetUpTower(buttonInfo.towerData);
                break;
            case SelcetionButtonType.TOWERLEVELUP:
                break;
            case SelcetionButtonType.TRAP:
                break;
        }
        //여기에서 버튼에게 할당되어있는 타입에 따라 동작하는 행동이 달라집니다 
        //타워냐 타워 강화냐 함정이냐에 따라서 기능이 달라집니다 
    }
    //public void SetupButtonData()
    //{

    //}
}


//왜 이렇게 만들었지? 아 생각이 정리가 안되네 ㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋ
//정말 어렵다 


//이 클래스는 버튼에서 사용 될 예정의 버튼입니다 
//사용 될 데이터는 타워 / 타워 강화 / 함정 등으로 일단 이 3가지입니다 
[Serializable]
public class SelcetionButtonInfo
{
    //생성되는 ui에 사용될 타입을 지정하기 위한 변수입니다 


    //이 아래에서는 이제 함정에 대한 클래스 
    public Tower towerData;
    //타워에 대한 클래스 
    public TrapData trapData;
    //가장 큰 문제는 타워를 강화하기 위한 클래스를 어떻게 만들어야 하냐인데 
    //이거는 좀 생각을 해봐야 할거 같습니다 


    //public void 

}
