using UnityEngine;

public class NetScript : MonoBehaviour
{
    public int num;
    public Vector2 position;
    public bool catching = false;
    public GameObject target = null;

    void Start()
    {
        position = new Vector2(3, 3);
    }

    void Update()
    {
        //position = new Vector2(300, 300);
        catching = target != null? true:false;

        if (catching)
        {
            target.transform.position = transform.position;
        }

        transform.position = new Vector3(position.x, -4f, position.y);
    }
}
