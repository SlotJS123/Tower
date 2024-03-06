using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawn : MonoBehaviour
{
    [SerializeField]
    private List<Tower> towers; // 타워 목록
    [SerializeField]
    private float[] probabilities; // 각 타워의 뽑기 확률
    [SerializeField]
    private EnemySpawn enemySpawn; // 현재 맵에 존재하는 적 리스트 정보


    // 원본 코드입니다 
    public void TowerInstallation() // 타워설치
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            // 확률 누적 계산
            float totalProbability = 0;
            float[] accumulatedProbabilities = new float[probabilities.Length];
            for (int i = 0; i < probabilities.Length; i++)
            {
                totalProbability += probabilities[i];
                accumulatedProbabilities[i] = totalProbability;
            }

            // 랜덤 값 생성
            float randomValue = Random.Range(0, totalProbability);

            // 뽑힌 타워 인덱스 찾기
            int selectedIndex = -1;
            for (int i = 0; i < accumulatedProbabilities.Length; i++)
            {
                if (randomValue <= accumulatedProbabilities[i])
                {
                    selectedIndex = i;
                    break;
                }
            }

            // 뽑힌 타워 생성
            if(selectedIndex != -1)
            {
                Vector2 mPos = Input.mousePosition;
                Vector2 target = Camera.main.ScreenToWorldPoint(mPos);
                Tower selectedTower = towers[selectedIndex];
                GameObject clone = Instantiate(selectedTower.prefab , target, Quaternion.identity);

                clone.GetComponent<Tower>().Setup(GameManager.Instance.monsterManager);
            }
        }

    }



    //JS 맵에서 사용 할 수 있게 수정한 코드입니다 
    public void JS_TowerInstallation(Tile _tile) // 타워설치
    {
        // 확률 누적 계산
        float totalProbability = 0;
        float[] accumulatedProbabilities = new float[probabilities.Length];
        for (int i = 0; i < probabilities.Length; i++)
        {
            totalProbability += probabilities[i];
            accumulatedProbabilities[i] = totalProbability;
        }

        // 랜덤 값 생성
        float randomValue = Random.Range(0, totalProbability);

        // 뽑힌 타워 인덱스 찾기
        int selectedIndex = -1;
        for (int i = 0; i < accumulatedProbabilities.Length; i++)
        {
            if (randomValue <= accumulatedProbabilities[i])
            {
                selectedIndex = i;
                break;
            }
        }

        // 뽑힌 타워 생성
        if (selectedIndex != -1)
        {
            Vector2 mPos = Input.mousePosition;
            Vector2 target = _tile.transform.position;
            Tower selectedTower = towers[selectedIndex];
            GameObject clone = Instantiate(selectedTower.prefab, target, Quaternion.identity);

            clone.GetComponent<Tower>().Setup(GameManager.Instance.monsterManager);
        }

    }

    private void Update()
    {
        //TowerInstallation();
    }
}
