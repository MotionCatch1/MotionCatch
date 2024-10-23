using UnityEngine;

public class BackgroundSelector : MonoBehaviour
{

    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(Random.Range(0, 4)).gameObject.SetActive(true);
    }

    void Update()
    {
        
    }
}
