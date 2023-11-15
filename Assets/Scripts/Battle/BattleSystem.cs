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

    private PlayerActions playerActions;
    private EnemyActions enemyActions;

    void Start()
    {
        playerActions = GetComponent<PlayerActions>();
        enemyActions = GetComponent<EnemyActions>();
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

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        state = BattleState.WAITING;

        StartCoroutine(playerActions.PlayerAttack(playerUnit, enemyUnit, state, enemyHUD, dialogueText));
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        state = BattleState.WAITING;

        StartCoroutine(playerActions.PlayerHeal(playerUnit, state, playerHUD, dialogueText));
    }

    public IEnumerator EnemyTurn()
    {
        yield return StartCoroutine(enemyActions.EnemyTurn(playerUnit, enemyUnit, state, playerHUD, enemyHUD, dialogueText));
    }

    public void PlayerTurn()
    {
        dialogueText.text = "What's your move?";
    }

    public void BattleEnd()
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