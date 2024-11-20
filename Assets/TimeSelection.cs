using UnityEngine;

public class TimeSelection : MonoBehaviour
{
    public SystemScript system;
    public int num;
    Vector2 pointer1;
    Vector2 pointer2;
    float pressTime = 0f;
    public float threshold = 3f;

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
        else
        {
            SystemScript.theme = num;
            pressTime = 0;
        }
    }
}
