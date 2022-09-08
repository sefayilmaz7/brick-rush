using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    [SerializeField] private Building[] allBuildings;
    
    private bool _isPassed = false;

    private void Update()
    {
        if (_isPassed)
        {
            foreach (var building in allBuildings)
            {
                if (!building.CheckFull())
                {
                    return;
                }
            }
        
            ResetAllBuildings();
            GameManager.Instance.CompleteLevel(1.5f);
        }
    }

    private void ResetAllBuildings()
    {
        foreach (var building in allBuildings)
        {
            building.ResetBuilding();
        }
    }

    private void SwitchPassValue()
    {
        _isPassed = true;
    }

    private void Start()
    {
        GameStates.Instance.TriggerSwitchToIdleArcade += SwitchPassValue;
    }

    private void OnDisable()
    {
        GameStates.Instance.TriggerSwitchToIdleArcade -= SwitchPassValue;
    }
}
