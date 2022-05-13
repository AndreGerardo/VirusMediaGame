using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatCameraFollow : MonoBehaviour
{
    [SerializeField] private CatFoodPlatformerScript CS;
    public Transform target;

	void LateUpdate () {
		if (target.position.y > transform.position.y)
		{
			Vector3 newPos = new Vector3(transform.position.x, target.position.y, transform.position.z);
			transform.position = newPos;
		}

        if(Mathf.Abs(transform.position.y - target.position.y) > 10f && CS.isPlaying)
        {
            CS.EndSequence();
        }
	}
}
