using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LifeManager : MonoBehaviour
{

    private int startLifeCount = 5;

    public Action OnGameOverEnvetHander;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamageLife()
    {
        //만약에 이 함수가 동작을 하면 라이프를 1씩 깍습니다 
        startLifeCount -= 1;

        if(startLifeCount == 0)
        {
            //만약에 라이프가 0이 된다면 gameManager에서 게임 오버 이벤트를 실행합니다 

            //이벤트를 실행합니다 
            OnGameOverEnvetHander?.Invoke();
        }
    }
}
