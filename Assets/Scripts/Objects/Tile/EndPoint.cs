using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private EnemySpawn enemySpawn;

    private void Start()
    {
        enemySpawn = GameManager.Instance.EnemySpawner;
    }

    public void GoalEnemy(Enemy enemy)
    {
        enemySpawn.EnemyList.Remove(enemy);
        Destroy(enemy.gameObject);

        GameManager.Instance.PlayerStatus.DecreaseHP();
    }
}
