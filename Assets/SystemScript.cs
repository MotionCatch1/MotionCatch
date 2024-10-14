using System;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class SystemScript : MonoBehaviour
{
    UdpClient udp;
    public bool network = true;
    public int port = 9999;
    public bool gameStart = false;
    public List<GameObject> elements;
    public GameObject start;
    public GameObject options;
    public float timer = 180f;
    int hiddenCount = 0;
    bool hidden = false;
    public string mode = "normal";
    public GameObject[] testPrefab = new GameObject[3];

    void Start()
    {
        udp = new UdpClient(port);
        ReceiveDataAsync();
    }

    private async void ReceiveDataAsync()
    {
        while (network)
        {
            try
            {
                // 비동기 방식으로 UDP 데이터를 수신
                UdpReceiveResult result = await udp.ReceiveAsync();
                string receivedMessage = System.Text.Encoding.UTF8.GetString(result.Buffer);
                //Debug.Log("Received: " + receivedMessage);

                ConvertToVector2(receivedMessage);
            }
            catch (Exception ex)
            {
                Debug.LogError("Error receiving UDP data: " + ex.Message);
            }
        }
    }

    void ConvertToVector2(string message)
    {
        Vector2 firstVector;
        Vector2 secondVector;

        if (message.Contains(";"))
        {
            string[] parts = message.Split(';');
            string[] firstVectorValues = parts[0].Split(',');
            firstVector = new Vector2(-map(float.Parse(firstVectorValues[0]), 0, 1280, -2.85f, 2.85f), -map(float.Parse(firstVectorValues[1]), 0, 720, -1.85f, 1.85f));
            GameObject.Find("Net1").GetComponent<NetScript>().position = firstVector;
            string[] secondVectorValues = parts[1].Split(',');
            secondVector = new Vector2(-map(float.Parse(secondVectorValues[0]), 0, 1280, -2.85f, 2.85f), -map(float.Parse(secondVectorValues[1]), 0, 720, -1.85f, 1.85f));
            GameObject.Find("Net2").GetComponent<NetScript>().position = secondVector;
        }
        else
        {
            string[] firstVectorValues = message.Split(',');
            firstVector = new Vector2(-map(float.Parse(firstVectorValues[0]), 0, 1280, -2.85f, 2.85f), -map(float.Parse(firstVectorValues[1]), 0, 720, -1.85f, 1.85f));
            GameObject.Find("Net1").GetComponent<NetScript>().position = firstVector;
            GameObject.Find("Net2").GetComponent<NetScript>().position = new Vector2(30, 3);
        }
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

    void OnApplicationQuit()
    {
        network = false;
        udp.Close();
    }

    float map(float oldValue, float oldMin, float oldMax, float newMin, float newMax)
    {
        return ((oldValue - oldMin) / (oldMax - oldMin)) * (newMax - newMin) + newMin;
    }
}
