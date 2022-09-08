using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : SingletonBehaviour<UIManager>
{
    [SerializeField] private GameObject joystick;

    public void ActivateJoystick()
    {
        if (joystick == null)
            return;
        joystick.SetActive(true);
    }

    private void Start()
    {
        GameStates.Instance.TriggerSwitchToIdleArcade += ActivateJoystick;
    }

    private void OnDisable()
    {
        GameStates.Instance.TriggerSwitchToIdleArcade -= ActivateJoystick;
    }
}
