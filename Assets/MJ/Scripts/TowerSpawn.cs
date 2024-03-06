using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawn : MonoBehaviour
{
    [SerializeField]
    private List<Tower> towers; // Ÿ�� ���
    [SerializeField]
    private float[] probabilities; // �� Ÿ���� �̱� Ȯ��
    [SerializeField]
    private EnemySpawn enemySpawn; // ���� �ʿ� �����ϴ� �� ����Ʈ ����


    // ���� �ڵ��Դϴ� 
    public void TowerInstallation() // Ÿ����ġ
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            // Ȯ�� ���� ���
            float totalProbability = 0;
            float[] accumulatedProbabilities = new float[probabilities.Length];
            for (int i = 0; i < probabilities.Length; i++)
            {
                totalProbability += probabilities[i];
                accumulatedProbabilities[i] = totalProbability;
            }

            // ���� �� ����
            float randomValue = Random.Range(0, totalProbability);

            // ���� Ÿ�� �ε��� ã��
            int selectedIndex = -1;
            for (int i = 0; i < accumulatedProbabilities.Length; i++)
            {
                if (randomValue <= accumulatedProbabilities[i])
                {
                    selectedIndex = i;
                    break;
                }
            }

            // ���� Ÿ�� ����
            if(selectedIndex != -1)
            {
                Vector2 mPos = Input.mousePosition;
                Vector2 target = Camera.main.ScreenToWorldPoint(mPos);
                Tower selectedTower = towers[selectedIndex];
                GameObject clone = Instantiate(selectedTower.prefab , target, Quaternion.identity);

                clone.GetComponent<Tower>().Setup(GameManager.Instance.monsterManager);
            }
        }

    }



    //JS �ʿ��� ��� �� �� �ְ� ������ �ڵ��Դϴ� 
    public void JS_TowerInstallation(Tile _tile) // Ÿ����ġ
    {
        // Ȯ�� ���� ���
        float totalProbability = 0;
        float[] accumulatedProbabilities = new float[probabilities.Length];
        for (int i = 0; i < probabilities.Length; i++)
        {
            totalProbability += probabilities[i];
            accumulatedProbabilities[i] = totalProbability;
        }

        // ���� �� ����
        float randomValue = Random.Range(0, totalProbability);

        // ���� Ÿ�� �ε��� ã��
        int selectedIndex = -1;
        for (int i = 0; i < accumulatedProbabilities.Length; i++)
        {
            if (randomValue <= accumulatedProbabilities[i])
            {
                selectedIndex = i;
                break;
            }
        }

        // ���� Ÿ�� ����
        if (selectedIndex != -1)
        {
            Vector2 mPos = Input.mousePosition;
            Vector2 target = _tile.transform.position;
            Tower selectedTower = towers[selectedIndex];
            GameObject clone = Instantiate(selectedTower.prefab, target, Quaternion.identity);

            clone.GetComponent<Tower>().Setup(GameManager.Instance.monsterManager);
        }

    }

    private void Update()
    {
        //TowerInstallation();
    }
}
