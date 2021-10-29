using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private Transform target;
    private Vector3 targetLastPos;
    private float speed = 10;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        targetLastPos = new Vector3(target.position.x, target.position.y, target.position.z);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetLastPos, speed * Time.deltaTime);
        transform.LookAt(targetLastPos);

        if(transform.position == targetLastPos)
        {
            rb.velocity = Vector3.zero;
            anim.Play("Explode");
            Destroy(this.gameObject, 0.5f);
        }
    }
}
