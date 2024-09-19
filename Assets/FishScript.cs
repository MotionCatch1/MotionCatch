using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishScript : MonoBehaviour
{
    Vector2 destination = Vector2.zero;

    void Start()
    {
        InvokeRepeating("SetDestination", 3f, 3f);
    }

    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, destination, 0.01f);
    }

    void SetDestination()
    {
        float x = Random.Range(0, Screen.width);
        float y = Random.Range(0, Screen.height);
        destination = new Vector2(x, y);
    }
}
