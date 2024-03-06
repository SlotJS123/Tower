using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyObj; // �� ������

    private List<Enemy> enemyList; // ���� �ʿ� �����ϴ� ��� ���� ����

    public List<Enemy> EnemyList => enemyList;

    private void Awake()
    {
        // �� ����Ʈ �޸� �Ҵ�
        enemyList = new List<Enemy>();
        // �� ���� �ڷ�ƾ �Լ� ȣ��
        //StartCoroutine("Spawn");
    }

    IEnumerator Spawn() 
    {
        while (true)
        {
            float spawnTime = 10.0f;
            // �� ������Ʈ ����
            GameObject clone = Instantiate(enemyObj, transform);
            // ��� ������ ���� enemy ������Ʈ
            Enemy enemy = clone.GetComponent<Enemy>();
            // ����Ʈ�� ��� ������ �� ���� ����
            enemyList.Add(enemy);

            yield return new WaitForSeconds(spawnTime);
        }
    }
}
