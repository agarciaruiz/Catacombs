using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GenericDamage : MonoBehaviour
{
    public float damage;
    [SerializeField] private string otherTag;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(otherTag) && other.isTrigger)
        {
            GenericHealth tempHealth = other.GetComponent<GenericHealth>();
            if (tempHealth)
            {
                tempHealth.Damage(damage);
            }
        }
    }
}
