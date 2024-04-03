using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public EnemySpawn enemySpawn;

    public void GoalEnemy(Enemy enemy)
    {
        enemySpawn.EnemyList.Remove(enemy);
        Destroy(enemy.gameObject);

        GameManager.Instance.PlayerStatus.DecreaseHP();
    }
}
