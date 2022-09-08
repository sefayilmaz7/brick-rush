using UnityEngine;


public class JoystickController : MonoBehaviour
{
    public float speed;
    public DynamicJoystick dynamicJoystick;
    public Rigidbody rb;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private PlayerAnimations playerAnimations;
    private Quaternion _direction;
    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * dynamicJoystick.Vertical + Vector3.right * dynamicJoystick.Horizontal;
        //transform.position += direction * speed * Time.deltaTime;
        characterController.Move(direction * speed * Time.deltaTime);
        if (direction != Vector3.zero)
        {
            _direction = Quaternion.LookRotation(direction);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, _direction, 2 * Time.deltaTime);

        if (direction == Vector3.zero)
        {
            playerAnimations.BackToIdleAnim();
        }
        else
        {
            playerAnimations.SwitchToRunAnim();
        }
    }

    private void EnableCharacterController()
    {
        characterController.enabled = true;
    }
    private void OnEnable()
    {
        EnableCharacterController();
    }
}