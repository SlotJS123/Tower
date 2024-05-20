using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.0f; // 이동속도
    [SerializeField]
    private Vector3 moveDir = Vector3.zero;
    [SerializeField]
    private float damage;
    private Transform target;

    public void SetUp(Transform target, float damage)
    {
        // 타워가 설정해준 target
        this.target = target;
        // 타워가 설정해준 공격력
        this.damage = damage;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        // target이 존재하면
        if(target != null) 
        {
            // 발사체를 target의 위치로 이동
            Vector3 dir = (target.position - transform.position).normalized;
            MoveTo(dir);
        }
        // target이 없으면
        else
        {
            // 발사체 오브젝트 삭제
            Destroy(gameObject);
        }
    }

    public void MoveTo(Vector3 dir)
    {
        moveDir = dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("충돌을 감지합니다 ");

        // 적이 아닌 대상과 부딪히면
        if (!collision.CompareTag("Enemy"))
        {
            Debug.Log("적이 아닙니다");
            return;
        }
        // 현재 target인 적이 아닐 때
        if (collision.transform != target) return;
        // 적 체력을 damage만큼 감소

        Debug.Log("오브젝트를 삭제합니다 ");
        // 발사체 오브젝트 삭제
        Destroy(gameObject);

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("충돌을 감지합니다 ");

        // 적이 아닌 대상과 부딪히면
        if (!other.CompareTag("Enemy"))
        {
            Debug.Log("적이 아닙니다");
            return;
        }
        // 현재 target인 적이 아닐 때
        if (other.transform != target) return;
        // 적 체력을 damage만큼 감소

        Enemy enemy = other.GetComponent<Enemy>();
        Debug.Log("전달하는 데미지를 확인합니다 ");
        enemy.TakeDamage(damage);


        Debug.Log("오브젝트를 삭제합니다 ");
        // 발사체 오브젝트 삭제
        Destroy(gameObject);
    }

}
