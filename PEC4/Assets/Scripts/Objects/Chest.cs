using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : Interactable
{
    [Header("Contents")]
    public Item content;
    public Inventory playerInventory;
    public BoolValue yetOpened;
    public bool isOpen;

    [Header("Player Health")]
    public FloatValue heartContainer;
    public FloatValue playerHealth;
    public Signal healthSignal;

   [Header("Signals and dialogue")]
    public Signal raiseItem;
    public GameObject dialogueBox;
    public Text dialogueText;

    [Header("Animation")]
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isOpen = yetOpened.runtimeValue;

        if (isOpen)
        {
            anim.SetBool("Opened", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && inRange)
        {
            if (!isOpen)
                OpenChest();
            else
                ChestOpened();
        }
    }

    public void OpenChest()
    {
        isOpen = !isOpen;
        SoundManager.soundManager.PlaySfx("chest");
        anim.SetBool("Opened", true);
        yetOpened.runtimeValue = isOpen;

        if (content.isHeartContainer)
        {
            AddExtraLife();
        }

        playerInventory.AddItem(content);
        playerInventory.currentItem = content;
        dialogueBox.SetActive(true);
        dialogueText.text = content.itemDescription;
        raiseItem.Raise();
    }

    public void ChestOpened()
    {
        inRange = false;
        dialogueBox.SetActive(false);
        raiseItem.Raise();  
    }

    public void AddExtraLife()
    {
        heartContainer.runtimeValue += 1;
        playerHealth.runtimeValue = heartContainer.runtimeValue * 2;
        healthSignal.Raise() ;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") && !collision.isTrigger && !isOpen)
        {
            inRange = true;
        }

    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger && !isOpen)
        {
            inRange = false;
        }
    }
}
