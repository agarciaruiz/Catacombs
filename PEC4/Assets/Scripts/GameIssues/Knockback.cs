using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float pushForce = 1.5f;
    [SerializeField] private float knockTime = 0.4f;
    [SerializeField] private string otherTag;
    [SerializeField] private int enemyLayer;
    //public float damage = 1;

    private void Start()
    {
        enemyLayer = LayerMask.NameToLayer("Enemy");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(otherTag) && collision.isTrigger)
        {
            Rigidbody2D hitRb = collision.GetComponentInParent<Rigidbody2D>();

            if(hitRb != null)
            {
                Vector2 dif = hitRb.transform.position - transform.position;
                dif = dif.normalized * pushForce;
                hitRb.AddForce(dif, ForceMode2D.Impulse);

                if (collision.gameObject.CompareTag("Enemy") && collision.isTrigger)
                {
                    if (collision.gameObject.layer == enemyLayer)
                    {
                        hitRb.GetComponent<BasicEnemy>().currentState = EnemyState.freeze;
                        collision.GetComponentInParent<BasicEnemy>().Knockback(hitRb, knockTime);
                    }
                }

                if (collision.gameObject.CompareTag("Player") && collision.isTrigger)
                {
                    if (collision.GetComponentInParent<PlayerMovement>().currentState != PlayerState.freeze)
                    {
                        hitRb.GetComponent<PlayerMovement>().currentState = PlayerState.freeze;
                        collision.GetComponentInParent<PlayerMovement>().Knockback(knockTime);
                    }
                }

            }
        }
    }
}
