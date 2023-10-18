using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, WAITING, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    public GameObject playerPrefab;
    public GameObject[] enemyPrefabs;

    public Transform playerBattleArea;
    public Transform enemyBattleArea;

    Unit playerUnit;
    Unit enemyUnit;

    public Text dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public GameObject battleSystem;

    public PlayerMovement playerMovement;

    private bool isBattling = false;

    void Start()
    {

    }

    public void BattleStart()
    {
        if (!isBattling)
        {
            battleSystem.SetActive(true);

            state = BattleState.START;
            StartCoroutine(SetupBattle());

            isBattling = true;
        }
    }

    IEnumerator SetupBattle()
    {
        if (state == BattleState.START)
        {
            Debug.Log("SetupBattle");

            ResetBattle();

            GameObject playerGameObject = Instantiate(playerPrefab, playerBattleArea);
            playerUnit = playerGameObject.GetComponent<Unit>();

            GameObject currentEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            GameObject enemyGameObject = Instantiate(currentEnemy, enemyBattleArea);
            enemyUnit = enemyGameObject.GetComponent<Unit>();

            dialogueText.text = "A wild " + enemyUnit.unitName + " appeared!";

            playerHUD.SetHUD(playerUnit);
            enemyHUD.SetHUD(enemyUnit);

            yield return new WaitForSeconds(2f);

            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    IEnumerator PlayerAttack()
    {

        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHealth(enemyUnit.curHP);
        dialogueText.text = "Enemy hit!";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            BattleEnd();

            yield return new WaitForSeconds(2f);

            battleSystem.SetActive(false);
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    void PlayerTurn()
    {
        dialogueText.text = "What's your move?";
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        state = BattleState.WAITING;

        StartCoroutine(PlayerAttack());
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHealth(playerUnit.curHP);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            BattleEnd();

            yield return new WaitForSeconds(2f);
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void BattleEnd()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You win!";
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You lose!";
        }

        StartCoroutine(BattleEndScreen(2f));

        playerMovement.isInBattle = false;
        state = BattleState.START;

        isBattling = false;
    }

    private IEnumerator BattleEndScreen(float delay)
    {
        yield return new WaitForSeconds(delay);

        enemyUnit.ResetStats();

        yield return new WaitForSeconds(1f);

        if (!isBattling)
        {
            battleSystem.SetActive(false);
        }

        StartCoroutine(SetupBattle());
    }

    void ResetBattle()
    {
        if (enemyUnit != null)
        {
            Destroy(enemyUnit.gameObject);
            enemyUnit = null;
        }
    }
}