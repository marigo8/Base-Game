using UnityEngine;

public class FrameRateLimiter : MonoBehaviour
{
    public int targetFrameRate = 30;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;
    }

    private void Update() 
    {
        if(Application.targetFrameRate != targetFrameRate)
        {
            Application.targetFrameRate = targetFrameRate;
        }
    }
}
