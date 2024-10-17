using UnityEngine;

public class PointScript : MonoBehaviour
{
    public int num;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Net")
        {
            if (collision.GetComponent<NetScript>().catching)
            {
                Debug.Log($"point for Player {num + 1}");
                GameObject.Find("System").GetComponent<SystemScript>().points[num]++;
            }
        }
    }
}
