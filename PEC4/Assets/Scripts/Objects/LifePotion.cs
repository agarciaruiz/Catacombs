using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePotion : PowerUp
{
    public FloatValue playerHealth;
    public FloatValue heartContainers;
    public float increaseAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !collision.isTrigger)
        {
            playerHealth.runtimeValue += increaseAmount;
            if(playerHealth.initValue <= heartContainers.runtimeValue * 2)
            {
                playerHealth.runtimeValue = heartContainers.runtimeValue * 2;
            }
            powerUpSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
