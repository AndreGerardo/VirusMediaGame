using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneEnemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.5f;

    void Update()
    {
        if (MovieGameScript.instance.isPlaying)
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

    }
}
