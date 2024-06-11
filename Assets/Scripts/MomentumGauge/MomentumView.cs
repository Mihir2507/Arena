using UnityEngine;
using UnityEngine.UI;

public class MomentumView : MonoBehaviour
{
    [SerializeField] private Image[] segmentImages;

    public void UpdateMomentumUI(float currentMomentum, float maxMomentum, int segments)
    {
        float segmentValue = maxMomentum / segments;
        for (int i = 0; i < segments; i++)
        {
            if (currentMomentum >= (i + 1) * segmentValue)
            {
                segmentImages[i].fillAmount = 1f;
            }
            else
            {
                float fillValue = (currentMomentum - i * segmentValue) / segmentValue;
                segmentImages[i].fillAmount = Mathf.Clamp01(fillValue);
            }
        }
    }
}