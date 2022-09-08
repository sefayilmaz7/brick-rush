
using System;
using UnityEngine;

public class ExampleUIPanel : MonoBehaviour
{
    [SerializeField] private VoidEvent OnGameStarted;
    [SerializeField] private BoolEvent OnGameFinished;

    public void StartGame()
    {
        OnGameStarted.Raise();
    }

    private void Awake()
    {
        OnGameFinished.Raise(false);
    }

    private void Start()
    {
        OnGameFinished.Raise(true);
    }
}