using UnityEngine;

public class TimeSelection : MonoBehaviour
{
    public SystemScript system;
    Vector2 pointer1;
    Vector2 pointer2;

    void Start()
    {
        
    }

    void Update()
    {
        pointer1 = system.pointer1;
        pointer2 = system.pointer2;
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
