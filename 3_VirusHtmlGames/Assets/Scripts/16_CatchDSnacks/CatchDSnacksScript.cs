using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CatchDSnacksScript : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] private TMP_Text remainingSnackText;
    [SerializeField] private RectTransform logoUI, mainMenuUI, gameUI, remainingTextUI, endCardUI;
    
    [Header("Tween Params")]
    [SerializeField] private LeanTweenType easeType;
    private int animId;

    [Header("Game Params")]
    [SerializeField] private int _numOfSnacks = 10;
    public int NumOfSnacks{
        get { return _numOfSnacks; } set { _numOfSnacks = value; remainingSnackText.text = _numOfSnacks.ToString(); }
    }
    [SerializeField] private GameObject boxObj, SpawnerObj;
    [SerializeField] private GameObject[] snacks;
    private float timer = 0f;
    [SerializeField] bool isPlaying = false;


    void Start()
    {
        animId = LeanTween.moveY(logoUI, -36, 0.25f)
            .setEase(LeanTweenType.easeOutSine)
            .setLoopPingPong().id;

    }

    void Update()
    {
        if(isPlaying)
        {
            timer += Time.deltaTime;
            Vector3 screenMouspos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if(Input.touchCount > 0)
            {
                screenMouspos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            }

            boxObj.transform.position = new Vector3(screenMouspos.x, boxObj.transform.position.y, boxObj.transform.position.z);

            if(timer >= 0.783666f)
            {
                SpawnSnacks();
                timer = 0f;
            }
        }

        if(NumOfSnacks <= 0 && isPlaying)
        {
            EndSequence();
        }
    }

    public void PlaySequence()
    {
        LeanTween.cancel(animId);
        LeanTween.moveY(mainMenuUI, -480f, 1f)
            .setEase(easeType);
        LeanTween.moveY(boxObj, -5.65f, 1f)
            .setEase(easeType);
        
        LeanTween.moveY(gameUI, 0f, 1f)
            .setEase(easeType)
            .setOnComplete(()=>isPlaying = true);
    }

    private void EndSequence()
    {
        isPlaying = false;
        LeanTween.moveY(gameUI, -480f, 1f)
            .setEase(easeType);
        LeanTween.moveY(boxObj, -10f, 1f)
            .setEase(easeType);
        LeanTween.moveY(endCardUI, 0f, 1f)
            .setEase(easeType);
    }

    private void SpawnSnacks()
    {
        int rnd = Random.Range(0, snacks.Length);
        float rndPos = Random.Range(-2.9f, 2.9f);

        GameObject obj = (GameObject) Instantiate(snacks[rnd]);
        obj.transform.position = SpawnerObj.transform.position + new Vector3(rndPos,0,0);
        obj.transform.rotation = Quaternion.identity;
    }

    public void OpenWebPage(string webPage)
    {
        Application.OpenURL(webPage);
    }
}
