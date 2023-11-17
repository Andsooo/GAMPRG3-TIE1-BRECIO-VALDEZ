using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokeMart : MonoBehaviour
{
    public GameObject popupScreen;

    private bool playerInside = false;

    public PlayerMovement playerMovement;

    public PlayerInventory playerInventory;

    public Text dialogueText;

    void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }

    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.Space))
        {
            OpenPopupScreen();
        }
    }

    void OpenPopupScreen()
    {
        popupScreen.SetActive(true);
        playerMovement.isInBattle = true;

        dialogueText.text = "Good day! You currently have " + playerInventory.healPotions + " heal potion. How many more do you need?";
    }

    public void On1xButton()
    {
        playerInventory.AddHealPotions(1);
        dialogueText.text = "Good day! You currently have " + playerInventory.healPotions + " heal potion. How many more do you need?";
    }

    public void On5xButton()
    {
        playerInventory.AddHealPotions(5);
        dialogueText.text = "Good day! You currently have " + playerInventory.healPotions + " heal potion. How many more do you need?";
    }

    public void On10xButton()
    {
        playerInventory.AddHealPotions(10);
        dialogueText.text = "Good day! You currently have " + playerInventory.healPotions + " heal potion. How many more do you need?";
    }

    public void OnExitButton()
    {
        playerMovement.isInBattle = false;
        popupScreen.SetActive(false);
    }
}
