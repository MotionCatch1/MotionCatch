using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountScript : MonoBehaviour
{
    public Sprite[] countdownImage;

    void Start()
    {
        StartCoroutine(CountDown(GetComponent<Image>()));
    }

    void Update()
    {
        
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