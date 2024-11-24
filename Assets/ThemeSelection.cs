using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ThemeSelection : MonoBehaviour
{
    public SystemScript system;
    public int num;
    Vector2 pointer1;
    Vector2 pointer2;
    float pressTime = 0f;
    public float threshold = 3f;
    public bool selected = false;
    public List<GameObject> others = new List<GameObject>();

    void Start()
    {
        foreach (Transform child in transform.parent) if (child.gameObject != this.gameObject) others.Add(child.gameObject);
    }

    void Update()
    {
        if (selected) GetComponent<UnityEngine.UI.Image>().color = Color.gray;
        else GetComponent<UnityEngine.UI.Image>().color = Color.white;
        Debug.Log($"Theme: {num}, {pressTime}");

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
            selected = true;
            foreach (GameObject other in others) other.GetComponent<ThemeSelection>().selected = false;
            pressTime = 0;
        }
    }
}
