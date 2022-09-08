using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using EasyButtons;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class Stack : MonoBehaviour
{
    [SerializeField] private StackData stackData;
    
    [SerializeField] private Floor[] floorPrefabs;

    [SerializeField] private List<Floor> _allFloors = new();
    private List<Brick> _allBricks = new();

    private int _currentFloorIndex;

    private Coroutine _brickCollisionCoroutine;
    private Coroutine _placeBrickCoroutine;
    private Coroutine _fillBuildingCoroutine;

    [SerializeField] private GameObject damageBall;

    private void Awake()
    {
        SetDataObjects();
        CreateFirstFloor();
    }

    private void SetDataObjects()
    {
        floorPrefabs = stackData.layers;
    }

    public void AddBrick(Brick brickPrefab)
    {
        var lastFloor = GetLastFloor(true);
        var brick = Instantiate(brickPrefab);
        brick.Move(lastFloor);
        _allBricks.Add(brick);
    }

    public void AddBricks(int count , Brick brickPrefab)
    {
        var floor = GetTopFloor();
        for (int i = 0; i < count; i++)
        {
            AddBrick(brickPrefab);
        }
        MMVibrationManager.Haptic(HapticTypes.Selection);
        if (floor.BrickCount() >= (floor.Bricks.Length * 2) / 3) return;
        OnBrickCollision();
    }

    private Floor GetLastFloor(bool getLastNonFull = false)
    {
        if (_allFloors.Count == 0)
        {
            return default;
        }

        var floorToReturn = _allFloors[0];
        for (int i = 0; i < _allFloors.Count; i++)
        {
            floorToReturn = _allFloors[i];
            if (_allFloors[i].IsEmpty())
            {
                floorToReturn = _allFloors[Mathf.Max(0, i - 1)];
                if (getLastNonFull && floorToReturn.IsFull())
                {
                    floorToReturn = _allFloors[i];
                }

                break;
            }
        }

        if (getLastNonFull && floorToReturn.IsFull())
        {
            floorToReturn = CreateFloor();
        }

        return floorToReturn;
    }

    private Floor GetTopFloor()
    {
        for (int i = _allFloors.Count - 1; i >= 0; i--)
        {
            if (!_allFloors[i].IsEmpty())
            {
                return _allFloors[Mathf.Max(0, i - 1)];
            }
        }

        return _allFloors[0];
    }

    private void CreateFirstFloor()
    {
        var currentFloorPrefab = GetFloorPrefab();
        var transformPosition = transform.position + stackData.height * Vector3.up;
        var newFloor = Instantiate(currentFloorPrefab, transformPosition, transform.rotation);
        newFloor.transform.parent = transform;
        _allFloors.Add(newFloor);
        newFloor.OnCreate(this);
    }

    private Floor CreateFloor()
    {
        var lastFloor = _allFloors[^1];
        var currentFloorPrefab = GetFloorPrefab();
        var targetTransform = lastFloor ? lastFloor.transform : transform;
        var transformPosition = targetTransform.position + stackData.height * transform.up;
        var newFloor = Instantiate(currentFloorPrefab, transformPosition, targetTransform.rotation);
        newFloor.transform.parent = transform;
        _allFloors.Add(newFloor);
        newFloor.OnCreate(this);
        return newFloor;
    }

    
    private Floor GetFloorPrefab()
    {
        var prefab = floorPrefabs[_currentFloorIndex];
        _currentFloorIndex = ++_currentFloorIndex % floorPrefabs.Length;
        return prefab;
    }

    private IEnumerator BrickCollisionCoroutine()
    {
        yield return BetterWaitForSeconds.Wait(.5f);

        foreach (var floor in _allFloors)
        {
            for (var index = 0; index < floor.Bricks.Length; index++)
            {
                var brick = floor.Bricks[index];
                if (brick == null) // Boş brick bulundu
                {
                    for (int i = _allFloors.IndexOf(floor);
                         i < _allFloors.Count;
                         i++) // boş brick'in bulunduğu floor'un üstündeki floorlar taranıyor
                    {
                        var replacementBrick = _allFloors[i].Bricks[index];
                        if (_allFloors[i].Bricks[index] != null) // aynı index'de başka bir brick bulundu
                        {
                            // O brick'i şu anki floor'un o indexine taşıyacağız

                            replacementBrick.SwitchFloor(replacementBrick.CurrentFloor, floor, index);
                            //yield return BetterWaitForSeconds.Wait(Mathf.Epsilon);
                            break;
                        }
                    }
                }
            }
        }

        _brickCollisionCoroutine = null;
    }

    public void OnBrickCollision()
    {
        if (_brickCollisionCoroutine == null)
        {
            _brickCollisionCoroutine = StartCoroutine(BrickCollisionCoroutine());
        }
    }

    public Floor GetBottomFloor(Floor floor)
    {
        var index = _allFloors.IndexOf(floor);
        return index - 1 < 0 ? null : _allFloors[index];
    }

    private IEnumerator PlaceBrickCoroutine(Vector3 roadPosition)
    {
        var lastFloor = GetLastFloor();
        if (lastFloor.IsEmpty())
        {
            PlayerAnimations.Instance.EnableRagdoll();
            FTemplate.TriggerLevelFinished(false);
            yield break;
        }

        var lastBrick = lastFloor.GetLastBrick();
        lastBrick.Align(roadPosition);

        yield return BetterWaitForSeconds.Wait(0.012f);
        _placeBrickCoroutine = null;
    }

    public void PlaceBrickToRoad(Vector3 roadPosition)
    {
        if (_placeBrickCoroutine == null)
        {
            _placeBrickCoroutine = StartCoroutine(PlaceBrickCoroutine(roadPosition));
        }
    }

    private IEnumerator FillBuildingCoroutine(Coroutine openPartCoroutine , Collider playerCollider)
    {
        var lastFloor = GetLastFloor();
        if (lastFloor.IsEmpty())
        {
            playerCollider.enabled = false;
            openPartCoroutine = null;
            GameManager.Instance.CompleteLevel(1.5f);
            yield break;
        }

        var lastBrick = lastFloor.GetLastBrick();
        lastBrick.Disappear();

        yield return BetterWaitForSeconds.Wait(0.04f);
        //_fillBuildingCoroutine = null;
    }

    public void FillBuilding(Coroutine openPartCoroutine , Collider playerCollider)
    {
        if (_fillBuildingCoroutine == null)
        {
            _fillBuildingCoroutine = StartCoroutine(FillBuildingCoroutine(openPartCoroutine , playerCollider));
        }
    }

    public void StopBuildingCoroutine()
    {
        if (_fillBuildingCoroutine != null)
        {
            StopCoroutine(_fillBuildingCoroutine);
        }
        _fillBuildingCoroutine = null;
    }

    public void RemoveBrick(Coroutine openPartCoroutine)
    {
        var lastFloor = GetLastFloor();
        if (lastFloor.IsEmpty())
        {
            //playerCollider.enabled = false;
            if(openPartCoroutine != null)
                StopCoroutine(openPartCoroutine);
            GameManager.Instance.CompleteLevel(1.5f);
        }

        var lastBrick = lastFloor.GetLastBrick();

        if (lastBrick == null)
            return;
        
        lastBrick.Disappear();
    }

    public void DropAll()
    {
        foreach (var brick in _allBricks)
        {
            brick.Drop();
        }

        Instantiate(damageBall, transform.position , transform.rotation);

    }
}