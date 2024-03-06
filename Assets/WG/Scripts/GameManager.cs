using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YWM
{
    // 사용 예시
    // 오브젝트에 컴포넌트로 추가하여 Scene에 배치
    public class GameManager : MonoBehaviour
    {
        private MonsterManager monsterManager = new MonsterManager();
        private List<Tile> route;

        //  시작전 이동 경로 넣어주는 함수
        public void SetMapData(List<Tile> _route)
        {
            route = _route;
        }

        //  시작버튼 클릭시 실행, 현재는 수동으로 오브젝트에 연결한 상태
        public void OnTouchStartButton()
        {
            if (route == null)
            {
                Debug.Log("Route is null");
                return;
            }

            monsterManager.SetRoute(route);
            StartCoroutine(monsterManager.MonsterSpawnCoroutine());
        }
    }

}
