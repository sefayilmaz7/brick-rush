using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float damage);
    void TakeDamage(float damage, Vector3 point);
    void TakeDamage(float damage, Vector3 point, IDamager damageSource);
}