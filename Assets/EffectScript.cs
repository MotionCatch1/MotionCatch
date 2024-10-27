using UnityEngine;

public class EffectScript : MonoBehaviour
{
    float opacity = 1f;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (opacity < 0) GameObject.Destroy(this.gameObject);

        transform.Translate(Vector3.up * 0.005f);
        Color color = new Color(1f, 1f, 1f, opacity);
        GetComponent<MeshRenderer>().material.SetColor("_Color", color);
        opacity -= 0.01f;
    }
}
