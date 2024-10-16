using System.Collections;
using UnityEngine;

public class FishScript : MonoBehaviour
{
    float speed = 0;
    public float rotationSpeed = 1f;
    public Vector3 point = Vector3.zero;
    Vector3 destination;
    bool catched = false;

    void Start()
    {
        GetComponent<Animator>().SetBool("isSwimming", true);
        StartCoroutine(ChangeDestination());
    }


    void Update()
    {
        if (!catched)
        {
            Vector3 direction = destination - transform.position;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
            speed = Vector3.Distance(destination, transform.position) / 3f;
            GetComponent<Animator>().speed = speed;
            transform.Translate(0, 0, speed * Time.deltaTime);
        }

        if (GameObject.Find("System").GetComponent<SystemScript>().gameStart)
        {
            GameObject[] nets = { GameObject.Find("Net1"), GameObject.Find("Net2") };
            foreach (GameObject net in nets)
            {
                Vector2 netPos = new Vector2(net.transform.position.x, net.transform.position.z);
                Vector2 fishPos = new Vector2(transform.position.x, transform.position.z);
                //Debug.Log(Vector2.Distance(netPos, fishPos));
                if (!net.GetComponent<NetScript>().catching && Vector2.Distance(netPos, fishPos) < 0.1f)
                {
                    net.GetComponent<NetScript>().target = this.gameObject;
                    catched = true;
                }
            }
        }
    }

    IEnumerator ChangeDestination()
    {
        while (true)
        {
            destination = point + new Vector3(Random.Range(-1.9f, 1.9f), Random.Range(-4.5f, -5.5f), Random.Range(-0.8f, 0.8f));
            yield return new WaitForSeconds(Random.Range(2, 4));
        }
    }
}
