using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonRoaming : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float changeInterval = 2f;
    public Collider2D roamingArea;

    private Vector2 moveDirection;
    private float changeTimer;

    void Start()
    {
        moveDirection = Random.insideUnitCircle.normalized;
        changeTimer = changeInterval;
    }

    void Update()
    {
        changeTimer -= Time.deltaTime;

        if (changeTimer <= 0f)
        {
            ChangeDirection();
            changeTimer = changeInterval;
        }

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    void ChangeDirection()
    {
        float angle = Random.Range(0f, 360f);
        moveDirection = Quaternion.Euler(0, 0, angle) * Vector2.up;
    }
}