using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Gate : MonoBehaviour
{
    // world canvas
    [SerializeField] private TextMeshPro gateText;
    [SerializeField] private int _gateValue;
    [SerializeField] private Brick brickPrefab;

    private void SetRandomValue()
    {
        if (_gateValue == 0)
        {
            _gateValue = Random.Range(30, 110);
            if (_gateValue % 5 != 0)
            {
                _gateValue -= _gateValue % 5;
            }
        }
        gateText.text = _gateValue.ToString();
    }

    private void Awake()
    {
        SetRandomValue();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Ragdoll ragdoll))
            return;
        
        var stack = other.GetComponentInChildren<Stack>();
        if (!stack) return;
        
        stack.AddBricks(_gateValue , brickPrefab);

    }
}
