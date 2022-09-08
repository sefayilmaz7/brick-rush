using System;
using UnityEngine;

public class SlideController
{
    public static event Action<SlideData> OnSlide = delegate { };

    private bool fingerDown = false;

    private Vector3 startPosition;
    private Vector3 lastPosition;

    private Vector2 Movement {
        get {
            if (!fingerDown) return Vector2.zero;
            return Input.mousePosition - startPosition;
        }
    }

    private Vector2 Delta
    {
        get
        {
            var currentPosition = Input.mousePosition;
            var delta = currentPosition - lastPosition;
            lastPosition = currentPosition;
            return delta;
        }
    }

    public void Tick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            fingerDown = true;
            startPosition = Input.mousePosition;
            lastPosition = startPosition;
        }

        if (Input.GetMouseButton(0))
        {
            SendSlide();
        }

        if (Input.GetMouseButtonUp(0))
        {
            fingerDown = false;
        }
    }

    private void SendSlide()
    {
        OnSlide(new SlideData()
        {
            movement = Movement,
            normalizedMovement = Vector3.Normalize(Movement),
            delta = Delta
        });
    }
}