using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public float timer = 10f;
    int hiddenCount = 0;
    bool hidden = false;
    public string mode = "normal";
    public GameObject[] testPrefab = new GameObject[3];
    public int[] points = { 0, 0 };
    public GameObject result;
    bool restarting = false;
    private Queue<string> dataQueue = new Queue<string>();  

    void Start()
    {
        udp = new UdpClient(port);
        udp.BeginReceive(new AsyncCallback(ReceiveCallback), null);
    }

    private void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, port);
            byte[] receivedBytes = udp.EndReceive(ar, ref remoteEndPoint);
            string receivedData = Encoding.ASCII.GetString(receivedBytes);

            Debug.Log("Received Data: " + receivedData); // 수신된 데이터 로그 출력

            // 큐에 데이터 추가
            lock (dataQueue)
            {
                dataQueue.Enqueue(receivedData);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error receiving UDP data: " + ex.Message);
        }
        finally
        {
            udp.BeginReceive(new AsyncCallback(ReceiveCallback), null); // 다시 수신 대기
        }
    }



    private void UpdateRacketPosition(string data)
    {
        // parts 배열을 만드는데 필요한 구문을 그대로 유지
        string[] parts = data.Split(new char[] { ' ', ':', ',' }, System.StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length >= 4)
        {
            int racketNumber = int.Parse(parts[1]);
            float x = float.Parse(parts[2]);
            float y = float.Parse(parts[3]);
            Debug.Log($"{racketNumber}, {x}, {y}");

            // 위치 업데이트 로직
            if (racketNumber == 1)
            {
                GameObject.Find("Net1").GetComponent<NetScript>().position = new Vector2(-map(x, 0, 1280, 6f, -6f), -map(y, 0, 720, -4f, 4f));
            }
            else if (racketNumber == 2)
            {
                GameObject.Find("Net2").GetComponent<NetScript>().position = new Vector2(-map(x, 0, 1280, 6f, -6f), -map(y, 0, 720, -4f, 4f));
            }
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
            //if (GameObject.Find("Hidden").activeSelf) GameObject.Find("Hidden").SetActive(false);

            if (timer <= 0)
            {
                if (!result.activeSelf)
                {
                    result.SetActive(true);
                    result.transform.GetChild(1).GetComponent<Text>().text = $"Player 1: {points[0]}\nPlayer 2: {points[1]}";
                    StartCoroutine(Restart());
                }
            }

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

            while (dataQueue.Count > 0)
            {
                string data;
                lock (dataQueue)
                {
                    data = dataQueue.Dequeue(); // 큐에서 데이터 꺼내기
                }
                UpdateRacketPosition(data); // 데이터로 라켓 위치 업데이트
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

    IEnumerator Restart()
    {
        restarting = true;
        if (restarting)
        {
            udp.Dispose();
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        restarting = false;
    }
}
