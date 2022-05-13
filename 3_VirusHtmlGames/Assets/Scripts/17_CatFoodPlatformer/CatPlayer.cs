using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatPlayer : MonoBehaviour
{
    [SerializeField] private CatFoodPlatformerScript CS;
    private bool isFacingRight = true;
    [SerializeField] private Sprite[] catSprites;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(CS.isPlaying)
        {
            if(rb.bodyType == RigidbodyType2D.Kinematic)
                rb.bodyType = RigidbodyType2D.Dynamic;

            Vector3 lastPos = transform.position;

            Vector3 screenMouspos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if(Input.touchCount > 0)
            {
                screenMouspos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            }

            transform.position = new Vector3(screenMouspos.x, transform.position.y, 0);

            float x = transform.position.x - lastPos.x;
            if (x > 0 && !isFacingRight) {
                Flip ();
            } else if (x < 0 && isFacingRight) {
                Flip ();
            }

            float y = rb.velocity.y;
            if (y > 0) {
                GetComponent<SpriteRenderer>().sprite = catSprites[0];
            } else if (y < 0) {
                GetComponent<SpriteRenderer>().sprite = catSprites[1];
            }
        }
        
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
		Vector3 newScale = transform.localScale;
		newScale.x *= -1f;
		transform.localScale = newScale;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Snack"))
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity = Vector2.zero;
            GetComponent<CircleCollider2D>().enabled = false;
            CS.EndSequence();
        }
    }
}
