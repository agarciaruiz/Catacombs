using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : GenericHealth
{
    [SerializeField] private Signal healthSignal;
    [SerializeField] private LootTable lootTable;
    [SerializeField] private BoolValue alreadyDead;
    [SerializeField] private bool isDead;

    public override void Damage(float damage)
    {
        base.Damage(damage);
        maxHealth.runtimeValue = currentHealth;
        healthSignal.Raise();
        checkIfDead();
    }

    public void checkIfDead()
    {
        if (currentHealth == 0)
        {
            StartCoroutine(Die());
            Drop();
        }
    }

    private void Drop()
    {
        if (lootTable != null)
        {
            PowerUp currentPowerup = lootTable.LootPowerUp();
            if (currentPowerup != null)
            {
                Instantiate(currentPowerup.gameObject, transform.position, Quaternion.identity);
            }
        }
    }

    public IEnumerator Die()
    {
        SoundManager.soundManager.PlaySfx("enemy_died");
        GetComponentInParent<BoxCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        isDead = true;
        alreadyDead.runtimeValue = isDead;
        GetComponentInParent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponentInParent<Animator>().Play("Died");

        yield return new WaitForSeconds(2f);

        this.transform.parent.gameObject.SetActive(false);
    }
}
