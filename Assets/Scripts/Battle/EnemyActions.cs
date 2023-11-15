using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyActions : MonoBehaviour
{
    public BattleSystem battleSystem;

    void Start()
    {
        battleSystem = GetComponent<BattleSystem>();
    }

    public IEnumerator EnemyTurn(Unit playerUnit, Unit enemyUnit, BattleState state, BattleHUD playerHUD, BattleHUD enemyHUD, Text dialogueText)
    {
        bool isHealing = Random.Range(0, 2) == 0;

        if (isHealing)
        {
            yield return StartCoroutine(HealMove(enemyUnit, dialogueText, enemyHUD));
        }
        else
        {
            yield return StartCoroutine(AttackMove(playerUnit, enemyUnit, state, playerHUD, dialogueText));
        }

        battleSystem.state = BattleState.PLAYERTURN;
    }

    private IEnumerator AttackMove(Unit playerUnit, Unit enemyUnit, BattleState state, BattleHUD playerHUD, Text dialogueText)
    {
        dialogueText.text = enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHealth(playerUnit.curHP);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            battleSystem.state = BattleState.LOST;
            battleSystem.BattleEnd();

            yield return new WaitForSeconds(2f);
        }
        else
        {
            battleSystem.state = BattleState.PLAYERTURN;
            battleSystem.PlayerTurn();
        }
    }

    private IEnumerator HealMove(Unit enemyUnit, Text dialogueText, BattleHUD enemyHUD)
    {
        int healAmount = Random.Range(5, 25);

        enemyUnit.Heal(healAmount);

        enemyHUD.SetHealth(enemyUnit.curHP);

        dialogueText.text = enemyUnit.unitName + " heals for " + healAmount + " HP!";

        yield return new WaitForSeconds(2f);

        battleSystem.state = BattleState.PLAYERTURN;
        battleSystem.PlayerTurn();
    }
}