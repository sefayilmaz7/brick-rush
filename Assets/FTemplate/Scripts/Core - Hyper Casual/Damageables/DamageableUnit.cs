using System.Collections;
using UnityEngine;

public class DamageableUnit : FDamageableBehaviour
{
    override public void TakeDamage(float damage)
    {
        GetComponent<RunnerAnimator>().SetDead();
    }
}
