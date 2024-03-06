using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFactory
{
    public Monster CreateMonster(int monsterId) 
    { 
        switch (monsterId)
        {
            case 0: return new TestMonster();

            default: return null;
        }
    }
}
