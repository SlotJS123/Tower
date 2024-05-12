using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class TowerDeck : MonoBehaviour
{
    //특정 타워를 덱에 추가 할 때 사용하는 이벤트 함수입니다 
    public Action OnAddTowerDack;

    //이미 덱에 있는 타워를 제거하기 위해 동작하는 이벤트 함수입니다 
    public Action OnRemoveTowerDack;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //이전에 게임 개발할때 사용하던 매니저를 가져와야할거 같은데 
    public void Info()
    {
        //이 함수가 호출이 되는 곳은 이벤토리의 타워 태크에서 실행이 됩니다 
        //여기에 특정 오브젝트의 이벤트에 연결을 해줘야합니다 
        
    }
}
