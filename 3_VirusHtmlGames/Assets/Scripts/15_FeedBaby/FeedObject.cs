using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedObject : MonoBehaviour
{
    [Header("Object Params")]
    [SerializeField] private FeedBabyScript FB;
    [SerializeField] private bool isThermos = false;
    private Vector3 startObjPos;
    private bool isDragging = false;
    [SerializeField]private bool isInPlateArea = false;
    private bool objectSetCheck = true;
    
    [Header("Spawn Params")]
    [SerializeField] private Transform spawnpoint;
    [SerializeField] private GameObject spawnObjectPrefab;
    [SerializeField] private float spawnRate = 0.67f;
    [SerializeField] private int objCapacity = 2;
    private List<GameObject> objCollection = new List<GameObject>();
    [SerializeField] private float timer = 0f;
    

    private void OnMouseDown()
    {
        isDragging = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    void Update()
    {
        if(FB.isPlaying)
        {
            if(objectSetCheck)
            {
                startObjPos = transform.position;
                objectSetCheck = false;
            }

            if(isDragging)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector3(mousePos.x, mousePos.y, 0);

                if(isInPlateArea)
                {
                    timer += Time.deltaTime;
                    if(timer >= spawnRate)
                    {
                        timer = 0f;

                        if((isThermos && FB.currentStage == 1) || (!isThermos && FB.currentStage == 0))
                            SpawnObject();
                    }
                }

            }else if(!isDragging)
            {
                transform.position = startObjPos;
                transform.rotation = Quaternion.identity;
                isInPlateArea = false;
            }

            if(!isThermos && objCapacity <= 0 && FB.currentStage == 0)
            { FB.currentStage = 1; GetComponent<SpriteRenderer>().enabled = false; }
            if(isThermos && objCapacity <= 0 && FB.currentStage == 1)
            { FB.currentStage = 2; Destroy(gameObject); }

            if(FB.currentStage == 2)
            {
                if(objCollection.Count > 0)
                {
                    for(int i = 0; i < objCollection.Count; i++)
                    {
                        Destroy(objCollection[i]);
                    }
                }
            }

        }else
        {
            if(FB.currentStage == 2)
            {
                transform.position = startObjPos;
                transform.rotation = Quaternion.identity;
                isInPlateArea = false;
            }
        }
        
    }

    private void SpawnObject()
    {
        GameObject obj = (GameObject)Instantiate(spawnObjectPrefab);
        obj.transform.position = spawnpoint.position;
        obj.transform.rotation = Quaternion.identity;

        objCollection.Add(obj);

        objCapacity--;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        
            if(isDragging && coll.name == "PlateArea" && !isThermos)
            {
                transform.rotation = Quaternion.Euler(0,0,-125);
                isInPlateArea = true;
            }else if(isDragging && coll.name == "PlateArea" && isThermos)
            {
                transform.rotation = Quaternion.Euler(0,0,100);
                isInPlateArea = true;
            }
        
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if(isDragging && coll.name == "PlateArea")
            Debug.Log("ExitArea");
            transform.rotation = Quaternion.Euler(0,0,0);
            isInPlateArea = false;
    }
}
