using System.Collections.Generic;
using UnityEngine;

public class CrowdController : MonoBehaviour, ICrowd
{
    public CrowdFormationStrategy formationStrategy;

    // TODO: add / remove members using an object pool
    private List<ICrowdMember> members = new List<ICrowdMember>();

    public int Size { get; set; }

    public void Add(int amount)
    {
        Size += amount;

        OnCrowdUpdate();
    }

    public void Remove(int amount)
    {
        Size -= amount;

        if (Size < 0)
            Size = 0;

        OnCrowdUpdate();
    }

    private void OnCrowdUpdate()
    {
        formationStrategy.HandleFormation(members);
    }
}
