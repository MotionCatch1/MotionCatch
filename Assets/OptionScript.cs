using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionScript : MonoBehaviour
{
    public SystemScript system;
    public bool option = false;
    public GameObject options;
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
            if (option) options.SetActive(false);
            else options.SetActive(true);
            pressTime = 0;
        }
    }
}
