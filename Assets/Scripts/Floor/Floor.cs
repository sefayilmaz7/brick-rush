using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Floor : MonoBehaviour
{
    [HideInInspector] public List<Transform> cellPositions = new();
    [SerializeField] private Brick[] bricks;
    public Brick[] Bricks => bricks;
    
    private Stack _stack;
    public Stack Stack => _stack;

    public int floorType;

    private int _brickCount;

    private void Awake()
    {
        SetCellPositions();
    }

    private void SetCellPositions()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
             // var randNumber = Random.Range(0, 10);
             // if (randNumber < 5)
             // {
             //     transform.GetChild(i).transform.position += new Vector3(0, 0, Random.Range(0,0.1f));
             // }
             cellPositions.Add(transform.GetChild(i));
        }

        bricks = new Brick[cellPositions.Count];
    }

    public bool IsFull()
    {
        for (var index = 0; index < bricks.Length; index++)
        {
            var brick = bricks[index];
            if (bricks[index] == null)
            {
                return false;
            }
        }
        
        return true;
    }

    public void AddBrick(Brick brick)
    {
        _brickCount++;
        bricks[brick.LocalIndex] = brick;
    }

    public void RemoveBrick(Brick brick)
    {
        _brickCount--;
        bricks[brick.LocalIndex] = null;
    }

    public void OnCreate(Stack stack)
    {
        _stack = stack;
    }
    
    public int GetLastBrickIndex()
    {
        for (var index = 0; index < bricks.Length; index++)
        {
            if (bricks[index] == null)
            {
                return index;
            }
            
            continue;
        }
        
        return bricks.Length;
    }

    public Brick GetLastBrick()
    {
        for (int i = Bricks.Length - 1; i >= 0; i--)
        {
            if (Bricks[i] != null)
            {
                return Bricks[i];
            }
        }

        return default;
    }

    public Transform GetTransformBy(int localIndex)
    {
        return cellPositions[localIndex];
    }

    public bool IsEmpty()
    {
        foreach (var brick in bricks)
        {
            if (brick != null)
            {
                return false;
            }
        }

        return true;
    }

    public int BrickCount()
    {
        /*int brickCount = 0;
        foreach (var brick in bricks)
        {
            if (brick != null)
            {
                brickCount++;
            }
        }*/

        return _brickCount;
    }
}
