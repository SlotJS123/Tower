using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum WeaponState { SearchTarget = 0, AttToTarget }

public class Tower : MonoBehaviour
{
    [SerializeField]
    private int towerId; // 타워이름
    [SerializeField]
    private string towerName; // 타워이름
    public GameObject prefab; // 타워 프리팹
    [SerializeField]
    private GameObject projectileObj; // 발사체 프리팹
    [SerializeField]
    private Transform point; // 발사체 생성위치
    [SerializeField]
    private float attRate; // 공격 속도
    [SerializeField]
    private float attRange; // 공격 범위
    [SerializeField]
    private float attDamage; // 공격력

    private int towerCost; // 타워 가격

    public int towerCount = 0;

    private EnemySpawn enemySpawn; // 존재하는 적 정보 획득용
    private WeaponState weapon = WeaponState.SearchTarget; // 타워 무기의 상태
    public EventTrigger clickTrigger;
    private Tile spawnTile; // 타워가 스폰된 타일 기억 (일단 임시)


    public List<Transform> enemiesInRange = new List<Transform>(); // 타워 공격 범위 안에 있는 적의 목록

    private Transform currentTarget; // 현재 타겟으로 지정된 적
    //public Image thumbnail;
    public SpriteRenderer thumbnail;
    //현재 타워에 대한 json 데이터를 가지고 있습니다 
    public TowerData rootTowerData;

    [SerializeField]
    private Sprite mainImage; // 대표 이미지입니다 
    private void Start()
    {

        Debug.LogError("타워에 대한 기본데이터 값이 없어서 추가해줍니다 ");
        rootTowerData = GameManager.Instance.TowerManager.towerJson.items.Find(x => x.towerId == towerId);

        clickTrigger = GetComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        // 아래 DestroyTower부분 차후 ObjectPull 구현시 반환하게 수정할 예정
        entry.callback.AddListener((eventData) => { UIManager.Instance.OnClickTower(this, towerCost); });

        enemySpawn = GameManager.Instance.EnemySpawner;
        //GameManager.Instance.EaveManager.wa

        //StartCoroutine(EnemySecrh());
        ChangeState(WeaponState.SearchTarget);

        //clickTrigger.triggers.Add(entry);
    }

    //public void Setup(MonsterManager enemySpawn)
    //{
    //    this.enemySpawn = enemySpawn;
    //    // 최초 상태를 WeaponState.SearchTarget으로 설정
    //    ChangeState(WeaponState.SearchTarget);
    //}




    public void Setup(EnemySpawn enemySpawn)
    {
        this.enemySpawn = enemySpawn;

        //StartCoroutine(EnemySecrh());

        // 최초 상태를 WeaponState.SearchTarget으로 설정
        ChangeState(WeaponState.SearchTarget);
    }

    //이 함수는 단순하게 타워 카운트를 증가 시키기 위한 함수입니다 
    public void TowerAddCount()
    {
        
        Debug.Log("타워 카운터를 증가시킵니다");



        towerCount++;
    }

    //이 함수는 현재 값을 전달 받은 타워가 몇개가 설치되어 있는지 확인을 하기 위한 함수입니다 
    public int GetTowerCount()
    {
        return towerCount;
    }

    //대표 이미지를 전달하기 위한 함수입니다 
    public Sprite GetMainSprite()
    {
        return mainImage;
    }

    public int GettowerID()
    {
        return towerId;
    }

