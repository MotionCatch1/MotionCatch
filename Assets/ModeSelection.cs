using UnityEngine;

public class ModeSelection : MonoBehaviour
{
<<<<<<< Updated upstream
=======
    public Sprite[] countdownImage;

>>>>>>> Stashed changes
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void NormalMode()
    {
<<<<<<< Updated upstream
        GameObject.Find("System").GetComponent<SystemScript>().mode = "normal";
=======
        SystemScript.mode = "normal";
>>>>>>> Stashed changes
    }

    public void SpeedMode()
    {
<<<<<<< Updated upstream
        GameObject.Find("System").GetComponent<SystemScript>().mode = "speed";
=======
        SystemScript.mode = "speed";
>>>>>>> Stashed changes
    }

    public void MissionMode()
    {
        SystemScript.mode = "mission";
    }
}
