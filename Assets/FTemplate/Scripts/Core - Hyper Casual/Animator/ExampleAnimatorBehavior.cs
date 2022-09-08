using UnityEngine;

public class ExampleAnimatorBehavior : AnimatorBehavior
{
    private readonly int A_HASH_TRIGGER_JUMP = Animator.StringToHash("Jump");
    private readonly int A_HASH_BOOL_RUNNING = Animator.StringToHash("Running");
    private readonly int A_HASH_STATE_IDLE = Animator.StringToHash("Idle");
    private readonly int A_HASH_STATE_DANCE = Animator.StringToHash("Dance");

    private void Start()
    {
        DoDance();
    }

    public void DoJump()
    {
        SetTrigger(A_HASH_TRIGGER_JUMP);
    }

    public void ChangeRunning(bool value)
    {
        Set(A_HASH_BOOL_RUNNING, value);
    }

    public void DoDance()
    {
        Play(A_HASH_STATE_DANCE, A_HASH_STATE_IDLE, AnimationTransition.Normal).Item1
            .AddKillCallback(() => GameManager.Instance.CompleteLevel())
            .OnUpdate(() => print("Dancing!"));
    }
}