using System.Collections;
using UnityEngine;

public class PlayVFX : IStep
{
    public bool IsComplete { get; set; }

    private GameObject effectPrefab;
    private Transform container;

    public PlayVFX(GameObject effectPrefab, Transform container = null)
    {
        this.effectPrefab = effectPrefab;
        this.container = container;
    }

    public void Start()
    {
        if (container != null)
        {
            Object.Instantiate(effectPrefab, container);
        }
        else
        {
            Object.Instantiate(effectPrefab);
        }
    }

    public void Tick(float deltaTime)
    {
        IsComplete = true;
    }
}
