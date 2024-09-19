using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScript : MonoBehaviour
{
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void GameStart()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(true);
        StartCoroutine(CountDown(transform.GetChild(2).GetComponent<Text>()));
    }

    private IEnumerator CountDown(Text count)
    {
        int countdown = 3;
        while (countdown > 0)
        {
            count.text = countdown.ToString();
            yield return new WaitForSeconds(1f);
            countdown --;
        }
        gameObject.SetActive(false);
        GameObject.Find("System").GetComponent<SystemScript>().gameStart = true;
    }
}
