using System.Collections;
using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    [Header("On Collision Settings")]
    public bool destroySelf = false;
    [Space]
    public GameObject effectPrefab;
    public Transform effectContainer;

    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(1f);

            if (effectPrefab != null)
                Instantiate(effectPrefab, effectContainer);

            if (destroySelf)
                Destroy(gameObject);
        }
    }
}