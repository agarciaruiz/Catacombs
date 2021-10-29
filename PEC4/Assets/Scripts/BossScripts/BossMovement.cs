using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossState
{
    home,
    disappear,
    appear,
    attack,
    shoot,
    died
}

public class BossMovement : MonoBehaviour
{
    public BossState currentState;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private Vector2 homePos;
    private Vector2 appearPos;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bossUI;

    [SerializeField] private Transform target;
    float radius = 1f;
    bool isAppearing = false;
    bool isMeleeAttack = false;
    bool isShooting = false;
    bool isAttacking = false;
    [SerializeField] private float idleInterval;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        currentState = BossState.home;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        timer = Time.time;
    }

    private void Update()
    {
        switch (currentState)
        {
            case BossState.home:
                HandleStateHome();
                break;
            case BossState.appear:
                HandleStateAppear();
                break;
            case BossState.disappear:
                HandleStateDisappear();
                break;
            case BossState.attack:
                HandleStateAttack();
                break;
            case BossState.shoot:
                HandleStateShoot();
                break;
            case BossState.died:
                HandleDeath();
                break;
            default:
                break;
        }
    }

    private void HandleStateHome()
    {
        if (timer + idleInterval < Time.time)
        {
            currentState = BossState.disappear;  
        } 
    }

    private void HandleStateDisappear()
    {
        anim.SetTrigger("Disappear");
        SoundManager.soundManager.PlaySfx("boss_disappear");
    }

    public void DisappearStateFinished()
    {
        currentState = BossState.appear;
        if (appearPos != homePos)
            transform.position = (Vector2)target.position + appearPos.normalized * radius + appearPos;
        else
            transform.position = homePos;
    }

    private void HandleStateAppear()
    {
        if (!isAppearing)
        {
            anim.SetTrigger("Appear");
            SoundManager.soundManager.PlaySfx("boss_appear");
            isAppearing = true;
        }
    }

    public void AppearStateFinished()
    {
        currentState = BossState.disappear;
        isAppearing = false;
        if (!isMeleeAttack)
        {
            currentState = BossState.attack;
            appearPos = homePos;
            isMeleeAttack = true;
        }
        else
        {
            currentState = BossState.shoot;
            appearPos = Random.insideUnitCircle * radius;
            ChangeAnim(-appearPos);
            isMeleeAttack = false;
        }
    }

    private void HandleStateAttack()
    {
        if (!isAttacking)
        {
            anim.SetTrigger("Attack");
            SoundManager.soundManager.PlaySfx("boss_attack");
            Invoke(nameof(AttackStateFinished), 2.4f);
            isAttacking = true;
        }
    }

    private void AttackStateFinished()
    {
        currentState = BossState.home;
        isAttacking = false;
        timer = Time.time;
    }

    private void HandleStateShoot()
    {
        if (!isShooting)
        {
            anim.SetTrigger("Shoot");
            isShooting = true;
            Invoke(nameof(ShootStateFinished), 6);
        }
    }

    public void ShootStateFinished()
    {
        currentState = BossState.home;
        isShooting = false;
        timer = Time.time;
    }

    private void HandleDeath()
    {
        SoundManager.soundManager.PlaySfx("boss_died");
        bossUI.SetActive(false);
    }

    public void Shoot()
    {
        Instantiate(bulletPrefab, shootPoint);
        SoundManager.soundManager.PlaySfx("fireball");
    }

    private void SetAnimFloat(Vector2 setVec2)
    {
        anim.SetFloat("MoveX", setVec2.x);
        anim.SetFloat("MoveY", setVec2.y);
    }

    public void ChangeAnim(Vector2 dir)
    {
        if (dir.x >= 0)
            SetAnimFloat(Vector2.right);
        else if (dir.x <= 0)
            SetAnimFloat(Vector2.left);

        if (dir.y >= 0)
            SetAnimFloat(Vector2.up);
        else if (dir.y <= 0)
            SetAnimFloat(Vector2.down);

        if (dir.x >= 0 && dir.y >= 0)
            SetAnimFloat(new Vector2(1, 1));
        else if (dir.x >= 0 && dir.y <= 0)
            SetAnimFloat(new Vector2(1, -1));
        else if(dir.x <= 0 && dir.y >= 0)
            SetAnimFloat(new Vector2(-1, 1));
        else if(dir.x <= 0 && dir.y <= 0)
            SetAnimFloat(new Vector2(-1, -1));
    }
}