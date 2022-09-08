using System.Collections;
using UnityEngine;

public class PlayAnimation : IStep
{
    public bool IsComplete { get; set; }

    private FAnimation animation;

    public PlayAnimation(FAnimation animation)
    {
        this.animation = animation;
    }

    public void Start()
    {
        animation.Apply();
    }

    public void Tick(float deltaTime)
    {
        IsComplete = true;
    }
}

public class StopAnimation : IStep
{
    public bool IsComplete { get; set; }

    private FAnimation animation;

    public StopAnimation(FAnimation animation)
    {
        this.animation = animation;
    }

    public void Start()
    {
        animation.Apply();
    }

    public void Tick(float deltaTime)
    {
        IsComplete = true;
    }
}
