using UnityEngine;

public class NetScript : MonoBehaviour
{
    public int num;
    public Vector2 position;
    public bool catching = false;
    bool inWater = false;
    public GameObject target = null;

    void Start()
    {
        position = new Vector2(3, 3);
    }

    void Update()
    {
        //position = new Vector2(300, 300);
        catching = (target != null && !inWater)? true:false;

        if (catching)
        {
            target.transform.position = transform.position;
        }

        if (GameObject.Find("System").GetComponent<SystemScript>().network)
        {
            transform.position = new Vector3(position.x, -4f, position.y);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            if (catching)
            {
                target.GetComponent<FishScript>().catched = false;
                target.GetComponent<FishScript>().point = collision.transform.position;
                target = null;
            }
        }
    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Water") inWater = true;
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Water") inWater = false;
    }
}
