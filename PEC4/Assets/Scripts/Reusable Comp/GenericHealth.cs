using UnityEngine;

public class GenericHealth : MonoBehaviour
{
    public FloatValue maxHealth;
    public float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth.runtimeValue;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = maxHealth.runtimeValue;
    }

    public virtual void Heal(float amount)
    {
        currentHealth += amount;
        if(currentHealth > maxHealth.runtimeValue)
        {
            currentHealth = maxHealth.runtimeValue;
        }
    }

    public virtual void FullHeal()
    {
        currentHealth = maxHealth.runtimeValue;
    }

    public virtual void Damage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
        }
    }

    public virtual void InstantDeath()
    {
        currentHealth = 0;
    }

}
