using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class ShootRaycast : MonoBehaviour
{
    [Header("Muzzle Settings")]
    public GameObject muzzlePrefab;
    public Transform muzzleContainer;

    [Header("Shooting Settings")]
    public Transform directionSource;
    public Transform shootingSource;

    [Header("Extra Shooting Settings")]
    public float range;
    public LayerMask layerMask;

    public float damage = 1f;

    public void Execute()
    {
        if (muzzlePrefab != null)
        {
            Instantiate(muzzlePrefab, muzzleContainer);
        }

        if (Physics.Raycast(shootingSource.position, directionSource.forward, out RaycastHit hit, range, layerMask))
        {
            OnRaycastHit(hit);
        }
    }

    abstract protected void OnRaycastHit(RaycastHit hit);
}
