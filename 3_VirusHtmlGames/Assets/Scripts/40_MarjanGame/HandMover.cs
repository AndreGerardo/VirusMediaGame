using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7.5f;
    [SerializeField] private bool isRightHand = true;
    private Transform target;

    private Rigidbody2D rb;
    private int animID;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = MarjanGameScript.instance.enemyTarget;
        Vector3 targ = target.position;
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        if (isRightHand)
        {
            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 180f));
        }
        else
        {
            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        animID = LeanTween.move(gameObject, target.position, moveSpeed).id;
    }

    void OnMouseDown()
    {
        LeanTween.cancel(animID);
        Destroy(gameObject, 1f);
    }
}
