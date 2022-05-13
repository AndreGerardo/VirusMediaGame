using UnityEngine;

public class CatchPlayerObj : MonoBehaviour
{
    public CatchGameScript gameScript;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Snack"))
        {
            gameScript.NumOfSnacks--;
        }
    }
}
