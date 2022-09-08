using UnityEngine;

public class TimedState : IState
{
    public bool IsComplete { get; set; }

    protected Timer timer;

    public TimedState(float duration)
    {
        timer = new Timer(duration);
    }

    virtual public void OnEnter()
    {
        if (!timer.Stopped)
            timer.Start();
        else
            timer.Restart();
    }

    virtual public void Tick()
    {
        timer.Tick(Time.deltaTime);
        if (timer.Stopped)
            IsComplete = true;
    }

    virtual public void OnExit()
    {
        timer.Stop();
        IsComplete = false;
    }
}