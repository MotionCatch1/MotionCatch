using UnityEngine;

public class NetScript : MonoBehaviour
{
    public int num;
    public Vector2 position;

    void Start()
    {
        position = new Vector2(300, 300);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(position.x, 0, position.y);
    }
}
