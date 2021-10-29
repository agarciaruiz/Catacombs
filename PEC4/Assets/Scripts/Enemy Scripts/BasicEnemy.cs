using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    freeze,
    died
}

public class BasicEnemy : MonoBehaviour
{
    [Header("Enemy Variables")]
    public EnemyState currentState;

    public string enemyName;
    public int baseAttack;
    public float speed;

    public void Knockback(Rigidbody2D rb, float knockTime)
    {
        StartCoroutine(Knock(rb, knockTime));
    }

    IEnumerator Knock(Rigidbody2D rb, float knockTime)
    {
        if (rb != null)
        {
            yield return new WaitForSeconds(knockTime);

            if(rb.bodyType != RigidbodyType2D.Static)
                rb.velocity = Vector2.zero;
            if(currentState != EnemyState.died)
                currentState = EnemyState.idle;
        }
    }
}
