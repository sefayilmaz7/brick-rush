using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class FingerMonitor : MonoBehaviour
{
    [SerializeField, Space] private bool Enabled = true;
    
    [Header("References")]
    [SerializeField] private Sprite NormalFinger;
    [SerializeField] private Sprite TappedFinger;
    [SerializeField] private Image image;

    [Header("Properties")]
    [SerializeField] private float RotateAmount = 45f;
    [SerializeField] private float LerpModifier = 10f;
    [SerializeField] private float InvisibleDuration = 1f;

    private Vector3 _lastMousePosition;
    private float _invisibleDuration;
    private bool _faded = false;

    private void Update()
    {
        UpdateFinger(Input.mousePosition, Time.deltaTime);
    }

    private void UpdateFinger(Vector3 mousePosition, float deltaTime)
    {
        if (image.gameObject.activeSelf != Enabled) image.gameObject.SetActive(Enabled);

        if (!Enabled) return;

        var newSprite = Input.GetMouseButton(0) ? TappedFinger : NormalFinger;
        image.sprite = newSprite;

        var newPosition = mousePosition;

        UpdateFingerFade(newPosition, deltaTime);

        var delta = newPosition - image.transform.position;
        var rotateAmount = -delta.normalized.x * RotateAmount;
        var newQuaternion = Quaternion.AngleAxis(rotateAmount, Vector3.forward);
        image.transform.rotation = Quaternion.Slerp(image.transform.rotation, newQuaternion, deltaTime * LerpModifier);

        image.transform.position = newPosition;
        _lastMousePosition = mousePosition;
    }

    private void UpdateFingerFade(Vector3 newPosition, float deltaTime)
    {
        if (_lastMousePosition == newPosition)
            _invisibleDuration += deltaTime;
        else
            _invisibleDuration = 0f;

        if (_invisibleDuration >= InvisibleDuration && !_faded)
        {
            _faded = true;
            image.CrossFadeAlpha(0f, 1f, false);
        }

        if (_faded && _invisibleDuration < InvisibleDuration)
        {
            _faded = false;
            image.CrossFadeAlpha(255f, 1f, false);
        }
    }
}