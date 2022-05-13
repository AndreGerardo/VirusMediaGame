using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotdogPieceDrop : MonoBehaviour
{
    [SerializeField] private bool isFalling = true;
    [SerializeField] private float moveSpeed = 2.5f;

    void Update()
	{
        if(BuildaHotdogScript.instance.isPlaying && isFalling)
		    transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        isFalling = false;
        transform.SetParent(coll.transform);
        GetComponent<BoxCollider2D>().enabled = false;
        BuildaHotdogScript.instance.piecesStacked--;
    }
}
