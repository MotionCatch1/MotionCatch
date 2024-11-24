using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class StartScript : MonoBehaviour
{
    public SystemScript system;
    public GameObject count;
    public GameObject gameStart;
    public GameObject options;
    public string mode;
    Vector2 pointer1;
    Vector2 pointer2;
    float pressTime = 0f;
    public float threshold = 3f;

    void Start()
    {
        
    }

    void Update()
    {
        if (!options.activeSelf)
        {
            pointer1 = system.pointer1;
            pointer2 = system.pointer2;

            RectTransform transform = GetComponent<RectTransform>();
            if (pressTime < threshold)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(transform, pointer1) || RectTransformUtility.RectangleContainsScreenPoint(transform, pointer2))
                {
                    pressTime += Time.deltaTime;
                    Debug.Log($"{mode}:{pressTime}");
                }
                else if (pressTime > 0) pressTime -= Time.deltaTime;
            }
            else
            {
                SystemScript.mode = mode;
                count.SetActive(true);

                if (SystemScript.mode == "mission")
                {
                    system.correct = Random.Range(0, system.fishes.Count);
                    system.guide = true;
                }

                pressTime = 0;
                gameStart.SetActive(false);
            }
        }
    }
}