    IEnumerator EnemySecrh()
    {
      

        while(true)
        {
            if(currentTarget != null)
            {
                yield return new WaitForSeconds(attRate);

                // 발사체 생성
                Debug.Log("공격합니다");
                SpawnProjectileObj();
            }
            else
            {
                Debug.Log("타겟이 없습니다");
                yield return null;
            }
        }

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Enemy")) // 적인 경우에만 처리
    //    {
    //        enemiesInRange.Add(other.transform); // 적을 리스트에 추가
    //        SetTarget(); // 새로운 타겟 설정
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Enemy")) // 적인 경우에만 처리
    //    {
    //        enemiesInRange.Remove(other.transform); // 적을 리스트에서 제거
    //        if (other.transform == currentTarget) // 벗어나는 적이 현재 타겟인 경우
    //        {
    //            SetTarget(); // 새로운 타겟 설정
    //        }
    //    }
    //}

    private void SetTarget()
    {
        if (enemiesInRange.Count > 0) // 공격 범위 안에 적이 있는 경우
        {
            float closestDistance = Mathf.Infinity; // 가장 가까운 적과의 거리
            Transform closestEnemy = null; // 가장 가까운 적

            foreach (Transform enemy in enemiesInRange) // 모든 적에 대해 반복
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.position); // 타워와 적 사이의 거리 계산
                if (distanceToEnemy < closestDistance) // 현재 적이 가장 가까운 적보다 더 가까운 경우
                {
                    closestDistance = distanceToEnemy; // 거리 업데이트
                    closestEnemy = enemy; // 가장 가까운 적 업데이트
                }
            }

            currentTarget = closestEnemy; // 현재 타겟으로 가장 가까운 적 설정
            Debug.Log("New target: " + currentTarget.gameObject.name);
            // 여기에 새로운 타겟을 설정한 후의 동작을 추가할 수 있습니다.
        }
        else // 공격 범위 안에 적이 없는 경우
        {
            currentTarget = null; // 현재 타겟 없음
            Debug.Log("No target in range.");
            // 여기에 타겟이 없는 경우의 동작을 추가할 수 있습니다.
        }
    }




    public void SetSpawnTile(Tile _tile)
    {
        spawnTile = _tile;
    }

    // 타워 제거 함수
    // 차후 ObjectPull 구현시 반환하게 수정할 예정
    public void DestroyTower()
    {
        // spawnTile.SetTileState(true);
        Destroy(this.gameObject);
    }

    //원본 코드 
    //public void ChangeState(WeaponState state)
    //{
    //    // 이전에 재생중이던 상태 종료
    //    StopCoroutine(weapon.ToString());
    //    // 상태 변경
    //    weapon = state;

    //    Debug.Log("지금 동작하는게 무엇인지 확인을 하기 위한 로그입니다 " + weapon.ToString());
    //    // 새로운 상태 재생
    //    StartCoroutine(weapon.ToString());
    //}


    public void ChangeState(WeaponState state)
    {
        // 이전에 재생중이던 상태 종료
        StopCoroutine(weapon.ToString());
        // 상태 변경
        weapon = state;

        Debug.Log("지금 동작하는게 무엇인지 확인을 하기 위한 로그입니다 " + weapon.ToString());
        // 새로운 상태 재생
        StartCoroutine(weapon.ToString());

        //StartCoroutine(SearchTarget());

    }
    IEnumerator SearchTarget()
    {

        // 이 아래 for문으로 동작되는 부분을 수정해야합니다 수정이 필요한 부분입니다 
        while (true)
        {

            if(enemySpawn.EnemyList.Count != 0)
            {
                // 제일 가까이 있는 적을 찾기 위해 최초 거리를 최대한 크게 설정
                float minDistance = Mathf.Infinity;
                // EnemySpawn의 EnemyList에 있는 현재 맵에 존재하는 모든 적 검사
                for (int i = 0; i < enemySpawn.EnemyList.Count; i++)
                {
                    float distance = Vector3.Distance(enemySpawn.EnemyList[i].transform.position, transform.position);
                    // 현재 검사중인 적과의 거리가 공격 범위내에 있고, 현재까지 검사한 적보다 거리가 가까우면
                    if (distance <= attRange && distance <= minDistance)
                    {
                        minDistance = distance;
                        currentTarget = enemySpawn.EnemyList[i].transform;
                    }
                    yield return null;
                }

                if (currentTarget != null)
                {
                    ChangeState(WeaponState.AttToTarget);
                }

            }


            yield return null;

        }
    }

    IEnumerator AttToTarget()
    {
        while (true)
        {
            // target이 있는지 검사
            if (currentTarget == null)
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }
            // target이 공격 범위 안에 있는지 검사
            float distance = Vector3.Distance(currentTarget.position, transform.position);
            if (distance > attRange)
            {
                currentTarget = null;
                ChangeState(WeaponState.SearchTarget);
                break;
            }
            // attRate 시간만큼 대기
            yield return new WaitForSeconds(attRate);

            // 발사체 생성
            SpawnProjectileObj();
        }
    }

    void SpawnProjectileObj()
    {
        GameObject clone = Instantiate(projectileObj, point.position, Quaternion.identity);

        clone.GetComponent<Ball>().SetUp(currentTarget, rootTowerData.a7);
    }



    Color indicatorColor = Color.red;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = indicatorColor;

        // 공격 사거리를 그리기 위해 현재 오브젝트의 위치를 중심으로 하는 원을 그림
        Gizmos.DrawWireSphere(transform.position, attRange);
    }

}