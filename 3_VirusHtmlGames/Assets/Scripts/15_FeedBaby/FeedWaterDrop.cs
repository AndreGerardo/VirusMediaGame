using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedWaterDrop : MonoBehaviour
{
    
    void OnCollisionEnter2D(Collision2D coll)
    {
        Destroy(gameObject);
    }

}
