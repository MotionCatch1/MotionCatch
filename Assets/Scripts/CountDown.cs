using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public Sprite[] countdownImage;
    private Image countImage;

    private void Awake()
    {
        countImage = GetComponent<Image>();
    }

    public void StartCountdown()
    {
        StartCoroutine(CountDownImage());
    }

    private IEnumerator CountDownImage()
    {
        int countdown = 3;
        while (countdown > 0)
        {
            countImage.sprite = countdownImage[countdown - 1];
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        // 카운트다운이 끝난 후 게임 시작
        gameObject.SetActive(false);
        GameObject.Find("System").GetComponent<SystemScript>().gameStart = true;
    }
}