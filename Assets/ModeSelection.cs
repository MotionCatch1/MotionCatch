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
        SystemScript.mode = "normal";
    }

    public void SpeedMode()
    {
        SystemScript.mode = "speed";
    }

    public void MissionMode()
    {
        SystemScript.mode = "mission";
    }
}
