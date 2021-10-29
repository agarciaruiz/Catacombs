using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DoorType
{
    key,
    boss
}


public class Door : Interactable
{
    [Header("Door Variables")]
    public DoorType thisDoorType;
    public BoolValue yetOpened;
    public bool open;
    public Inventory playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physicCollider;

    [Header("Dialogue Variables")]
    public GameObject dialogueBox;
    public Text dialogueText;
    public string dialogue;

    private void Start()
    {
        open = yetOpened.runtimeValue;
        if (open)
        {
            doorSprite.enabled = false;
            physicCollider.enabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (inRange && thisDoorType == DoorType.key)
            {
                if (playerInventory.numOfKeys > 0)
                {
                    dialogueBox.SetActive(false);
                    playerInventory.numOfKeys--;
                    Open();
                }
                else
                {
                    dialogueBox.SetActive(true);
                    dialogueText.text = dialogue;
                }
            }
        }
    }

    public void Open()
    {
        doorSprite.enabled = false;
        open = true;
        physicCollider.enabled = false;
        yetOpened.runtimeValue = open;
    }

    public void Close()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            inRange = true;
        }

    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            inRange = false;
            dialogueBox.SetActive(false);
        }
    }
}
