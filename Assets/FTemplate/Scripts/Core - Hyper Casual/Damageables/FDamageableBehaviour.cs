using System.Collections;
using UnityEngine;

public class FDamageableBehaviour : FBehaviour, IDamageable
{
    virtual public void TakeDamage(float damage)
    {
        throw new System.NotImplementedException();
    }

    virtual public void TakeDamage(float damage, Vector3 point)
    {
        throw new System.NotImplementedException();
    }

    virtual public void TakeDamage(float damage, Vector3 point, IDamager damageSource)
    {
        throw new System.NotImplementedException();
    }
}