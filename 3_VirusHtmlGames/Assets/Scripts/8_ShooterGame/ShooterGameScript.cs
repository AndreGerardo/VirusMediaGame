using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShooterGameScript : MonoSingleton<ShooterGameScript>
{
    [Header("UI Reference")]
    [SerializeField] private TMP_Text remainingEnemiesText;
    [SerializeField] private RectTransform logoUI, mainMenuUI, endCardUI, endCardButton, remainingTextUI;

    [Header("Tween Params")]
    [SerializeField] private LeanTweenType easeType;
    private int animId;

    [Header("Game Params")]
    [SerializeField] private int _numOfEnemies = 5;
    public int NumOfEnemies{
        get { return _numOfEnemies; } set { _numOfEnemies = value; remainingEnemiesText.text = _numOfEnemies.ToString(); }
    }
    [SerializeField] private GameObject playerObj, SpawnerObj;
    [SerializeField] private GameObject[] objects;
    private float timer = 0f;
    [SerializeField] private bool isPlaying = false;
    private List<GameObject> bulletList = new List<GameObject> ();
	[SerializeField] private GameObject bulletPrefab;
	private bool canIncreaseBullet = true;
	private int bulletCount = 10;
    [SerializeField] private GameObject[] enemies;

    void Start()
    {
        animId = LeanTween.moveY(logoUI, -36, 0.25f)
            .setEase(LeanTweenType.easeOutSine)
            .setLoopPingPong().id;

		for (int i = 0; i < bulletCount; i++) {
			GameObject obj = (GameObject)Instantiate (bulletPrefab);
			obj.SetActive (false);
			bulletList.Add (obj);
		}
    }

    void Update()
    {
        if(isPlaying)
        {  
            timer += Time.deltaTime;
            Vector3 screenMouspos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if(Input.touchCount > 0)
            {
                screenMouspos = Input.GetTouch(0).position;
            }

            playerObj.transform.position = new Vector3(screenMouspos.x, playerObj.transform.position.y, playerObj.transform.position.z);

            if(timer >= 0.5f)
            {
                Shoot();
                SpawnEnemy();
                timer = 0f;
            }

            if(NumOfEnemies <= 0)
            {
                EndSequence();
            }
        }
    }

    public void PlaySequence()
    {
        LeanTween.cancel(animId);
        LeanTween.moveY(logoUI, 480f, 1f)
            .setEase(easeType);
        LeanTween.moveY(remainingTextUI, 80f, 1f)
            .setEase(easeType);
        LeanTween.moveY(mainMenuUI, -480f, 1f)
            .setEase(easeType)
            .setOnComplete(()=>isPlaying = true);
    }

    public void EndSequence()
    {
        isPlaying = false;
        LeanTween.moveY(playerObj, -16f, 1f)
            .setEase(easeType)
            .setDelay(1f);
        LeanTween.moveY(endCardUI, 0f, 1f)
            .setEase(easeType)
            .setDelay(1f);
        LeanTween.moveY(remainingTextUI, 480f, 1f)
            .setEase(easeType)
            .setDelay(1f);
        LeanTween.scale(endCardButton, new Vector3(1.2f,1.2f,1.2f), 0.5f)
            .setEase(easeType)
            .setLoopPingPong()
            .setDelay(2f);
    }

    public GameObject SpawnBullet()
	{
		for (int i = 0; i < bulletList.Count; i++) {
			if (bulletList[i].activeInHierarchy == false) {
				return bulletList [i];
			}
		}

		if (canIncreaseBullet) {
			GameObject obj = (GameObject)Instantiate (bulletPrefab);
			bulletList.Add (obj);
			return obj;
		}

		return null;
	}

    private void Shoot()
    {
        GameObject obj = (GameObject)SpawnBullet();

		if (obj == null) {
			return;
		}

		obj.transform.position = playerObj.transform.position;
		obj.transform.rotation = Quaternion.identity;
		obj.SetActive (true);
    }

    private void SpawnEnemy()
    {
        int rnd = Random.Range(0, enemies.Length);
        float rndPos = Random.Range(-3.1f, 3.1f);

        GameObject obj = (GameObject) Instantiate(enemies[rnd]);
        obj.transform.position = SpawnerObj.transform.position + new Vector3(rndPos,0,0);
        obj.transform.rotation = Quaternion.identity;
    }
}
