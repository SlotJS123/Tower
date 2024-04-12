using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public void GoalEnemy(EnemySpawn enemySpawn, Enemy enemy)
    {
        enemySpawn.EnemyList.Remove(enemy);
        Destroy(enemy.gameObject);

        GameManager.Instance.PlayerStatus.DecreaseHP();
    }
}
