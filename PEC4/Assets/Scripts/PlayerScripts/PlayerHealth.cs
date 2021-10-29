using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : GenericHealth
{
    [SerializeField] private Signal healthSignal;

    public override void Damage(float damage)
    {
        base.Damage(damage);
        maxHealth.runtimeValue = currentHealth;
        healthSignal.Raise();
        checkIfDead();
    }

    public void checkIfDead()
    {
        if(currentHealth == 0)
        {
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        SoundManager.soundManager.PlaySfx("player_died");
        GetComponentInParent<Animator>().Play("Death");
        GetComponentInParent<PlayerMovement>().enabled = false;
        yield return new WaitForSeconds(2);
        this.transform.parent.gameObject.SetActive(false);

        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("GameOver");        
    }
}
