using System.Collections.Generic;
using UnityEngine;

public class ExampleSearcherBehavior : SearcherBehavior
{
    private void Awake()
    {
        StartSearch<DamageableUnit>(FoundedDamageables, 10);
    }

    private void FoundedDamageables(List<DamageableUnit> damageables)
    {
        damageables.ForEach(x => x.TakeDamage(100));
    }
}