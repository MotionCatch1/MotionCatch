using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartScript : MonoBehaviour
{
    public Sprite[] countdownImages = new Sprite[3];
    
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
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(true);
        StartCoroutine(CountDown(transform.GetChild(3).GetComponent<Image>()));
    }

    private IEnumerator CountDown(Image countImage)
    {
        int countdown = 3;
        while (countdown > 0)
        {
            countImage.sprite = countdownImages[countdown - 1];
            yield return new WaitForSeconds(1f);
            countdown --;
        }
        gameObject.SetActive(false);
        GameObject.Find("System").GetComponent<SystemScript>().gameStart = true;
    }
}
