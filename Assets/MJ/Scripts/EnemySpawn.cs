using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyObj; // 적 프리팹

    private List<Enemy> enemyList; // 현재 맵에 존재하는 모든 적의 정보

    public List<Enemy> EnemyList => enemyList;

    private void Awake()
    {
        // 적 리스트 메모리 할당
        enemyList = new List<Enemy>();
        // 적 생성 코루틴 함수 호출
        //StartCoroutine("Spawn");
    }

    IEnumerator Spawn() 
    {
        while (true)
        {
            float spawnTime = 10.0f;
            // 적 오브젝트 생성
            GameObject clone = Instantiate(enemyObj, transform);
            // 방금 생성된 적의 enemy 컴포넌트
            Enemy enemy = clone.GetComponent<Enemy>();
            // 리스트에 방금 생성된 적 정보 저장
            enemyList.Add(enemy);

            yield return new WaitForSeconds(spawnTime);
        }
    }
}
