using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
    public Slider healthSlider;

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = "Lvl " + unit.unitLevel;
        healthSlider.maxValue = unit.maxHP;
        healthSlider.value = unit.curHP;
    }

    public void SetHealth(int health)
    {
        healthSlider.value = health;
    }
}
