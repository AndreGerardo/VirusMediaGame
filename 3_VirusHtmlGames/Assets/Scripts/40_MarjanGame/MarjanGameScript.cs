using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarjanGameScript : MonoSingleton<MarjanGameScript>
{
    [Header("UI Reference")]
    [SerializeField] private RectTransform logoUI;
    [SerializeField] private RectTransform mainMenuUI, endCardUI;
    [SerializeField] private GameObject gameUI;

    [Header("Tween Params")]
    [SerializeField] private LeanTweenType easeType = LeanTweenType.easeInOutSine;
    private int animId;

    [Header("Game Params")]
    public bool isPlaying = false;
    private bool timerStart = false;
    private float timer = 0f;
    public float timeRemaining = 15f, maxTime;
    public Transform enemyTarget;
    [SerializeField] private GameObject tutorObj, slapObj, soundObj;
    [SerializeField] private Vector3 slapObjectOffset;
    [SerializeField] private GameObject[] handObjects;
    private List<GameObject> handList = new List<GameObject>();
    [SerializeField] private Transform[] spawnpoints;
    [SerializeField] private SpriteRenderer clockObj;
    [SerializeField] private Sprite[] clocks;
    [SerializeField] private float spawnTime = 1.5f;

    void Start()
    {
        animId = LeanTween.scale(logoUI, Vector3.one * 1.1f, 0.5f)
            .setEase(LeanTweenType.easeInOutSine)
            .setLoopPingPong().id;

        maxTime = timeRemaining;

    }

    void Update()
    {
        if (isPlaying)
        {
            Vector3 screenMouspos;
            if (Input.GetMouseButtonDown(0))
            {
                screenMouspos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                SpawnSlapper(screenMouspos);
            }

            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    screenMouspos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                    SpawnSlapper(screenMouspos);
                }
            }



            if (timerStart) timeRemaining -= Time.deltaTime;
            ClockConfiguration();
            timer += Time.deltaTime;
            if (timer >= spawnTime)
            {
                SpawnEnemy();
                timer = 0f;
            }

            if (timeRemaining <= 0f) FinishSequence();

        }

    }

    public void PlaySequence()
    {
        LeanTween.cancel(animId);
        LeanTween.moveY(mainMenuUI, -720f, 1f)
            .setEase(easeType);
        LeanTween.moveY(gameUI, 0f, 1f)
            .setEase(easeType)
            .setOnComplete(() =>
            {
                TutorSequence();
                isPlaying = true;
            });

    }

    public void EndSequence()
    {
        isPlaying = false;
        LeanTween.moveY(endCardUI, 0f, 1f)
            .setEase(easeType);
    }

    public void FinishSequence()
    {
        isPlaying = false;
        foreach (var obj in handList) Destroy(obj);

        LeanTween.alpha(soundObj, 1f, 0.5f)
            .setDelay(0.5f)
            .setLoopPingPong()
            .setEase(easeType);

        LeanTween.moveY(endCardUI, 0f, 1f)
            .setEase(easeType)
            .setDelay(3f);
    }

    private void TutorSequence()
    {
        LeanTween.moveY(tutorObj, 3.5f, 1f)
            .setEase(easeType);

        LeanTween.alpha(tutorObj, 0f, 1f)
            .setEase(easeType)
            .setDelay(2f)
            .setOnComplete(() =>
            {
                tutorObj.SetActive(false);
                timerStart = true;
            });
    }

    private void SpawnEnemy()
    {
        int rdm = Random.Range(1, 3);

        for (int i = 0; i < rdm; i++)
        {
            float leftOrRight = Random.Range(0f, 1f);
            if (leftOrRight >= 0.5f)
            {
                GameObject obj = (GameObject)Instantiate(handObjects[1], spawnpoints[1].position + (Vector3.up * Random.Range(-2.5f, 2.5f)), Quaternion.identity);
                handList.Add(obj);
            }
            else
            {
                GameObject obj = (GameObject)Instantiate(handObjects[0], spawnpoints[0].position + (Vector3.up * Random.Range(-2.5f, 2.5f)), Quaternion.identity);
                handList.Add(obj);
            }
        }
    }

    private void ClockConfiguration()
    {
        if (timeRemaining <= maxTime / 1.5f && timeRemaining > maxTime / 2f) { clockObj.sprite = clocks[1]; }
        else if (timeRemaining <= maxTime / 2f && timeRemaining > maxTime / 3f) { clockObj.sprite = clocks[2]; }
        else if (timeRemaining <= maxTime / 3f && timeRemaining > 0f) { clockObj.sprite = clocks[3]; }
        else if (timeRemaining <= 0f) { clockObj.sprite = clocks[4]; }
    }

    private void SpawnSlapper(Vector3 pos)
    {
        GameObject obj = (GameObject)Instantiate(slapObj);
        obj.transform.position = pos + slapObjectOffset;
        obj.transform.rotation = Quaternion.identity;
    }

    public void OpenWebPage(string webPage)
    {
        Application.OpenURL(webPage);
    }
}
