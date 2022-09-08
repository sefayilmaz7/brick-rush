using System.Collections;
using UnityEngine;

abstract public class SequencePlayer : MonoBehaviour
{
    protected Sequence sequence;

    private void Awake()
    {
        sequence = new Sequence();
        AddSteps();
    }

    private void Start()
    {
        sequence.Start();
    }

    private void Update()
    {
        sequence.Tick(Time.deltaTime);

        if (sequence.IsComplete)
            enabled = false;
    }


    abstract protected void AddSteps();
}