using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System;

public class TowerSpawn : MonoBehaviour
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

    public void SetEnemySpawn(EnemySpawn enemySpawn)
    {
        this.enemySpawn = enemySpawn;
    }

    //게임을 시작 할 때 가장 먼저 타워에 대한 기본 데이터 json 파일을 받아옵니다 
    // todo js_20240510:: 이부분은 앞으로 게임 씬에서 받아오는게 아니라 로비 씬에서 미리 전달 받은 데이터로 진행하게 할 예정입니다 
    public void GetStartJsonData()
    {

        //  todo js_20240510::일단 게임 진행을 위해서 유지 시키지만 로비씬 작업에 완료가 된다면 없애 예정입니다 
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


        Debug.Log("선택이 되어는지 확인을 합니다");

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


    public void TowerCountUp(Tower _tower)
    {
        Tower towerData = towers.Find(x => x == _tower);
        towerData.TowerAddCount();
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

                clone.GetComponent<Tower>().Setup(GameManager.Instance.EnemySpawner);
            }
        }

    }



    //JS 맵에서 사용 할 수 있게 수정한 코드입니다 
    public void JS_TowerInstallation(Tile _tile) // 타워설치
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

        if (setUpAddTowerData == null)
        {
            Debug.LogError("현재 할당 받은 타워 데이터가 없습니다!!!");
            return;
        }

        Vector2 mPos = Input.mousePosition;
        Vector3 target = _tile.transform.localPosition;
        Tower selectedTower = setUpAddTowerData;
        GameManager.Instance.TowerManager.TowerCountUp(selectedTower);



        GameObject clone = Instantiate(selectedTower.prefab, target, Quaternion.identity);
        clone.GetComponent<Tower>().Setup(GameManager.Instance.EnemySpawner);
        GameManager.Instance.TowerManager.RemoveAddTowerData();

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

//json에 사용 될 클래스 정의입니다 
//나중에 대한 데이터 수정이 필요합니다 
[Serializable]
public class TowerData
{
    [JsonProperty("a1")]
    public int towerId;
    [JsonProperty("a2")]
    public int a2;
    [JsonProperty("a3")]
    public TowerNameData towerNameList;
    [JsonProperty("a4")]
    public int a4;
    [JsonProperty("a5")]
    public TowerNameData towerNumber;
    //[JsonProperty("a6")]
    public int a6;
    //[JsonProperty("a7")]
    public int a7;
    //[JsonProperty("a8")]
    public int a8;
    //[JsonProperty("a9")]
    public int a9;
    //[JsonProperty("a10")]
    public int a10;
    //[JsonProperty("a11")]
    public int a11;
    //[JsonProperty("a1")]
    public int a12;
    //public int a13;

    ////[JsonProperty("a2")]
    //public int a2;
    ////[JsonProperty("a3")]
    //public TowerNameData towerNameList;
    ////[JsonProperty("a4")]
    //public int a4;
    ////[JsonProperty("a5")]
    //public TowerNameData a5;
    ////[JsonProperty("a6")]
    //public int a6;
    ////[JsonProperty("a7")]
    //public int a7;
    ////[JsonProperty("a8")]
    //public int a8;
    ////[JsonProperty("a9")]
    //public int a9;
    ////[JsonProperty("a10")]
    //public int a10;
    ////[JsonProperty("a11")]
    //public int a11;
    ////[JsonProperty("a1")]
    //public int a12;
    ////public int a13;
}
[Serializable]

public class TowerNameData
{
    public string kr;
    public string en;
    public string jp;
}
[Serializable]
public class RootTower
{
    public int version;
    public List<TowerData> items;
}





//json에 사용 될 클래스 정의입니다 
[Serializable]
public class TrapData
{
    [JsonProperty("a1")]
    public int towerId;
    [JsonProperty("a2")]
    public int a2;
    [JsonProperty("a3")]
    public TowerNameData towerNameList;
    //[JsonProperty("a4")]
    public int a4;
    //[JsonProperty("a5")]
    public TowerNameData a5;
    //[JsonProperty("a6")]
    public int a6;
    //[JsonProperty("a7")]
    public int a7;
    //[JsonProperty("a8")]
    public int a8;
    //[JsonProperty("a9")]
    public int a9;
    //[JsonProperty("a10")]
    public int a10;
    //[JsonProperty("a11")]
    public int a11;
    //[JsonProperty("a1")]
    public int a12;
    //public int a13;
}
[Serializable]

public class TraprNameData
{
    public string kr;
    public string en;
    public string jp;
}
[Serializable]
public class RootTrap
{
    public int version;
    public List<TrapData> items;
}