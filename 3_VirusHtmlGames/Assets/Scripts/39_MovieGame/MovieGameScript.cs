using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieGameScript : MonoSingleton<MovieGameScript>
{
    [Header("UI Reference")]
    [SerializeField] private RectTransform logoUI;
    [SerializeField] private RectTransform mainMenuUI, endCardUI;
    [SerializeField] private CanvasGroup background;
    [SerializeField] private GameObject gameUI;

    [Header("Tween Params")]
    [SerializeField] private LeanTweenType easeType = LeanTweenType.easeInOutSine;
    private int animId;

    [Header("Game Params")]
    public bool isPlaying = false;
    public bool enemyCanSpawn = false;
    private float timer = 0f;
    [SerializeField] private GameObject kryptoObj, tutorObj;
    [SerializeField] private int enemyCount = 10;
    [SerializeField] private Transform[] spawnpoints;
    [SerializeField] private GameObject[] spawnObjects;
    [SerializeField] private float spawnTime = 1.5f;


    void Start()
    {
        animId = LeanTween.scale(logoUI, Vector3.one * 1.1f, 0.5f)
            .setEase(LeanTweenType.easeInOutSine)
            .setLoopPingPong().id;

    }

    void Update()
    {
        if (isPlaying)
        {
            timer += Time.deltaTime;
            if (enemyCanSpawn && timer >= spawnTime)
            {
                if (enemyCount > 0)
                {
                    SpawnEnemy(0);
                }
                else if (enemyCount == 0)
                {
                    SpawnEnemy(1);
                }
                enemyCount--;
            }

        }

    }

    public void PlaySequence()
    {
        LeanTween.cancel(animId);
        LeanTween.moveY(mainMenuUI, -720f, 1f)
            .setEase(easeType);
        LeanTween.alphaCanvas(background, 1f, 1f)
            .setEase(easeType);
        LeanTween.moveX(kryptoObj, -2.33f, 1f)
            .setEase(easeType)
            .setOnComplete(() =>
            {
                isPlaying = true;
                TutorSequence();
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
        LeanTween.moveX(kryptoObj, 16f, 1f)
            .setEase(easeType);
        LeanTween.moveY(endCardUI, 0f, 1f)
            .setEase(easeType)
            .setDelay(1.5f);
    }

    private void TutorSequence()
    {
        LeanTween.moveY(tutorObj, 3.5f, 1f)
            .setEase(easeType);

        LeanTween.alpha(tutorObj, 0f, 1f)
            .setEase(easeType)
            .setDelay(2f)
            .setOnComplete(() => tutorObj.SetActive(false));
    }

    private void SpawnEnemy(int objIndex)
    {
        int rdm = Random.Range(0, spawnpoints.Length);
        GameObject obj = (GameObject)Instantiate(spawnObjects[objIndex]);
        obj.transform.position = spawnpoints[rdm].position;
        obj.transform.rotation = Quaternion.identity;
        timer = 0f;
    }

    public void OpenWebPage(string webPage)
    {
        Application.OpenURL(webPage);
    }
}
