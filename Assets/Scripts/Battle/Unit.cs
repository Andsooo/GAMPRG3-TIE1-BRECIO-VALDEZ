using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;
    public int minDamage;
    public int maxDamage;
    public int damage;
    public int maxHP;
    public int curHP;
    public string healDialogue;
    public PlayerInventory playerInventory;

    void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
    }

    public bool TakeDamage(int damage)
    {
        damage = Random.Range(minDamage, maxDamage);
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

    public bool HasHealPotion()
    {
        return playerInventory.healPotions > 0;
    }

    public void IncreaseHealPotion(int amount = 1)
    {
        playerInventory.healPotions += amount;
    }

    public void UseHealPotion()
    {
        if (HasHealPotion())
        {
            Heal(10);

            IncreaseHealPotion(-1);

            healDialogue = " used a heal potion! He only has " + playerInventory.healPotions + " heal potions left now.";
        }
        else
        {
            healDialogue = " has no heal potions available!";
        }
    }
}
