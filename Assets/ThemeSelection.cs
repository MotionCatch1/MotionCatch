using UnityEditor.Rendering;
using UnityEngine;

public class ThemeSelection : MonoBehaviour
{
    public SystemScript system;
    public int num;
    Vector2 pointer1;
    Vector2 pointer2;
    float pressTime = 0f;
    public float threshold = 180f;

    void Start()
    {
        
    }

    void Update()
    {
        pointer1 = system.pointer1;
        pointer2 = system.pointer2;

        RectTransform transform = GetComponent<RectTransform>();
        if (pressTime < threshold)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(transform, pointer1) || RectTransformUtility.RectangleContainsScreenPoint(transform, pointer2))
            {
                pressTime += Time.deltaTime;
            }
            else if (pressTime > 0) pressTime -= Time.deltaTime;
        }
        else {
            SystemScript.theme = num;
            pressTime = 0;
        }
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
