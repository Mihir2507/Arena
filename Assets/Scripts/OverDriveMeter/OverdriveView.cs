using UnityEngine;
using UnityEngine.UI;

public class OverdriveView : MonoBehaviour
{
    [SerializeField] private Image[] segmentImages;

    public void UpdateOverdriveUI(float currentOverdrive, float maxOverdrive, int segments)
    {
        float fillAmount = currentOverdrive / maxOverdrive;
        int filledSegments = Mathf.FloorToInt(fillAmount * segments);

        for (int i = 0; i < segmentImages.Length; i++)
        {
            if (i < filledSegments)
            {
                segmentImages[i].fillAmount = 1f;
            }
            else if (i == filledSegments)
            {
                segmentImages[i].fillAmount = fillAmount * segments - filledSegments;
            }
            else
            {
                segmentImages[i].fillAmount = 0f;
            }
        }
    }
}