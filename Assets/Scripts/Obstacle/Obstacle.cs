using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.NiceVibrations;
using Unity.VisualScripting;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ragdoll ragdoll))
        {
            PlayerAnimations.Instance.EnableRagdoll();
            var stack = other.GetComponentInChildren<Stack>();

            other.transform.parent.GetComponent<Player>().ActivateTrayPhysics();
            stack.DropAll();

            FTemplate.TriggerLevelFinished(false);
            return;
        }
        
        if (!other.TryGetComponent(out Brick brick)) return;
        MMVibrationManager.Haptic(HapticTypes.Selection);
        brick.Drop();

    }
}
