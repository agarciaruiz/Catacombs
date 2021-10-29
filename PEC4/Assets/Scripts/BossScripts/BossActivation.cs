using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivation : MonoBehaviour
{
    [SerializeField] private GameObject boss;

    private void Start()
    {
        SoundManager.soundManager.PlayMusic("boss_ost");
        boss.GetComponent<BossMovement>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            boss.GetComponent<BossMovement>().enabled = true;
            BossContainers.instance.BossActivator();
            Destroy(gameObject);
        }
    }
}
