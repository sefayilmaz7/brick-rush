using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class AnimatorBehavior : MonoBehaviour
{
    protected Animator _animator;

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        _animationTweens = new SimplePool<AnimationTween>(5, () => { return new AnimationTween(); });
    }

    protected virtual void Update()
    {
        if (_popedAnimationTweens.Count != 0) _popedAnimationTweens.ForEach(x => x.Update());
    }

    protected void Set<T>(int hash, T value)
    {
        if (!_animator) return;

        if (typeof(T) == typeof(bool))
            _animator.SetBool(hash, System.Convert.ToBoolean(value));

        if (typeof(T) == typeof(int))
            _animator.SetInteger(hash, System.Convert.ToInt16(value));

        if (typeof(T) == typeof(float))
            _animator.SetFloat(hash, System.Convert.ToSingle(value));
    }

    protected void SetTrigger(int hash)
    {
        if (!_animator) return;
        _animator.SetTrigger(hash);
    }

    #region Tween, Custom Transition
    private readonly float A_TRANSITION_SHORT = .1f;
    private readonly float A_TRANSITION_NORMAL = .25f;
    private readonly float A_TRANSITION_LONG = .5f;

    public enum AnimationTransition { Short, Normal, Long }

    private SimplePool<AnimationTween> _animationTweens;
    private List<AnimationTween> _popedAnimationTweens = new List<AnimationTween>();

    public class AnimationTween
    {
        public int Hash;
        public int Layer;

        private System.Action _killCallback;
        private System.Action _onUpdateCallback;

        public void Update()
        {
            _onUpdateCallback?.Invoke();
        }

        public void Play(int hash, int layer)
        {
            Hash = hash;
            Layer = layer;

            _killCallback = null;
            _onUpdateCallback = null;
        }

        public void Kill()
        {
            _killCallback?.Invoke();
        }

        public AnimationTween AddKillCallback(System.Action callback)
        {
            _killCallback += callback;
            return this;
        }

        public AnimationTween OnUpdate(System.Action onUpdate)
        {
            _onUpdateCallback += onUpdate;
            return this;
        }
    }

    protected AnimationTween Play(int stateHash, int stateLayer = 0, float transition = .25f)
    {
        if (!_animator) return null;

        _animator.CrossFade(stateHash, transition, stateLayer);
        var tween = _animationTweens.Pop();
        _popedAnimationTweens.Add(tween);
        tween.Play(stateHash, stateLayer);

        var animationDuration = _animator.GetCurrentAnimatorStateInfo(stateLayer).length;
        DelayManager.WaitAndInvoke(() =>
        {
            tween.Kill();
            _popedAnimationTweens.Remove(tween);
            _animationTweens.Push(tween);
        }, t: animationDuration);

        return tween;
    }

    protected AnimationTween Play(int stateHash, AnimationTransition animationTransition, int stateLayer = 0)
    {
        var transition = GetTransitionDuration(animationTransition);
        return Play(stateHash, stateLayer, transition);
    }

    protected (AnimationTween, AnimationTween) Play(int firstStateHash, int secondStateHash,
        int firstStateLayer = 0, int secondStateLayer = 0, float transition = .25f)
    {
        var firstTween = Play(firstStateHash, firstStateLayer, transition);
        var animationDuration = _animator.GetCurrentAnimatorStateInfo(firstStateLayer).length;
        var secondTween = firstTween;

        DelayManager.WaitAndInvoke(() =>
        {
            secondTween = Play(secondStateHash, secondStateLayer, transition);
        }, t: animationDuration);

        return (firstTween, secondTween);
    }

    protected (AnimationTween, AnimationTween) Play(int firstStateHash, int secondStateHash, AnimationTransition animationTransition,
        int firstStateLayer = 0, int secondStateLayer = 0)
    {
        var transition = GetTransitionDuration(animationTransition);
        var firstTween = Play(firstStateHash, firstStateLayer, transition);

        var animationDuration = _animator.GetCurrentAnimatorStateInfo(firstStateLayer).length;
        var secondTween = firstTween;

        DelayManager.WaitAndInvoke(() =>
        {
            secondTween = Play(secondStateHash, secondStateLayer, transition);
        }, t: animationDuration);

        return (firstTween, secondTween);
    }

    private float GetTransitionDuration(AnimationTransition animationTransition)
    {
        switch (animationTransition)
        {
            case AnimationTransition.Short: return A_TRANSITION_SHORT;
            case AnimationTransition.Normal: return A_TRANSITION_NORMAL;
            case AnimationTransition.Long: return A_TRANSITION_LONG;
            default: return A_TRANSITION_NORMAL;
        }
    }
    #endregion
}