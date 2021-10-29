using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : BasicEnemy
{
    [Header("Skeleton Variables")]
    public Rigidbody2D rb;
    public Animator anim;
    [SerializeField] private Vector2 homePos;

    [Header("Target Variables")]
    public Transform target;
    public float chaseRadius = 5;
    public float attackRadius = 0.3f;


    // Start is called before the first frame update
    void Start()
    {  
        ChangeState(EnemyState.idle);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    public virtual void CheckDistance()
    {
        if (rb.bodyType != RigidbodyType2D.Static)
        {
            if (Vector3.Distance(target.position, transform.position) <= chaseRadius
                && Vector3.Distance(target.position, transform.position) > attackRadius)
            {
                if (currentState == EnemyState.idle || currentState == EnemyState.walk
                    && currentState != EnemyState.freeze)
                {
                    Vector3 temp = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                    ChangeAnim(temp - transform.position);
                    rb.MovePosition(temp);
                    ChangeState(EnemyState.walk);
                    anim.SetBool("Running", true);
                }
            }
            else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, homePos, speed * Time.deltaTime);
                ChangeAnim(temp - transform.position);
                rb.MovePosition(temp);
                ChangeState(EnemyState.walk);
                anim.SetBool("Running", true);

                if (transform.position == (Vector3)homePos)
                {
                    anim.SetBool("Running", false);
                }
            }
            else if (Vector3.Distance(target.position, transform.position) <= chaseRadius
                       && Vector3.Distance(target.position, transform.position) <= attackRadius)
            {
                if (currentState == EnemyState.idle || currentState == EnemyState.walk
                    && currentState != EnemyState.freeze)
                {
                    StartCoroutine(Attack());
                }
            }
        }
    }

    private void SetAnimFloat(Vector2 setVec2)
    {
        anim.SetFloat("MoveX", setVec2.x);
        anim.SetFloat("MoveY", setVec2.y);
    }

    public void ChangeAnim(Vector2 dir)
    {
        if(Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            if(dir.x > 0)
                SetAnimFloat(Vector2.right); 
            else if(dir.x < 0)
                SetAnimFloat(Vector2.left);

        } else if(Mathf.Abs(dir.x) < Mathf.Abs(dir.y))
        {
            if (dir.y > 0)
                SetAnimFloat(Vector2.up);
            else if (dir.y < 0)
                SetAnimFloat(Vector2.down);

        }
    }

    private void ChangeState(EnemyState state)
    {
        if(currentState != state)
        {
            currentState = state;
        }
    }

    public IEnumerator Attack()
    {
        currentState = EnemyState.attack;
        anim.SetBool("Attacking", true);

        yield return new WaitForSeconds(1f);

        currentState = EnemyState.walk;
        anim.SetBool("Attacking", false);

    }
}
