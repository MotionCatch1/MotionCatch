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
        GameObject.Find("System").GetComponent<SystemScript>().timer = 180f;
    }

    public void Play6Minute()
    {
        GameObject.Find("System").GetComponent<SystemScript>().timer = 360f;
    }

    public void Play9Minute()
    {
        GameObject.Find("System").GetComponent<SystemScript>().timer = 540f;
    }

    public void Play10Minute()
    {
        GameObject.Find("System").GetComponent<SystemScript>().timer = 600f;
    }
}
