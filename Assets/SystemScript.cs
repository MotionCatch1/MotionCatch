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

    public static float timer = 180f;
    float time = 0f;

    int hiddenCount = 0;
    bool hidden = false;
    public static string mode = "normal";
    public int correct;
    bool generated = false;

    public static int theme = 0;
    public GameObject[] themeObj = new GameObject[4];

    public List<GameObject> fishes = new List<GameObject>();
    public int[] points = { 0, 0 };
    public GameObject result;
    private Queue<string> dataQueue = new Queue<string>();

    public bool guide = false;
    public GameObject[] guides = new GameObject[4];
    string[] fishNames =
    {
        "그라마",
        "나비 물고기",
        "흰동가리",
        "줄무늬 물고기",
        "쥐치",
        "숭어",
        "전어",
        "은어"
    };

    void Start()
    {
        udpConnect();
    }

    public void udpConnect()
    {
        udp = new UdpClient(port);
        udp.BeginReceive(new AsyncCallback(ReceiveCallback), null);
    }

    public void resetUdpSet()
    {
        port = int.Parse(GameObject.Find("Port").GetComponent<InputField>().text);
    }

    private void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, port);
            byte[] receivedBytes = udp.EndReceive(ar, ref remoteEndPoint);
            string receivedData = Encoding.ASCII.GetString(receivedBytes);

            //Debug.Log("Received Data: " + receivedData);

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
            udp.BeginReceive(new AsyncCallback(ReceiveCallback), null);
        }
    }

    private void UpdateRacketPosition(string data)
    {
        string[] parts = data.Split(new char[] { ' ', ':', ',' }, System.StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length >= 4)
        {
            int racketNumber = int.Parse(parts[1]);
            float x = float.Parse(parts[2]);
            float y = float.Parse(parts[3]);
            //Debug.Log($"{racketNumber}, {x}, {y}");

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

        foreach (GameObject obj in themeObj)
        {
            if (obj == themeObj[theme] && !themeObj[theme].activeSelf) obj.SetActive(true);
            else if (obj != themeObj[theme] && themeObj[theme].activeSelf) obj.SetActive(false);
        }

        if (gameStart)
        {
            guide = false;
            if (!generated) generateFish();

            if (mode == "mission")
            {
                GameObject guide1 = GameObject.Find("Guide 1");
                Vector3 guide1Pos = new Vector3(-2.7f, -1f, 0);
                Vector3 guide1Angle = new Vector3(0, 180f, -90f);
                if (Vector3.Distance(guide1.transform.position, guide1Pos) > 0.1f) guide1.transform.position = Vector3.Lerp(guide1.transform.position, guide1Pos, 0.02f);
                if (Quaternion.Angle(guide1.transform.rotation, Quaternion.Euler(guide1Angle)) > 0.1f) guide1.transform.rotation = Quaternion.Lerp(guide1.transform.rotation, Quaternion.Euler(guide1Angle), 0.01f);

                GameObject guide2 = GameObject.Find("Guide 2");
                Vector3 guide2Pos = new Vector3(2.7f, -1f, 0);
                Vector3 guide2Angle = new Vector3(0, 0, -90f);
                if (Vector3.Distance(guide2.transform.position, guide2Pos) > 0.1f) guide2.transform.position = Vector3.Lerp(guide2.transform.position, guide2Pos, 0.02f);
                if (Quaternion.Angle(guide2.transform.rotation, Quaternion.Euler(guide2Angle)) > 0.1f) guide2.transform.rotation = Quaternion.Lerp(guide2.transform.rotation, Quaternion.Euler(guide2Angle), 0.01f);
            }

            int catchedFishes = 0;
            foreach (int point in points) catchedFishes += point;

            foreach (GameObject element in elements)
            {
                if (element.activeSelf == false) element.SetActive(true);
            }
            if (time > 0) time -= Time.deltaTime;
            else time = 0f;
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
            GameObject.Find("Timer1").transform.GetChild(0).GetComponent<Text>().text = string.Format("{0:00}:{1:00}", minutes, seconds);
            GameObject.Find("Timer2").transform.GetChild(0).GetComponent<Text>().text = string.Format("{0:00}:{1:00}", minutes, seconds);
            //if (GameObject.Find("Hidden").activeSelf) GameObject.Find("Hidden").SetActive(false);

            if (time <= 0 || catchedFishes >= (mode == "mission" ? 700 : 1300))
            {
                if (!result.activeSelf)
                {
                    result.SetActive(true);
                    result.transform.GetChild(3).GetComponent<Text>().text = $"{points[0]}점";
                    result.transform.GetChild(4).GetComponent<Text>().text = $"{points[1]}점";
                    GameObject.Find("Time").SetActive(false);
                    StartCoroutine(Restart());
                }
            }

            while (dataQueue.Count > 0)
            {
                string data;
                lock (dataQueue)
                {
                    data = dataQueue.Dequeue();
                }
                UpdateRacketPosition(data);
            }
        }
        else if (mode == "mission" && guide)
        {
            foreach (GameObject guide in guides) guide.SetActive(true);
            GameObject.Find("GuideText1").GetComponent<Text>().text = $"{fishNames[correct]}를 잡으세요!";
            GameObject.Find("GuideText2").GetComponent<Text>().text = $"{fishNames[correct]}를 잡으세요!";

            GameObject guide1 = Instantiate(fishes[correct], new Vector3(0, -1, -1.1f), Quaternion.Euler(0, -90f, -90f));
            GameObject guide2 = Instantiate(fishes[correct], new Vector3(0, -1, 1.1f), Quaternion.Euler(0, 90f, -90f));

            guide1.name = "Guide 1";
            guide1.GetComponent<FishScript>().enabled = false;
            guide1.GetComponentInChildren<SkinnedMeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            guide1.transform.localScale = guide1.transform.localScale * 1.5f;

            guide2.name = "Guide 2";
            guide2.GetComponent<FishScript>().enabled = false;
            guide2.GetComponentInChildren<SkinnedMeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            guide2.transform.localScale = guide2.transform.localScale * 1.5f;

            guide = false;
        }
    }

    void generateFish()
    {
        time = timer;

        if (mode == "mission")
        {
            foreach (GameObject guide in guides) guide.SetActive(false);

            int[] index = new int[fishes.Count];
            for (int i = 0; i < fishes.Count; i++) index[i] = i;
            int[] others = Array.FindAll(index, num => num != correct);

            for (int i = 0; i < 13; i++)
            {
                GameObject fish = Instantiate(i < 7 ? fishes[correct] : fishes[others[UnityEngine.Random.Range(0, fishes.Count - 1)]]);
                fish.transform.parent = GameObject.Find("Fishes").transform;
                fish.transform.localPosition = new Vector3(UnityEngine.Random.Range(-1.2f, 1.2f), UnityEngine.Random.Range(-0.5f, -1.5f), UnityEngine.Random.Range(-1.2f, 1.2f));
            }
        }
        else
        {
            for (int i = 0; i < 13; i++)
            {
                GameObject fish = Instantiate(fishes[UnityEngine.Random.Range(0, fishes.Count)]);
                fish.transform.parent = GameObject.Find("Fishes").transform;
                fish.transform.localPosition = new Vector3(UnityEngine.Random.Range(-1.2f, 1.2f), UnityEngine.Random.Range(-0.5f, -1.5f), UnityEngine.Random.Range(-1.2f, 1.2f));
            }
        }

        generated = true;
    }

    public void HiddenMenu()
    {
        if (hiddenCount < 5) hiddenCount++;
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
        udp.Close();
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
