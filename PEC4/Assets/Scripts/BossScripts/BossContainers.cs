using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossContainers : HeartManager
{
    [SerializeField] private GameObject bossPanel;

    public static BossContainers instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        bossPanel.SetActive(false);
        UpdateHearts();
    }

    public void BossActivator()
    {
        bossPanel.SetActive(true);
    }

    public override void UpdateHearts()
    {
        InitHearts();
        float tempHealth = playerCurrentHealth.runtimeValue / 2;

        for (int i = 0; i < heartContainers.runtimeValue; i++)
        {
            if (i <= tempHealth - 1)
                hearts[i].sprite = fullHeart;
            else if (i >= tempHealth)
                hearts[i].sprite = emptyHeart;
            else
                hearts[i].sprite = halfHeart;
        }
    }

}
