using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundSelector : MonoBehaviour
{
    public Sprite[] backgroundImg;

    void Start()
    {
        GetComponent<Image>().sprite = backgroundImg[Random.Range(0, backgroundImg.Length)];
    }

    void Update()
    {
        
    }
}
