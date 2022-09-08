using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStates : SingletonBehaviour<GameStates>
{
    public delegate void SwitchToIdleArcade();
    public SwitchToIdleArcade TriggerSwitchToIdleArcade;

    public void SwitchToIdleEvent()
    {
        TriggerSwitchToIdleArcade();
    }
}
