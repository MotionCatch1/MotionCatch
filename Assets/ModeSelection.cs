using UnityEngine;

public class ModeSelection : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void NormalMode()
    {
        GameObject.Find("System").GetComponent<SystemScript>().mode = "normal";
    }

    public void SpeedMode()
    {
        GameObject.Find("System").GetComponent<SystemScript>().mode = "speed";
    }

    public void MissionMode()
    {
        GameObject.Find("System").GetComponent<SystemScript>().mode = "mission";
    }
}
