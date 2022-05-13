using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchDSnacksPlayer : MonoBehaviour
{
    public CatchDSnacksScript gameScript;


    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Snack"))
        {
            gameScript.NumOfSnacks--;
        }
    }
}
