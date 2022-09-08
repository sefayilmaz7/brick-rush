using System.Collections;
using UnityEngine;
using DG.Tweening;

public class SwingAnimation : MonoBehaviour
{
    public float angle = 30f;
    public float duration = 1f;
    public Ease ease = Ease.InOutQuint;

    public Transform target;

    private int directionMultiplier = 1;

    private void Awake()
    {
        if (target.rotation.z < 0)
        {
            target.DOLocalRotate(new Vector3(0f, 0f, angle), duration * ((-target.rotation.z + angle) / (angle * 2f))).SetEase(ease).OnComplete(Swing);
            directionMultiplier = -1;
        }
        else
        {
            target.DOLocalRotate(new Vector3(0f, 0f, -angle), duration * ((target.rotation.z + angle) / (angle * 2f))).SetEase(ease).OnComplete(Swing);
            directionMultiplier = 1;
        }
    }

    private void Swing()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(target.DOLocalRotate(new Vector3(0f, 0f, directionMultiplier * angle), duration).SetEase(ease));
        sequence.Append(target.DOLocalRotate(new Vector3(0f, 0f, directionMultiplier * -angle), duration).SetEase(ease));
        sequence.SetLoops(-1, LoopType.Restart);
        sequence.Play();
    }
}