using System.Collections;
using UnityEngine;

public class WaitForDuration : IStep
{
    public bool IsComplete { get; set; }

    private Timer timer;

    public WaitForDuration(float duration)
    {
        timer = new Timer(duration);
    }

    public void Start()
    {
        timer.Start();
    }

    public void Tick(float deltaTime)
    {
        timer.Tick(Time.deltaTime);

        if (timer.Stopped)
            IsComplete = true;
    }
}