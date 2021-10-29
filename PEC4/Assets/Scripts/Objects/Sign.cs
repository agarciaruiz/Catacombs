using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : Interactable
{
    public GameObject dialogueBox;
    public Text dialogueText;

    public string[] dialogue;
    private float typingSpeed = 0.02f;
    private int index;

    public GameObject continueButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && inRange)
        {
            if (dialogueBox.activeInHierarchy)
                dialogueBox.SetActive(false);
            else
            {
                dialogueBox.SetActive(true);
                StartCoroutine(Type());
            }
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

    private IEnumerator Type()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            if (dialogueText.text == dialogue[index])
            {
                continueButton.SetActive(true);
            }
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);

        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = " ";
            StartCoroutine(Type());
        }
        else
            dialogueText.text = " ";
    }
}
