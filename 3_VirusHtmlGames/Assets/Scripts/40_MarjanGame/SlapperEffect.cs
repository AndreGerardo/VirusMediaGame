using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlapperEffect : MonoBehaviour
{
    [SerializeField] private float animSpeed = 0.05f;
    [SerializeField] private GameObject slapObj;

    void Start()
    {
        LeanTween.rotate(gameObject, new Vector3(0f, 0f, 15f), animSpeed)
            .setOnComplete(() =>
            {
                Instantiate(slapObj, transform.position - new Vector3(4.08f, -0.42f, 0f), Quaternion.identity);
                Destroy(gameObject, 0.25f);
            });
    }

}
