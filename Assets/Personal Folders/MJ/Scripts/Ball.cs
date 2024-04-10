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
        // 적이 아닌 대상과 부딪히면
        if (!collision.CompareTag("Enemy")) return;
        // 현재 target인 적이 아닐 때
        if (collision.transform != target) return;
        // 적 체력을 damage만큼 감소

        // 발사체 오브젝트 삭제
        Destroy(gameObject);

    }
}
