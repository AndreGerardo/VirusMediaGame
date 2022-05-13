using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarjanDetector : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Enemy") && MarjanGameScript.instance.isPlaying)
        {
            MarjanGameScript.instance.EndSequence();
        }
    }
}
