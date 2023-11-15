using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActions : MonoBehaviour
{
    public BattleSystem battleSystem;

    void Start()
    {
        battleSystem = GetComponent<BattleSystem>();
    }

    public IEnumerator PlayerAttack(Unit playerUnit, Unit enemyUnit, BattleState state, BattleHUD enemyHUD, Text dialogueText)
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHealth(enemyUnit.curHP);
        dialogueText.text = "Enemy hit!";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            battleSystem.state = BattleState.WON;
            battleSystem.BattleEnd();

            yield return new WaitForSeconds(2f);

            battleSystem.battleSystem.SetActive(false);
        }
        else
        {
            battleSystem.state = BattleState.ENEMYTURN;
            StartCoroutine(battleSystem.EnemyTurn());
        }
    }

    public IEnumerator PlayerHeal(Unit playerUnit, BattleState state, BattleHUD playerHUD, Text dialogueText)
    {
        int healAmount = Random.Range(5, 10);

        playerUnit.Heal(healAmount);

        playerHUD.SetHealth(playerUnit.curHP);

        dialogueText.text = playerUnit.unitName + " heals for " + healAmount + " HP!";

        yield return new WaitForSeconds(2f);

        battleSystem.state = BattleState.ENEMYTURN;
        StartCoroutine(battleSystem.EnemyTurn());
    }
}