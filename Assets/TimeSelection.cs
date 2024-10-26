using UnityEngine;

public class TimeSelection : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Play3Minute()
    {
        SystemScript.timer = 180f;
    }

    public void Play6Minute()
    {
        SystemScript.timer = 360f;
    }

    public void Play9Minute()
    {
        SystemScript.timer = 540f;
    }

    public void Play10Minute()
    {
        SystemScript.timer = 600f;
    }
}
