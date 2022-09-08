using System.Collections;
using UnityEngine;
using FluffyUnderware.Curvy.Controllers;

public class SplineFollower : FBehaviour, IInputListener
{
    [Header("References")]
    public InputManager InputManager;
    public Animator animator;
    public SplineController splineController;
    public Transform mainBody;
    public float horizontalSpeed = 1f;

    [Header("Movement Settings")]
    public FloatRange xRange;

    protected override void OnLevelStarted()
    {
        base.OnLevelStarted();
        InputManager.Subscribe(this);
        splineController.enabled = true;
        animator.SetBool(Animations.RUN, true);
    }
    

    protected override void OnLevelFinished(bool success)
    {
        base.OnLevelFinished(success);
        InputManager.Unsubscribe(this);
        splineController.enabled = false;
        animator.SetBool(Animations.RUN, false);
    }

    public void OnSwipe(SwipeData data)
    {
        throw new System.NotImplementedException();
    }

    public void OnSlide(SlideData data)
    {
        var newLocalPosition = mainBody.localPosition + Vector3.right * data.delta.x * horizontalSpeed * Time.deltaTime;
        newLocalPosition.x = Mathf.Clamp(newLocalPosition.x, xRange.min, xRange.max);
        mainBody.localPosition = newLocalPosition;
    }

    virtual protected void OnEnable()
    {
        InputManager.Subscribe(this);
        GameManager.Instance.GameStartedEvent += OnLevelStarted;
        GameManager.Instance.GameOverEvent += OnLevelFinished;
    }

    virtual protected void OnDisable()
    {
        InputManager.Unsubscribe(this);
        GameManager.Instance.GameStartedEvent -= OnLevelStarted;
        GameManager.Instance.GameOverEvent -= OnLevelFinished;
    }
}