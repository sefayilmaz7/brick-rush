using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Brick : MonoBehaviour
{
    public int LocalIndex;
    public Floor CurrentFloor;

    [SerializeField] private Rigidbody Rigidbody;
    private bool _dropped;

    public void Move(Floor floor)
    {
        if (CurrentFloor)
            CurrentFloor.RemoveBrick(this);

        LocalIndex = floor.GetLastBrickIndex();
        CurrentFloor = floor;
        CurrentFloor.AddBrick(this);

        var targetTransform = CurrentFloor.GetTransformBy(LocalIndex);
        transform.DOScale(1f, 0.4f).From(0f);
        transform.position = CurrentFloor.transform.position;
        transform.SetParent(targetTransform);
        transform.DOLocalMove(Vector3.zero, .1f);
        transform.DOLocalRotate(Vector3.zero, .1f);
    }

    public void SwitchFloor(Floor currentFloor , Floor targetFloor , int index)
    {
        CurrentFloor.RemoveBrick(this);
        targetFloor.AddBrick(this);
        CurrentFloor = targetFloor;
        var floorCellPosition = targetFloor.cellPositions[index];
        transform.SetParent(floorCellPosition);
        transform.DOLocalMove(Vector3.zero, Random.Range(0.15f,0.3f));
        transform.DOLocalRotate(Vector3.zero, Random.Range(0.15f,0.3f));
    }
    
    public void Drop()
    {
        if (_dropped) return;
        _dropped = true;
        transform.SetParent(null);
        Rigidbody.isKinematic = false;
        GetComponent<Collider>().isTrigger = false;
        Rigidbody.AddForce(Vector3.back * 25 , ForceMode.Force);
        //transform.DORotate(Random.Range(0, 90) * Vector3.one, 1f);
        CurrentFloor.RemoveBrick(this);
        CurrentFloor.Stack.OnBrickCollision();
        CurrentFloor = null;
        Destroy(this, 2);
    }

    public void Align(Vector3 roadPosition)
    {
        CurrentFloor.RemoveBrick(this);
        transform.SetParent(null);
        transform.position = roadPosition;
        transform.DOScale(1.45f * Vector3.one, 0.1f);
        transform.rotation = quaternion.Euler(Vector3.zero);
    }

    public void Disappear()
    {
        CurrentFloor.RemoveBrick(this);
        transform.SetParent(null);
        transform.DOScale(Vector3.zero, 0.2f);
    }
}