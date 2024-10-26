using UnityEngine;

public class ThemeSelection : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetSpring()
    {
        SystemScript.theme = 0;
    }

    public void SetSummer()
    {
        SystemScript.theme = 1;
    }

    public void SetAutumn()
    {
        SystemScript.theme = 2;
    }

    public void SetWinter()
    {
        SystemScript.theme = 3;
    }
}
