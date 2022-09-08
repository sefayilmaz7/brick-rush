using System;
using UnityEngine;

public class ExecuteAction : IStep
{
    public bool IsComplete { get; set; }

    private Action action;

    public ExecuteAction(Action action)
    {
        this.action = action;
    }

    public void Start()
    {
        action.Invoke();
    }

    public void Tick(float deltaTime)
    {
        IsComplete = true;
    }
}