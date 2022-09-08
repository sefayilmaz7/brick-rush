using System.Collections;
using UnityEngine;

public class ActivateGravity : IStep
{
    public bool IsComplete { get; set; }

    private Rigidbody rigidbody;

    public ActivateGravity(Rigidbody rigidbody)
    {
        this.rigidbody = rigidbody;
    }

    public void Start()
    {
        rigidbody.isKinematic = false;
        rigidbody.useGravity = true;
    }

    public void Tick(float deltaTime)
    {
        IsComplete = true;
    }
}