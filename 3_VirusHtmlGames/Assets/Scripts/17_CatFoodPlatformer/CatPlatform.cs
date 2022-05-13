using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatPlatform : MonoBehaviour
{
    public float jumpForce = 1000f;

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.relativeVelocity.y <= 0f)
		{
			Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
			if (rb != null)
			{
				Vector2 velocity = rb.velocity;
				velocity.y = jumpForce * Time.deltaTime;
				rb.velocity = velocity;
			}
		}
	}
}
