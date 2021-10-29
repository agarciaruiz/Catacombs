using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    idle,
    walk,
    attack,
    interact,
    freeze,
    died
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public static PlayerMovement instance;

    public VectorValue startingPos;
    public Inventory playerInventory;
    public SpriteRenderer itemSprite;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector3 direction;
    public float speed = 6;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.soundManager.PlayMusic("level_ost");
        currentState = PlayerState.walk;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetFloat("MoveX", 0);
        anim.SetFloat("MoveY", -1);
        transform.position = startingPos.initValue;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState == PlayerState.interact)
        {
            return;
        }

        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        if(Input.GetButtonDown("Attack") && currentState != PlayerState.attack
            && currentState != PlayerState.freeze)
        {
            StartCoroutine(AttackCo());
        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
            UpdateAnimAndMove();
    }

    private IEnumerator AttackCo()
    {
        anim.SetBool("Attacking", true);
        currentState = PlayerState.attack;
        SoundManager.soundManager.PlaySfx("sword");
        yield return null;

        anim.SetBool("Attacking", false);
        yield return new WaitForSeconds(.3f);

        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
    }

    public void RaiseItem()
    {
        if (playerInventory.currentItem != null)
        {
            if (currentState != PlayerState.interact)
            {
                anim.SetBool("Item Received", true);
                currentState = PlayerState.interact;
                itemSprite.sprite = playerInventory.currentItem.itemSprite;
            }
            else
            {
                anim.SetBool("Item Received", false);
                currentState = PlayerState.idle;
                itemSprite.sprite = null;
                playerInventory.currentItem = null;
            }
        }
    }

    void UpdateAnimAndMove()
    {
        if (direction != Vector3.zero)
        {
            PlayerMove();
            anim.SetFloat("MoveX", direction.x);
            anim.SetFloat("MoveY", direction.y);
            anim.SetBool("Running", true);
        }
        else
            anim.SetBool("Running", false);
    }

    void PlayerMove()
    {
        direction.Normalize();
        rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
    }

    public void Knockback(float knockTime)
    {
        StartCoroutine(Knock(knockTime));
    }

    IEnumerator Knock(float knockTime)
    {
        if (rb != null)
        {
            yield return new WaitForSeconds(knockTime);
            if(rb.bodyType != RigidbodyType2D.Static)
                rb.velocity = Vector2.zero;
            currentState = PlayerState.idle;
        }
    }
}
