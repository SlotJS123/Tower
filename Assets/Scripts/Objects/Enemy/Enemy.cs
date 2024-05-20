using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { ground = 0, air = 1 } // 1.지상몬스터 2. 공중몬스터 (임시)

public class Enemy : MonoBehaviour
{
    public EnemyType enemyType; // 인스펙터창에서 선택(임시)
    [SerializeField]
    private float hp = 0;// enemy체력
    [SerializeField]
    private float moveSpeed = 0.0f;
    [SerializeField]
    private Vector3 moveDir = Vector3.zero;

    private int wayPointCount; // 이동 경로 수
    private Transform[] wayPoints; // 이동 경로 정보
    private int currentIndex = 0; // 현재 목표지점 인덱스
    private EnemySpawn enemySpawn;

    public void Setup(EnemySpawn enemySpawn ,Transform[] point)
    {
        // 적 이동 경로 waypoint 정보 설정
        wayPointCount = point.Length;
        this.enemySpawn = enemySpawn;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = point;
        // 적의 위치를 첫번째 waypoint 위치로 설정
        transform.position = point[currentIndex].position;
        // 적 이동/목표지점 설정 코루틴 함수 시작
        StartCoroutine("OnMove");
    }

    private IEnumerator OnMove()
    {
        //다음 이동방향 설정
        NextMoveTo();
        while (true)
        {
            // 적의 현재위치와 목표위치의 거리가 0.04 * moveSpeed보다 작을 때 if 조건문 실행
            if(Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.04f * moveSpeed ) 
            {
                //다음 이동방향 설정
                NextMoveTo();
            }

            yield return null;
        }
    }

    private void NextMoveTo()
    {
        // 아직 이동할 wayPoint가 남아있다면
        if (currentIndex < wayPointCount - 1)
        {
            // 적의 위치를 정확하게 목표 위치로 설정
            transform.position = wayPoints[currentIndex].position;
            // 이동 방향 설정 > 다음 목표지점
            currentIndex++;
            Vector3 dir = (wayPoints[currentIndex].position - transform.position).normalized;
            MoveTo(dir);
        }
        // 현재 위치가 마지막 wayPoint이면
        else
        {
            EndPoint endPoint = wayPoints[currentIndex].GetComponent<EndPoint>();
            endPoint.GoalEnemy(enemySpawn, this);
        }
    }

    public void MoveTo(Vector3 dir)
    {
        moveDir = dir;
    }

    public void TakeDamage(float damage) // 자신이 공격받는 함수
    {
        Debug.Log("피격을 확인합니다 전달 받은 데미지는   :   " + damage);

        hp -= damage;
        if(hp < 0)
        {

            DestroyGameobject();

           
        }
    }

    public void DestroyGameobject()
    {
        


        Debug.Log("Enemy 오브젝트를 삭제합니다 ");
        enemySpawn.EnemyList.Remove(this);
        int count = GameManager.Instance.EnemySpawner.FieldEnemycount();

        if (count == 0)
        {
            GameManager.Instance.OnNextWawveventHander.Invoke();
            // 다음 웨이브 준비를 한다 
        }
        Destroy(gameObject);
    }


    void FixedUpdate()
    {
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
}
