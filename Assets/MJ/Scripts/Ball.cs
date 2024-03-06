using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.0f; // �̵��ӵ�
    [SerializeField]
    private Vector3 moveDir = Vector3.zero;
    [SerializeField]
    private float damage;
    private Transform target;

    public void SetUp(Transform target, float damage)
    {
        // Ÿ���� �������� target
        this.target = target;
        // Ÿ���� �������� ���ݷ�
        this.damage = damage;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        // target�� �����ϸ�
        if(target != null) 
        {
            // �߻�ü�� target�� ��ġ�� �̵�
            Vector3 dir = (target.position - transform.position).normalized;
            MoveTo(dir);
        }
        // target�� ������
        else
        {
            // �߻�ü ������Ʈ ����
            Destroy(gameObject);
        }
    }

    public void MoveTo(Vector3 dir)
    {
        moveDir = dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� �ƴ� ���� �ε�����
        if (!collision.CompareTag("Enemy")) return;
        // ���� target�� ���� �ƴ� ��
        if (collision.transform != target) return;
        // �� ü���� damage��ŭ ����

        // �߻�ü ������Ʈ ����
        Destroy(gameObject);

    }
}
