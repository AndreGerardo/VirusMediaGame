using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterObjectMover : MonoBehaviour
{
    [SerializeField] private bool isEnemy = false;
    [SerializeField] private GameObject explodePrefab;
    [SerializeField] private float verticalDir = 1f;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float lifetime = 3f;

    private void OnEnable()
    {
        Invoke("SwitchObject", lifetime);
    }

    void OnDisable()
	{
		CancelInvoke ();
	}

    void Update()
	{
		transform.Translate(Vector3.up * moveSpeed * verticalDir * Time.deltaTime);
	}

    void SwitchObject()
	{
        GameObject obj = (GameObject)Instantiate(explodePrefab, transform.position, Quaternion.identity);
        Destroy(obj, 0.5f);
		gameObject.SetActive (false);
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(isEnemy)
        {
            if(coll.CompareTag("Player"))
            {
                SwitchObject();
                ShooterGameScript.instance.EndSequence();
            }
        }else
        {
            if(coll.CompareTag("Enemy"))
            {
                SwitchObject();
                Destroy(coll.gameObject);
                ShooterGameScript.instance.NumOfEnemies--;
            }
        }
    }

}
