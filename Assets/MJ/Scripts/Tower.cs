using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum WeaponState { SearchTarget = 0, AttToTarget }

public class Tower : MonoBehaviour
{
    [SerializeField]
    private string name; // Ÿ���̸�
    public GameObject prefab; // Ÿ�� ������
    [SerializeField]
    private GameObject  projectileObj; // �߻�ü ������
    [SerializeField]
    private Transform   point; // �߻�ü ������ġ
    [SerializeField]
    private float attRate; // ���� �ӵ�
    [SerializeField]
    private float attRange; // ���� ����
    [SerializeField]
    private float attDamage; // ���ݷ�
    private Transform   attTarget = null; // ���� ���

    private MonsterManager enemySpawn; // �����ϴ� �� ���� ȹ���

    private WeaponState weapon = WeaponState.SearchTarget; // Ÿ�� ������ ����


    public void Setup(MonsterManager enemySpawn)
    {
        this.enemySpawn = enemySpawn;

        // ���� ���¸� WeaponState.SearchTarget���� ����
        ChangeState(WeaponState.SearchTarget);
    }

    //���� �ڵ� 
    //public void ChangeState(WeaponState state)
    //{
    //    // ������ ������̴� ���� ����
    //    StopCoroutine(weapon.ToString());
    //    // ���� ����
    //    weapon = state;

    //    Debug.Log("���� �����ϴ°� �������� Ȯ���� �ϱ� ���� �α��Դϴ� " + weapon.ToString());
    //    // ���ο� ���� ���
    //    StartCoroutine(weapon.ToString());
    //}


    public void ChangeState(WeaponState state)
    {
        // ������ ������̴� ���� ����
        StopCoroutine(weapon.ToString());
        // ���� ����
        weapon = state;

        Debug.Log("���� �����ϴ°� �������� Ȯ���� �ϱ� ���� �α��Դϴ� " + weapon.ToString());
        // ���ο� ���� ���
        StartCoroutine(weapon.ToString());
    }
    IEnumerator SearchTarget()
    {
        while (true)
        {
            // ���� ������ �ִ� ���� ã�� ���� ���� �Ÿ��� �ִ��� ũ�� ����
            float minDistance = Mathf.Infinity;
            // EnemySpawn�� EnemyList�� �ִ� ���� �ʿ� �����ϴ� ��� �� �˻�
            for(int i = 0; i < enemySpawn.RetunnMonsterList().Count; i++)
            {
                float distance = Vector3.Distance(enemySpawn.RetunnMonsterList()[i].transform.position, transform.position);
                // ���� �˻����� ������ �Ÿ��� ���� �������� �ְ�, ������� �˻��� ������ �Ÿ��� ������
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
            // target�� �ִ��� �˻�
            if( attTarget == null)
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }
            // target�� ���� ���� �ȿ� �ִ��� �˻�
            float distance = Vector3.Distance(attTarget.position, transform.position);
            if(distance > attRange)
            {
                attTarget = null;
                ChangeState(WeaponState.SearchTarget);
                break;
            }
            // attRate �ð���ŭ ���
            yield return new WaitForSeconds(attRate);

            // �߻�ü ����
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

        // ���� ��Ÿ��� �׸��� ���� ���� ������Ʈ�� ��ġ�� �߽����� �ϴ� ���� �׸�
        Gizmos.DrawWireSphere(transform.position, attRange);
    }
}
