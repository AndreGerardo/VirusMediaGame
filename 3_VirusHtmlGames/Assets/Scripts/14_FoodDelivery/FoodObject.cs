using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodObject : MonoBehaviour
{
    [SerializeField] private FoodDeliveryScript FS;
    [SerializeField] private int FoodCode;
    private Vector3 startObjPos;
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

    void Update()
    {
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
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        
            if(coll.name == "house_1")
            {
                SendGojek(0);
            }else if(coll.name == "house_2")
            {
                SendGojek(1);
            }else if(coll.name == "house_3")
            {
                SendGojek(2);
            }
        
    }

    void SendGojek(int index)
    {
        transform.position = startObjPos;
        if(FS.houseFoodCode[index] == FoodCode && FS.bikeCanDeliver)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            FS.houseFoodCode[index] = -1;
            isDragging = false;
            gameObject.SetActive(false);
            switch (index)
            {
                case 0 : FS.House1Waypoint(); break;
                case 1 : FS.House2Waypoint(); break;
                case 2 : FS.House3Waypoint(); break;
            }
        }else if(FS.houseFoodCode[index] == FoodCode && !FS.bikeCanDeliver)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            FS.houseFoodCode[index] = -1;
            isDragging = false;
            gameObject.SetActive(false);
            FS.deliveryQueue.Enqueue(index);
        }else if(FS.houseFoodCode[index] != FoodCode)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}
