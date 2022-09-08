using System.Collections;
using UnityEngine;

public class ActivateRagdoll : IStep
{
    public bool IsComplete { get; set; }

    private Ragdoll ragdoll;

    public ActivateRagdoll(Ragdoll ragdoll)
    {
        this.ragdoll = ragdoll;
    }

    public void Start()
    {
        ragdoll.Open();
    }

    public void Tick(float deltaTime)
    {
        IsComplete = true;
    }
}