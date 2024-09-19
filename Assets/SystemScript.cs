using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class SystemScript : MonoBehaviour
{
    public bool gameStart = false;
    public GameObject[] elements;
    public GameObject start;
    public GameObject options;
    public float timer = 180f;
    int hiddenCount = 0;
    bool hidden = false;
    public string mode = "normal";
    public GameObject[] testPrefab = new GameObject[3];

    void Start()
    {
        
    }

    void Update()
    {
        if (gameStart)
        {
            foreach (GameObject element in elements)
            {
                if (element.activeSelf == false) element.SetActive(true);
            }
            if (timer > 0) timer -= Time.deltaTime;
            else timer = 0f;
            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer % 60);
            GameObject.Find("Time").GetComponent<Text>().text = string.Format("{0:00}:{1:00}", minutes, seconds);

            if (mode == "normal")
            {
                if (GameObject.Find("Test") == null)
                {
                    GameObject test = Instantiate(testPrefab[0]);
                    test.name = "Test";
                }
            }
            else if (mode == "speed")
            {
                if (GameObject.Find("Test") == null)
                {
                    GameObject test = Instantiate(testPrefab[1]);
                    test.name = "Test";
                }
            }
            else if (mode == "mission")
            {
                if (GameObject.Find("Test") == null)
                {
                    GameObject test = Instantiate(testPrefab[2]);
                    test.name = "Test";
                }
            }
        }
    }

    public void HiddenMenu()
    {
        if (hiddenCount < 5) hiddenCount ++;
        else
        {
            if (hidden)
            {
                start.SetActive(true);
                options.SetActive(false);
                hidden = false;
                hiddenCount = 0;
            }
            else
            {
                start.SetActive(false);
                options.SetActive(true);
                hidden = true;
                hiddenCount = 0;
            }   
        }
    }
}
