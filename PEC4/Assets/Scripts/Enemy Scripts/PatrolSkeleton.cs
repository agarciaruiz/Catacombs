using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolSkeleton : Skeleton
{
    public Transform[] path;
    public int currentPoint;
    public Transform currentObjective;

    public float deltaDistance;

    public override void CheckDistance()
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
                    anim.SetBool("Running", true);
                }
            }
            else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
            {
                if (Vector3.Distance(transform.position, path[currentPoint].position) > deltaDistance)
                {
                    Vector3 temp = Vector3.MoveTowards(transform.position, path[currentPoint].position, speed * Time.deltaTime);
                    ChangeAnim(temp - transform.position);
                    rb.MovePosition(temp);
                    anim.SetBool("Running", true);
                }
                else
                    ChangeGoal();
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

    private void ChangeGoal()
    {
        if(currentPoint == path.Length - 1)
        {
            currentPoint = 0;
            currentObjective = path[0];
        }
        else
        {
            currentPoint++;
            currentObjective = path[currentPoint];
        }
    }
}
