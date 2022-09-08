using Cinemachine;
using DG.Tweening;
using FluffyUnderware.Curvy.Controllers;
using UnityEngine;

public class Player : MonoBehaviour, IInputListener
{ 
    [SerializeField] private CinemachineVirtualCamera[] cameras;
    [SerializeField] private SplineController splineController;
    [SerializeField] private SplineFollower splineFollower;
    [SerializeField] private JoystickController joystickController;
    [SerializeField] private float forwardTilt;
    
    [SerializeField] private Transform tray;
    public Transform Tray => tray;
    [SerializeField] private Rigidbody _trayRigidBody;
    [SerializeField] private Collider _trayCollider;

    private void Awake()
    {
        DOTween.SetTweensCapacity(1000,50);
    }

    private void SwitchCameras()
    {
        cameras[0].GetComponent<CinemachineVirtualCamera>().m_Priority = 0;
        cameras[1].GetComponent<CinemachineVirtualCamera>().m_Priority = 20;
    }

    private void DisableSpline()
    {
        splineController.enabled = false;
        splineFollower.enabled = false;
    }

    private void EnableJoystick()
    {
        joystickController.enabled = true;
        splineFollower.InputManager.Unsubscribe(this);
    }

    private void OnEnable()
    {
        GameStates.Instance.TriggerSwitchToIdleArcade += SwitchCameras;
        GameStates.Instance.TriggerSwitchToIdleArcade += DisableSpline;
        GameStates.Instance.TriggerSwitchToIdleArcade += EnableJoystick;
        GameStates.Instance.TriggerSwitchToIdleArcade += ResetTilt;
        splineFollower.InputManager.Subscribe(this);
    }

    private void OnDisable()
    {
        GameStates.Instance.TriggerSwitchToIdleArcade -= SwitchCameras;
        GameStates.Instance.TriggerSwitchToIdleArcade -= DisableSpline;
        GameStates.Instance.TriggerSwitchToIdleArcade -= EnableJoystick;
        GameStates.Instance.TriggerSwitchToIdleArcade -= ResetTilt;
    }

    private Quaternion _tilt;
    [SerializeField] private float sideTilt;
    
    
    private void FixedUpdate()
    {
        tray.rotation = Quaternion.Lerp(tray.rotation, _tilt , 1*Time.deltaTime);
    }

    public void OnSwipe(SwipeData data)
    {
        return;
    }

    public void OnSlide(SlideData data)
    {
        var absX = Mathf.Abs(data.delta.x) < 0.3f ? 0 : 1;
        _tilt = Quaternion.Euler( absX * forwardTilt, 0f, data.delta.x * sideTilt);
        tray.rotation = Quaternion.Lerp(tray.rotation, _tilt, 5*Time.deltaTime);
        _tilt = Quaternion.identity;
    }

    public void ActivateTrayPhysics()
    {
        _trayRigidBody.isKinematic = false;
        _trayRigidBody.useGravity = true;
        //_trayCollider.isTrigger = false;
    }

    private void ResetTilt()
    {
        forwardTilt = 0;
        sideTilt = 0;
    }
}
