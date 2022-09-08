using System.Collections;
using UnityEngine;

abstract public class FAnimation
{
    protected Animator animator;
    protected string key;

    public FAnimation(Animator animator, string key)
    {
        this.animator = animator;
        this.key = key;
    }

    abstract public void Apply();
    abstract public void Unapply();
}

public class TriggerAnimation : FAnimation
{
    public TriggerAnimation(Animator animator, string key) : base(animator, key) { }

    public override void Apply()
    {
        animator.SetTrigger(key);
    }

    public override void Unapply()
    {
        animator.ResetTrigger(key);
    }
}

public class BoolAnimation : FAnimation
{
    public BoolAnimation(Animator animator, string key) : base(animator, key) { }

    public override void Apply()
    {
        animator.SetBool(key, true);
    }

    public override void Unapply()
    {
        animator.SetBool(key, false);
    }
}