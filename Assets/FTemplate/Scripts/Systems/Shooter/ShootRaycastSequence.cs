using System.Collections;
using UnityEngine;

public class ShootRaycastSequence : SequencePlayer
{
    public Animator animator;

    public ShootRaycast shootRaycast;

    public float preShootAnimDelay;
    public float postShootAnimDelay;

    protected override void AddSteps()
    {
        var shootAnim = new BoolAnimation(animator, "Shoot");

        sequence.AddStep(new PlayAnimation(shootAnim));
        sequence.AddStep(new WaitForDuration(preShootAnimDelay));
        sequence.AddStep(new ExecuteAction(() => shootRaycast.Execute()));
        sequence.AddStep(new WaitForDuration(postShootAnimDelay));
        sequence.AddStep(new StopAnimation(shootAnim));
    }
}