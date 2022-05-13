using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedBabyScript : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] private RectTransform logoUI;
    [SerializeField] private RectTransform endCardUI;
    [SerializeField] private CanvasGroup crackerObj;
    [SerializeField] private CanvasGroup[] babyStates;
    
    [Header("Tween Params")]
    [SerializeField] private LeanTweenType easeType = LeanTweenType.easeInOutSine;
    private int animId;

    [Header("Game Params")]
    public bool isPlaying = true;
    private bool tutorialDone = false;
    public int currentStage = 0;
    [SerializeField] private GameObject fingerObj;


    void Start()
    {
        LeanTween.moveY(logoUI, -42, 0.25f)
            .setEase(LeanTweenType.easeOutSine)
            .setLoopPingPong();       

        animId = LeanTween.move(fingerObj, Vector3.zero, 0.75f)
            .setLoopCount(-1)
            .setEase(LeanTweenType.easeOutSine).id;

        

    }

    void Update()
    {
        if(isPlaying)
        {
            if(currentStage == 1 && !tutorialDone)
            {
                tutorialDone = true;
                LeanTween.cancel(animId);
                fingerObj.SetActive(false);
            }

            if(currentStage == 2)
            {
                PlaySequence();
            }
        }

        
    }

    public void PlaySequence()
    {
        isPlaying = false;
        LeanTween.alphaCanvas(crackerObj, 1f, 0.5f)
            .setEase(easeType);
        LeanTween.alphaCanvas(babyStates[0], 0f, 0.5f)
            .setEase(easeType)
            .setDelay(1f);
        LeanTween.alphaCanvas(babyStates[1], 1f, 0.5f)
            .setEase(easeType)
            .setLoopPingPong(3)
            .setDelay(1f);
        LeanTween.alphaCanvas(babyStates[2], 1f, 0.5f)
            .setEase(easeType)
            .setLoopPingPong(3)
            .setDelay(1.5f);
        LeanTween.alphaCanvas(babyStates[0], 1f, 1f)
            .setEase(easeType)
            .setDelay(4f);
        LeanTween.alphaCanvas(crackerObj, 0f, 0.5f)
            .setEase(easeType)
            .setDelay(4f)
            .setOnComplete(EndSequence);
    }

    private void EndSequence()
    {
        LeanTween.moveY(endCardUI, 0, 1f)
            .setDelay(0.5f)
            .setEase(easeType);
    }

    public void OpenWebPage(string webPage)
    {
        Application.OpenURL(webPage);
    }
}
