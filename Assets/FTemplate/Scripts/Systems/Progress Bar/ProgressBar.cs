using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image fillImage;
    [Space]
    public FloatRange valueRange;
    [Space]
    public ColorRange colorRange;
    [Space]
    public float speed;

    private float actualProgress;
    private float visualProgress;

    public void UpdateProgress(float value)
    {
        actualProgress = (value - valueRange.min) / valueRange.Diff;
    }

    private void Update()
    {
        visualProgress = Mathf.Lerp(visualProgress, actualProgress, Time.deltaTime * speed);

        fillImage.color = colorRange.GetValueAtPercent(visualProgress);
        fillImage.fillAmount = visualProgress;
    }
}
