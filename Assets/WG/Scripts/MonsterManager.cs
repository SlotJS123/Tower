using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class MonsterManager : MonoBehaviour
{
    private GameObject monsterPrefab;
    private List<Vector3> routeList = new List<Vector3>();
    [SerializeField]
    private Vector3 startPosition;

    //JS
    [SerializeField]
    public List<Monster> monsterList = new List<Monster>();
    //  List의 배열을 Queue로 변환후, 몬스터 스폰을 위한 시작지점 지정
    public void SetRoute(List<Tile> _routeList)
    {
        for (int i = 0; i < _routeList.Count; i++)
        {
            // print($"routeList[i].transform.position + i + 번째 루트");
            routeList.Add(_routeList[i].transform.position);
        }

        startPosition = routeList[0];
        routeList.RemoveAt(0);
    }

    public void SetMonsterObject(GameObject _monster)
    {
        monsterPrefab = _monster;
    }


    public void _MonsterSpawnCoroutine()
    {

    }


    // count만큼 몬스터를 시작지점에 순차적으로 생성
    public IEnumerator MonsterSpawnCoroutine(int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject monsterObject = Instantiate(monsterPrefab, startPosition+ new Vector3(0,0,-0.1f), monsterPrefab.transform.rotation);
            monsterObject.SetActive(false);
            Monster monster = monsterObject.GetComponent<Monster>();
            monsterList.Add(monster);
        }

        for (int i = 0; i < monsterList.Count; i++)
        {
            Monster monster = monsterList[i];
            monster.gameObject.SetActive(true);
            monster.StartMoving(routeList);
            
            yield return new WaitForSeconds(1.0f);
        }
    }

    public List<Monster> ReturnMonsterList()
    {
        return monsterList;
    }
}