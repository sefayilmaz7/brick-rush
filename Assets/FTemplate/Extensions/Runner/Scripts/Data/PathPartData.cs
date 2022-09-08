﻿using System;
using UnityEngine;

public enum PathDirection
{
    Left,
    Forward,
    Right
}

[Serializable]
public class PathPartData
{
    public PathDirection direction = PathDirection.Forward;
    public float distance = 20f;
    public float turnLenght = 11f;

    public void Copy(PathPartData pathData)
    {
        direction = pathData.direction;
        distance = pathData.distance;
        turnLenght = pathData.turnLenght;
    }
}