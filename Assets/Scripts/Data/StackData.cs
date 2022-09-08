using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StackData", menuName = "Data/StackData", order = 1)]
public class StackData : ScriptableObject
{
    public Floor[] layers;
    public float brickTimeInterval;
    public float brickScaleTime;

    public float height;
}
