using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionPopup : MonoBehaviour
{
    //버튼 프리팹입니다 
    //재활용하기 위해서 미리 할당해줍니다 
    public PopupUseButton popupUseButton;
 
    public GameObject canvas;
    int count = 3;
    //웨이브가 끝났을 때 호출하는 방식으로 해야합니다 
    // 그래야 어디서든 사용 할 수 있는 상태로 쓸 수 있습니다 

    private void Update()
    {

    }

    public void Info()
    {
        Open();
        Setect();
    }

    //선택지를 세팅하기 위한 함수인데 뭐라고 해야할지 잘 모르겠네 
    void Setect()
    {
        if(canvas.transform.childCount > 0)
        {
            for (int i = 0; i < canvas.transform.childCount; i++)
            {
                //이부분은 나중에 재활용으로 쓸 수 있게 수정해야합니다 
                Destroy(canvas.transform.GetChild(i).gameObject);
            }
            
        }

        //여기서 타워매니저랑 함정 매니저한테 데이터를 받아 오고 랜덤으로 돌릴 때 마다 데이터를 참조하여 사용하는 방식으로 쓰면 될거 같습니다 

        //순서를 정하자
        //1 랜덤으로 타입을 정합니다
        //2 그 타입에 맞는 데이터를 가지고 다시 한 번 랜덤으로 돌립니다
        //2-1 단 타워인 경우 동일한 타워가 5개 이상 설치가 되어 있다면 그 타워는 빼고 진행해야합니다 
        //그러면 차라리 위에서 데이터를 받아올 때 부터 제외를 하여 최적화를 시키는 편이 좋지 않을까 싶은데
        //가독성을 생각하면 아래에서 조건으로 제외하는게 편할거 같습니다 
        //3 생성을 해줍니다 

        for (int i = 0; i < count; i++)
        {
            PopupUseButton _popupUseButton = Instantiate(popupUseButton);

            //버튼이 클릭되었다면 윈도우창도 닫아주는 기능을 이벤트로 연결시켜줍니다 
            _popupUseButton.OnClickEventHander += CLose;
            SelcetionButtonType selcetionButtonType = (SelcetionButtonType)Random.Range(0, 3);

            switch (selcetionButtonType)
            {
                case SelcetionButtonType.TOWER:

                    bool state = true;
                    while(state)
                    {
                        int towerIndxe = Random.Range(0, GameManager.Instance.towerManager.GetTowerList().Count);
                        List<Tower> towers = GameManager.Instance.towerManager.GetTowerList();
                        var tower = towers.Find(x => x.GetTowerCount() < 2);

                        if(tower != null)
                        {
                            Tower towerData = towers[towerIndxe];

                            if (towerData.GetTowerCount() == 2)
                            {
                                Debug.LogError("기능 테스트를 위한 로그입니다");
                            }
                            else
                            {

                                _popupUseButton.SetupTowerButtonData(towerData);
                                state = false;

                            }
                        }
                        else
                        {
                            state = false;
                            Debug.LogError("모든 타워가 설치가 되었다는 조건을 고려하기 위한 임시 예외처리입니다");
                        }
                      

                    }
                 
                    break;
                case SelcetionButtonType.TOWERLEVELUP:
                    _popupUseButton.SetupTowerLevelUpButtonData();
                    break;
                case SelcetionButtonType.TRAP:
                    //int trapIndxe = Random.Range(0, GameManager.Instance.towerSpawn.towerJson.items.Count);
                    //var tarpData = GameManager.Instance.towerSpawn.towerJson.items[trapIndxe];
                    //_popupUseButton.SetupTrapButtonData(tarpData);
                    _popupUseButton.SetupTrapButtonData();

                    break;
            }
            _popupUseButton.transform.SetParent(canvas.transform, false); 
        }




    }


    public void Open()
    {
        this.gameObject.SetActive(true);
    }
    public void CLose()
    {
        this.gameObject.SetActive(false);

    }
}
