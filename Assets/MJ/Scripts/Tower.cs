using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum WeaponState { SearchTarget = 0, AttToTarget }

public class Tower : MonoBehaviour
{
    [SerializeField]
    private string name; // 타워이름
    public GameObject prefab; // 타워 프리팹
    [SerializeField]
    private GameObject  projectileObj; // 발사체 프리팹
    [SerializeField]
    private Transform   point; // 발사체 생성위치
    [SerializeField]
    private float attRate; // 공격 속도
    [SerializeField]
    private float attRange; // 공격 범위
    [SerializeField]
    private float attDamage; // 공격력
    private Transform   attTarget = null; // 공격 대상

    private MonsterManager enemySpawn; // 존재하는 적 정보 획득용

    private WeaponState weapon = WeaponState.SearchTarget; // 타워 무기의 상태


    public void Setup(MonsterManager enemySpawn)
    {
        this.enemySpawn = enemySpawn;

        // 최초 상태를 WeaponState.SearchTarget으로 설정
        ChangeState(WeaponState.SearchTarget);
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
    }
    IEnumerator SearchTarget()
    {
        while (true)
        {
            // 제일 가까이 있는 적을 찾기 위해 최초 거리를 최대한 크게 설정
            float minDistance = Mathf.Infinity;
            // EnemySpawn의 EnemyList에 있는 현재 맵에 존재하는 모든 적 검사
            for(int i = 0; i < enemySpawn.RetunnMonsterList().Count; i++)
            {
                float distance = Vector3.Distance(enemySpawn.RetunnMonsterList()[i].transform.position, transform.position);
                // 현재 검사중인 적과의 거리가 공격 범위내에 있고, 현재까지 검사한 적보다 거리가 가까우면
                if( distance <= attRange && distance <= minDistance)
                {
                    minDistance = distance;
                    attTarget = enemySpawn.RetunnMonsterList()[i].transform;
                }
                yield return null;
            }

            if( attTarget!= null ) 
            {
                ChangeState(WeaponState.AttToTarget);
            }

            yield return null;
        }
    }

    IEnumerator AttToTarget()
    {
        while(true) 
        {
            // target이 있는지 검사
            if( attTarget == null)
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }
            // target이 공격 범위 안에 있는지 검사
            float distance = Vector3.Distance(attTarget.position, transform.position);
            if(distance > attRange)
            {
                attTarget = null;
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

        clone.GetComponent<Ball>().SetUp(attTarget, attDamage);
    }

    Color indicatorColor = Color.red;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = indicatorColor;

        // 공격 사거리를 그리기 위해 현재 오브젝트의 위치를 중심으로 하는 원을 그림
        Gizmos.DrawWireSphere(transform.position, attRange);
    }
}
