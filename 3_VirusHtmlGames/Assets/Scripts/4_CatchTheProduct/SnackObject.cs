using UnityEngine;

public class SnackObject : MonoBehaviour
{
    void Update()
    {
        if(transform.position.y < -6f)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
