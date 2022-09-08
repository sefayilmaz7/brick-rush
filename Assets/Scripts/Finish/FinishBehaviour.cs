using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.PLAYER))
        {
            var gameStates = GameStates.Instance;
            if (!gameStates) return;
            gameStates.TriggerSwitchToIdleArcade();
        }
    }
}
