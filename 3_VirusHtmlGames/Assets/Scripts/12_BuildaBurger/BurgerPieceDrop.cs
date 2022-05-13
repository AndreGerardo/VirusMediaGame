using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerPieceDrop : MonoBehaviour
{
    [SerializeField] private bool isFalling = true;
    [SerializeField] private Transform attachObject;
    [SerializeField] private float moveSpeed = 2.5f;

    void Update()
	{
        if(BuildAMealScript.instance.isPlaying && isFalling)
		    transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        isFalling = false;
        transform.SetParent(coll.transform);
        GetComponent<BoxCollider2D>().enabled = false;
        BuildAMealScript.instance.piecesStacked--;
    }
}
