using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private Transform[] wayPoints; // 현재 스테이지의 이동 경로
    private Wave currentwave;
    private List<Enemy> enemyList; // 현재 맵에 존재하는 모든 적의 정보

    public List<Enemy> EnemyList => enemyList;

    private void Awake()
    {
        // 적 리스트 메모리 할당
        enemyList = new List<Enemy>();
        // 적 생성 코루틴 함수 호출
        //StartCoroutine("Spawn");
    }

    public void StartWave(Wave wave)
    {
        currentwave= wave;
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn() 
    {
        // 현재 웨이브에서 생성한 적 수
        int enemyCount = 0;

        int k = 0;
        // 현재 웨이브에서 생성되어야 하는 적의 수만큼 적을 생성
        while (k < currentwave.enemyPrefabCount.Length)
        {
            for(int i = 0; enemyCount < currentwave.enemyPrefabCount[k]; i++)
            {               
                // 적 오브젝트 생성
                GameObject clone = Instantiate(currentwave.enemyPrefabs[k]);
                // 방금 생성된 적의 enemy 컴포넌트
                Enemy enemy = clone.GetComponent<Enemy>();
                // 리스트에 방금 생성된 적 정보 저장
                enemyList.Add(enemy);
                // wayPoint 정보를 매개변수로 Setup를 호출
                enemy.Setup(this,wayPoints);
                // 현재 웨이브 에서 생성한 적의 숫자 +1
                enemyCount++;
                // spawnTime 시간 동안 대기
                yield return new WaitForSeconds(currentwave.spawnTime);
            }
            k++;
            if(currentwave.enemyPrefabCount.Length > k)
            {
                enemyCount = 0;
            }
            else
            {
                // 현재 웨이브 종료
                break;
            }
        }
    }
}
