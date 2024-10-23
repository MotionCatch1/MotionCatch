using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ModeSelection : MonoBehaviour
{
    public Sprite[] countdownImage;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void NormalMode()
    {
        var systemScript = GameObject.Find("System").GetComponent<SystemScript>();
        systemScript.mode = "normal";
        systemScript.gameStart = true;
        GameObject.Find("Options").SetActive(false);
        StartCoroutine(CountDown(transform.GetChild(3).GetComponent<Image>()));
    }

    public void SpeedMode()
    {
        var systemScript = GameObject.Find("System").GetComponent<SystemScript>();
        systemScript.mode = "speed";
        systemScript.gameStart = true;
        GameObject.Find("Options").SetActive(false);
        StartCoroutine(CountDown(transform.GetChild(3).GetComponent<Image>()));
    }

    public void MissionMode()
    {
        GameObject.Find("System").GetComponent<SystemScript>().mode = "mission";
    }

    private IEnumerator CountDown(Image countImage)
    {
        int countdown = 3;
        while (countdown > 0)
        {
            countImage.sprite = countdownImage[countdown - 1];
            yield return new WaitForSeconds(1f);
            countdown--;
        }
        gameObject.SetActive(false);
        GameObject.Find("System").GetComponent<SystemScript>().gameStart = true;
    }
}
