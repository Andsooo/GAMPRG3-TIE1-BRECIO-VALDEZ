using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
