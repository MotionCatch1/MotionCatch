using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartScript : MonoBehaviour
{
    public SystemScript system;
    public GameObject count;
    public Sprite[] countdownImage;
    public GameObject gameStart;
    public int num;
    Vector2 pointer1;
    Vector2 pointer2;
    float pressTime = 0f;
    public float threshold = 180f;

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
            count.SetActive(true);
            StartCoroutine(CountDown(count.GetComponent<Image>()));

            if (SystemScript.mode == "mission")
            {
                system.correct = Random.Range(0, system.fishes.Count);
                system.guide = true;
            }

            pressTime = 0;
            gameStart.SetActive(false);
        }
    }

    public void GameStart()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(true);
        StartCoroutine(CountDown(transform.GetChild(3).GetComponent<Image>()));

        if (SystemScript.mode == "mission")
        {
            SystemScript system = GameObject.Find("System").GetComponent<SystemScript>();
            system.correct = Random.Range(0, system.fishes.Count);
            system.guide = true;
        }
    }

    private IEnumerator CountDown(Image countImage)
    {
        int countdown = 3;
        while (countdown > 0)
        {
            countImage.sprite = countdownImage[countdown - 1];
            yield return new WaitForSeconds(1f);
            countdown --;
        }
        gameObject.SetActive(false);
        GameObject.Find("System").GetComponent<SystemScript>().gameStart = true;
    }
}
