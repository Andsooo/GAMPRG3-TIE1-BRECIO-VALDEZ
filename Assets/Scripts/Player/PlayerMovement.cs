using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum PlayerState { ROAMING, BATTLE }

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Rigidbody2D rb;
    private Vector2 movement;

    public Tilemap encounterTileMap;
    public float baseEncounterInterval = 5f;
    public float encounterChance = 0.5f;

    private float encounterTimer = 0f;

    public BattleSystem battleSystem;
    public bool isInBattle = false;

    public Animator animator;




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator= GetComponent<Animator>();
    }


    void Update()
    {
        if (isInBattle)
        {
            return;
        }

        //Sprite Last Movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


        if(movement.x != 0 || movement.y != 0)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetBool("IsWalking", true);
        }else
        {
            animator.SetBool("IsWalking", false);

        }


        encounterTimer += Time.deltaTime;

        if (encounterTimer >= baseEncounterInterval)
        {
            if (IsInsideEncounterArea())
            {
                if (Random.Range(0f, 1f) <= encounterChance)
                {
                    InitiateRandomBattle();
                }
            }

            encounterTimer = 0f;
        }
    }

  
    void FixedUpdate()
    {
        if(!isInBattle)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
        
    }

    void InitiateRandomBattle()
    {
        if (isInBattle == false)
        {
            battleSystem.BattleStart();
            isInBattle = true;
        }
    }

    void EncounterChecker()
    {
        Vector3Int playerTilePosition = encounterTileMap.WorldToCell(transform.position);

        if (encounterTileMap.GetTile(playerTilePosition) != null)
        {
            if (Random.Range(0f, 1f) <= encounterChance)
            {
                InitiateRandomBattle();
            }
        }
    }

    bool IsInsideEncounterArea()
    {
        Vector3Int playerTilePosition = encounterTileMap.WorldToCell(transform.position);

        TileBase currentTile = encounterTileMap.GetTile(playerTilePosition);
        return currentTile != null;
    }
}