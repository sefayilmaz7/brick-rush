
using System.Collections.Generic;
using UnityEngine;

public class BaseGameEvent<T> : ScriptableObject
{
    private readonly List<IGameEventListener<T>> eventListeners = new List<IGameEventListener<T>>();

    public void Raise(T data)
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
        {
            eventListeners[i].OnEventRaised(data);
        }
    }

    public void RegisterListener(IGameEventListener<T> listener)
    {
        if (!eventListeners.Contains(listener))
        {
            eventListeners.Add(listener);
        }
    }

    public void UnRegisterListener(IGameEventListener<T> listener)
    {
        if (eventListeners.Contains(listener))
        {
            eventListeners.Remove(listener);
        }
    }
}
