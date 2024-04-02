using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Wave
{
    public float spawnTime; //현재 웨이브 적 생성 주기
   // public int maxEnemyCount; // 현재 웨이브 적 수
    public GameObject[] enemyPrefabs; // 현재 웨이브 적 종류
    public int[] enemyPrefabCount;   //현재 웨이브 적 개별 등장 숫자
}

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    private Wave[] waves;            //현재 스테이지의 모든 웨이브 정보
    [SerializeField]
    private EnemySpawn enemySpawn;      //EnemySpawner 저장
    [SerializeField]
    private int currentWaveIndex = -1;      //현재 웨이브
    [SerializeField]
    private float interval;                 //웨이브간 시간 간격

    private float time = 0;

    // Start is called before the first frame update
    public void StartWave()
    {
        // 현재 맵에 적이 없고, wave가 남아있으면
        if( enemySpawn.EnemyList.Count == 0 && currentWaveIndex < waves.Length-1 ) 
        {
            // 인덱스의 시작이 -1이기 때문에 웨이브 인덱스 증가를 제일 먼저함
            currentWaveIndex++;
            // EnemySpawn의 StartWave 함수 호출, 현재 웨이브 정보 제공
            enemySpawn.StartWave(waves[currentWaveIndex]);
        }
    }
}
