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

    public IEnumerator PlayerTackle(Unit playerUnit, Unit enemyUnit, BattleState state, BattleHUD enemyHUD, Text dialogueText)
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

    public IEnumerator PlayerKick(Unit playerUnit, Unit enemyUnit, BattleState state, BattleHUD enemyHUD, Text dialogueText)
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage + 10);

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

    public IEnumerator Run(BattleState state, Text dialogueText)
    {
        float runChance = 0.2f; // 20% chance to run, adjust as needed

        // Regardless of the escape attempt's outcome, end the player's turn
        state = BattleState.ENEMYTURN;

        if (Random.value < runChance)
        {
            state = BattleState.ESCAPED;
            battleSystem.BattleEnd();

            dialogueText.text = "You escaped!";

            yield return new WaitForSeconds(2f);

            battleSystem.battleSystem.SetActive(false);
        }
        else
        {
            dialogueText.text = "You tried to escape, but failed!";

            yield return new WaitForSeconds(2f);

            yield return battleSystem.StartCoroutine(battleSystem.EnemyTurn());
        }
    }
}