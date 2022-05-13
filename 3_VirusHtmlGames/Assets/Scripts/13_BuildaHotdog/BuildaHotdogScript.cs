using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildaHotdogScript : MonoSingleton<BuildaHotdogScript>
{
    [Header("UI Reference")]
    [SerializeField] private RectTransform[] endCardUI;
    [SerializeField] private RectTransform logoUI, mainMenuUI;
    
    [Header("Tween Params")]
    [SerializeField] private LeanTweenType easeType;
    private int animId;

    [Header("Game Params")]
    public bool isPlaying = false;
    [SerializeField] private GameObject playerObj;
    [SerializeField] private GameObject[] pieces;
    public int piecesStacked = 6;
    

    void Start()
    {
        animId = LeanTween.moveY(logoUI, -36, 0.25f)
            .setEase(LeanTweenType.easeOutSine)
            .setLoopPingPong().id;

        for(int i = 0; i < pieces.Length; i++)
        {
            float rndPos = Random.Range(-2.75f, 2.75f);
            Vector3 currentPos = pieces[i].transform.position;
            pieces[i].transform.position = new Vector3(rndPos, currentPos.y, 0);

        }
    }

    void Update()
    {
        if(isPlaying)
        {
            Vector3 screenMouspos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if(Input.touchCount > 0)
            {
                screenMouspos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            }

            playerObj.transform.position = new Vector3(screenMouspos.x, playerObj.transform.position.y, playerObj.transform.position.z);

            if(piecesStacked <= 0)
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
        LeanTween.moveY(mainMenuUI, -480f, 1f)
            .setEase(easeType);
        LeanTween.moveY(playerObj, -4.26f, 1f)
            .setEase(easeType)
            .setOnComplete(()=>isPlaying = true);
    }

    private void EndSequence()
    {
        isPlaying = false;
        LeanTween.move(playerObj, Vector3.zero, 1f)
            .setEase(easeType)
            .setDelay(0.5f);
        LeanTween.moveY(endCardUI[1], 0f, 1f)
            .setEase(easeType)
            .setDelay(1.5f);
        LeanTween.scale(endCardUI[1], new Vector3(1.25f,1.25f,1), 1f)
            .setEase(easeType)
            .setDelay(2.5f);
        LeanTween.moveY(endCardUI[1], 50f, 1f)
            .setEase(easeType)
            .setDelay(2.5f);
        LeanTween.moveY(endCardUI[0], -127.7f, 1f)
            .setEase(easeType)
            .setDelay(2.5f);
    }

    public void OpenWebPage(string webPage)
    {
        Application.OpenURL(webPage);
    }
}
