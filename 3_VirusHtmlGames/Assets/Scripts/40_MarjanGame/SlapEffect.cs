using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlapEffect : MonoBehaviour
{
    [SerializeField] private float animSpeed = 0.25f;

    void Start()
    {
        LeanTween.scale(gameObject, Vector3.one * 1.25f, animSpeed)
            .setLoopPingPong(2)
            .setOnComplete(() =>
            {
                DestroyAnimation();
            });
    }

    void DestroyAnimation()
    {
        LeanTween.alpha(gameObject, 0f, animSpeed)
            .setDelay(0.5f)
            .setOnComplete(() =>
            {
                Destroy(gameObject);
            });
    }
}
