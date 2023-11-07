using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;
    public int damage;
    public int maxHP;
    public int curHP;

    public bool TakeDamage(int damage)
    {
        curHP -= damage;

        if(curHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetStats()
    {
        curHP = maxHP;
    }

    public void Heal(int amount)
    {
        curHP += amount;
        if(curHP > maxHP)
        {
            curHP = maxHP;
        }
    }
}
