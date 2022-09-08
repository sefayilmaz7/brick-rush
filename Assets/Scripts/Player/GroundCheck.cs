using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private Stack _stack;
    [SerializeField] private LayerMask groundLayer;
    
    private void InitStack()
    {
        _stack = GetComponent<Stack>();
    }

    private void Awake()
    {
        InitStack();
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            Debug.DrawRay(transform.position + Vector3.up, Vector3.down * hit.distance, Color.red);
        }
        else
        {
            Debug.DrawRay(transform.position + Vector3.up, Vector3.down * 10, Color.green);
            _stack.PlaceBrickToRoad(GetRoadTransform());
        }
    }
    
    private Vector3 GetRoadTransform()
    {
        var position = transform.position;
        return new Vector3(position.x,-0.6f,position.z);
    }
    
}
