using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinObject : MonoBehaviour
{
    
    [SerializeField] private DrinkProductScript FS;
    private Vector3 startObjPos;
    [SerializeField] private Transform targetPos;
    private float objToTargetDist;
    [SerializeField] private float maxScale, minScale;
    private bool isDragging = false;
    private bool objectSetCheck = true;
    private void OnMouseDown()
    {
        isDragging = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private void Start()
    {
        objToTargetDist = Vector3.Distance(transform.position, targetPos.position);
    }

    void Update()
    {
        CheckTargetRange();
        if(FS.isPlaying && objectSetCheck)
        {
            startObjPos = transform.position;
            objectSetCheck = false;
        }

        if(isDragging)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePos);
        }else if(!isDragging && FS.isPlaying)
        {
            transform.position = startObjPos;
            transform.localScale = new Vector3(maxScale,maxScale,maxScale);
        }
    }

    void CoinEndSequence()
    {
        isDragging = false;
        FS.isPlaying = false;
        GetComponent<CircleCollider2D>().enabled = false;
        FS.StopTutorial();

        LeanTween.alpha(gameObject, 0f, 1f)
            .setEaseInOutSine()
            .setOnComplete(()=>FS.EndSequence());
    }

    void CheckTargetRange()
    {
        float dist = Vector3.Distance(transform.position, targetPos.position);
        float scale = Mathf.Lerp(minScale, maxScale, Mathf.InverseLerp(0f, objToTargetDist, dist)); 
        transform.localScale = new Vector3(scale, scale, scale);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Snack"))
        {
            CoinEndSequence();
        }
    }

}
