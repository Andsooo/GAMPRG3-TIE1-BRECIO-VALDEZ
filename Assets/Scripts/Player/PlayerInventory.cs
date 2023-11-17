using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int healPotions = 1;

    public PlayerInventory(int initialHealPotions)
    {
        healPotions = initialHealPotions;
    }

    public void AddHealPotions(int amount)
    {
        healPotions += amount;
    }
}
