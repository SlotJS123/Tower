using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System;

public class TowerSpawn_Backup : MonoBehaviour
{
    [SerializeField]
    private List<Tower> towers; // 타워 목록
    [SerializeField]
    private float[] probabilities; // 각 타워의 뽑기 확률
    [SerializeField]
    private EnemySpawn enemySpawn; // 현재 맵에 존재하는 적 리스트 정보

    //파일 이름을에 확장자까지 포함해서 넣어주세요 
    public string jsonFileName;

    public List<TowerData> towerDataList;

    public RootTower towerJson;

    Tower setUpAddTowerData;

    public Action OnFiledEventHanden;

    //게임을 시작 할 때 가장 먼저 타워에 대한 기본 데이터 json 파일을 받아옵니다 
    public void GetStartJsonData()
    {

        string path = Path.Combine(Application.dataPath, jsonFileName);
        string jsonContent = File.ReadAllText(path);

        Debug.Log("json 데이터를 확인하기 위한 로그입니다 " + jsonContent);
        //string jsonContent = System.IO.File.ReadAllText(filePath);
        //towerDataList = JsonConvert.DeserializeObject<List<TowerData>>(jsonContent);
        Debug.Log("json 파일 위치를 확인하기 위한 로그입니다" + path);
        //towerDataList = JsonConvert.DeserializeObject<List<TowerData>>(jsonContent);

        towerJson = JsonConvert.DeserializeObject<RootTower>(jsonContent);

        // TowerData 리스트에 접근하여 데이터 처리
        //foreach (var towerData in towerJson.items)
        //{
        //    Debug.Log("Tower Name (KR): " + towerData.towerNameList.kr);
        //    Debug.Log("Tower Name (EN): " + towerData.towerNameList.en);
        //    Debug.Log("Tower Name (JP): " + towerData.towerNameList.jp);
        //    Debug.Log("Damage: " + towerData.a7);
        //    // 필요한 경우 다른 필드도 출력하거나 사용할 수 있습니다.
        //}
    }
    public void SetUpTower(Tower _towerData)
    {
        setUpAddTowerData = _towerData; 
    }

    public Tower GetUpTowerData()
    {
        return setUpAddTowerData;
    }

    public void RemoveAddTowerData()
    {
        setUpAddTowerData = null;
    }

    public List<Tower> GetTowerList()
    {
        return towers;
    }


    // 원본 코드입니다 
    public void TowerInstallation() // 타워설치
    {
        if (Input.GetMouseButtonDown(0))
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
            float randomValue = UnityEngine.Random.Range(0, totalProbability);

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
                Vector2 target = Camera.main.ScreenToWorldPoint(mPos);
                Tower selectedTower = towers[selectedIndex];
                GameObject clone = Instantiate(selectedTower.prefab, target, Quaternion.identity);

                clone.GetComponent<Tower>().Setup(GameManager.Instance.enemySpawn);
            }
        }

    }



    //JS 맵에서 사용 할 수 있게 수정한 코드입니다 
    public void JS_TowerInstallation(Tile _tile, Tower _tower) // 타워설치
    {
        //// 확률 누적 계산
        //float totalProbability = 0;
        //float[] accumulatedProbabilities = new float[probabilities.Length];
        //for (int i = 0; i < probabilities.Length; i++)
        //{
        //    totalProbability += probabilities[i];
        //    accumulatedProbabilities[i] = totalProbability;
        //}

        //// 랜덤 값 생성
        //float randomValue = UnityEngine.Random.Range(0, totalProbability);

        // 뽑힌 타워 인덱스 찾기
        //int selectedIndex = -1;
        //for (int i = 0; i < accumulatedProbabilities.Length; i++)
        //{
        //    if (randomValue <= accumulatedProbabilities[i])
        //    {
        //        selectedIndex = i;
        //        break;
        //    }
        //}

        //뽑힌 타워 생성
        //if (selectedIndex != -1)
        //{
        //    Vector2 mPos = Input.mousePosition;
        //    Vector2 target = _tile.transform.position;
        //    Tower selectedTower = towers[selectedIndex];
        //    GameObject clone = Instantiate(selectedTower.prefab, target, Quaternion.identity);

        //    clone.GetComponent<Tower>().Setup(GameManager.Instance.monsterManager);
        //}

        if(_tower == null)
        {
            Debug.LogError("현재 할당 받은 타워 데이터가 없습니다!!!");
            return;
        }

        Vector2 mPos = Input.mousePosition;
        Vector2 target = _tile.transform.position;
        Tower selectedTower = _tower;
        GameObject clone = Instantiate(selectedTower.prefab, target, Quaternion.identity);
        clone.GetComponent<Tower>().Setup(GameManager.Instance.enemySpawn);
        GameManager.Instance.towerManager.RemoveAddTowerData();

    }

    private void Update()
    {
        //TowerInstallation();
    }


    public TowerData GetTowerIdData(int _id)
    {
        TowerData towerData = towerJson.items.Find(x => x.towerId == _id);
        return towerData;
    }
}

