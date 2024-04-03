using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager
{
    private int playerHP;
    private int playerGold;
    private float playTime;

    public int PlayerHP => playerHP;
    public int PlayerGold => playerGold;
    public float PlayTime => playTime;

    public void SetPlayerStatus(int _playerHP, int _playerGold)
    {
        playerHP = _playerHP;
        playerGold = _playerGold;
        playTime = 0;
    }

    public void IncreaseHP()
    {
        playerHP++;
    }

    public void IncreaseHP(int _count)
    {
        playerHP += _count;
    }

    public void DecreaseHP()
    {
        playerHP--;
    }

    public void DecreaseHP(int _count)
    {
        playerHP -= _count;
    }

    public void IncreaseGold(int _gold)
    {
        playerGold += _gold;
    }

    public void DecreaseGold(int _gold)
    {
        playerGold -= _gold;
    }

    // Use Update Function
    public void AddPlayerTime()
    {
        playTime += Time.deltaTime;
    }
}
