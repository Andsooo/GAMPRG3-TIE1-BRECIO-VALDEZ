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

    public GameObject battleSystem;
    public bool isInBattle = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        encounterTimer += Time.deltaTime;

        if (encounterTimer >= baseEncounterInterval)
        {
            if (IsInsideEncounterArea())
            {
                Debug.Log("Inside the encounter area!");

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
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void InitiateRandomBattle()
    {
        if (isInBattle == false)
        {
            Debug.Log("Pokemon encountered!");
            battleSystem.SetActive(true);
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
