using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductBasket : MonoBehaviour
{
    [SerializeField] private ProductToBasketScript PS;
    [SerializeField] private float s = 1.2f;
    [SerializeField] private LeanTweenType basketTweenType;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Snack"))
        {
            PS.productCount--;
            LeanTween.scale(gameObject, new Vector3(s,s,s), 0.25f)
                .setLoopPingPong(1)
                .setEase(basketTweenType);
        }
    }
}
