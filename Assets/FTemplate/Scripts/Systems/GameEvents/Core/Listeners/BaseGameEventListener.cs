
using System;
using UnityEngine;
using UnityEngine.Events;

public class BaseGameEventListener<T, E, UER> : MonoBehaviour, IGameEventListener<T> where E : BaseGameEvent<T> where UER : UnityEvent<T>
{
    [SerializeField] private E gameEvent;
    public E GameEvent => gameEvent;
    
    [SerializeField] private UER unityEvent;
    
    public void OnEventRaised(T data)
    {
        unityEvent.Invoke(data);
    }

    private void OnEnable()
    {
        if (gameEvent == null) return;

        GameEvent.RegisterListener(this);
    }
    
    
    private void OnDisable()
    {
        if (gameEvent == null) return;

        GameEvent.UnRegisterListener(this);
    }
}