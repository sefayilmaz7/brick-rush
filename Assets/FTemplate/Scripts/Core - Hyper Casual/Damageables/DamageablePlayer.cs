using System;
using System.Collections.Generic;
using UnityEngine;

public class DamageablePlayer : FDamageableBehaviour
{
    override public void TakeDamage(float damage)
    {
        GetComponent<RunnerAnimator>().SetDead();
        TriggerLevelFinished(false);
    }
}
