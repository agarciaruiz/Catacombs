using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : Door
{
    [Header("Enemy Variables")]
    public GameObject boss;
    public bool isDefeated;

    [Header("Door Variables")]
    public BoxCollider2D blocker;

    private void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Enemy");
        if (open)
        {
            blocker.enabled = false;
        }
    }

    private void Update()
    {
        CheckIfDefeted();
        if (isDefeated)
        {
            Open();
            blocker.enabled = false;
        }
    }

    private void CheckIfDefeted()
    {
        if(boss.activeSelf == false)
        {
            isDefeated = true;
        }
    }
}
