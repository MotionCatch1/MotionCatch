using System.Collections;
using UnityEngine;

public class FishScript : MonoBehaviour
{
    public int num;
    public bool catchable = true;
    float speed = 0;
    public float rotationSpeed = 1.2f;
    public Vector3 point = Vector3.zero;
    public Vector3 destination;
    public bool catched = false;

    void Start()
    {
        //GetComponent<Animator>().SetBool("isSwimming", true);
        StartCoroutine(ChangeDestination());
    }


    void Update()
    {
        rotationSpeed = SystemScript.mode == "speed" ? 2f : 1.2f;

        if (!catched)
        {
            Vector3 direction = destination - transform.position;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            if(SystemScript.mode == "speed")
            {
                speed = Vector3.Distance(destination, transform.position) / 1.5f;
            }
            else
            {
                speed = Vector3.Distance(destination, transform.position) / 3f;
            }

            
            GetComponent<Animator>().speed = speed * rotationSpeed;
            transform.Translate(0, 0, speed * Time.deltaTime);
        }

        if (GameObject.Find("System").GetComponent<SystemScript>().gameStart && catchable)
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
            destination = point.x + point.y + point.z == 0 ? new Vector3(Random.Range(-1.9f, 1.9f), Random.Range(-4.5f, -5.5f), Random.Range(-0.8f, 0.8f)) : point + new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-1.5f, -0.5f), Random.Range(-0.3f, 0.3f));
            yield return new WaitForSeconds(SystemScript.mode == "speed" ? Random.Range(1f, 3f) : Random.Range(2f, 4f));
        }
    }
}
