using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkProductScript : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] private RectTransform logoUI;
    [SerializeField] private RectTransform mainMenuUI, endCardUI;
    [SerializeField] private GameObject gameUI, fingerObj;
    
    [Header("Tween Params")]
    [SerializeField] private LeanTweenType easeType = LeanTweenType.easeInOutSine;
    private int animId;

    [Header("Game Params")]
    public bool isPlaying = false;
    [SerializeField] private GameObject[] vendingMachine;
    [SerializeField] private GameObject cokeBottleObj;
    void Start()
    {
        animId = LeanTween.moveY(logoUI, 33f, 0.25f)
            .setEase(LeanTweenType.easeOutSine)
            .setLoopPingPong().id;        

    }

    void Update()
    {
        if(isPlaying)
        {

            
        }

        
    }

    public void PlaySequence()
    {
        LeanTween.cancel(animId);
        LeanTween.moveY(mainMenuUI, -480f, 1f)
            .setEase(easeType);
        LeanTween.moveY(logoUI, 480f, 1f)
            .setEase(easeType);

        LeanTween.moveY(gameUI, 0f, 1f)
            .setEase(easeType)
            .setOnComplete(()=>{
                LeanTween.alpha(fingerObj, 1f, 1f)
                    .setEase(easeType);
                animId = LeanTween.move(fingerObj, new Vector3(2.16f, -1.27f, 0f), 1f)
                            .setEaseOutSine()
                            .setLoopCount(-1)
                            .id;
                isPlaying = true;
            });
    }

    public void EndSequence()
    {
        //isPlaying = false;
        LeanTween.alpha(vendingMachine[0], 0f, 1f)
            .setEase(easeType);
        
        LeanTween.rotateZ(cokeBottleObj, 0f, 1f)
            .setEase(easeType)
            .setDelay(1.5f);
        LeanTween.move(cokeBottleObj, Vector3.zero, 1f)
            .setEase(easeType)
            .setDelay(1.5f);
        LeanTween.scale(cokeBottleObj, Vector3.one, 1f)
            .setEase(easeType)
            .setDelay(1.5f);
        LeanTween.alpha(vendingMachine[1], 0f, 1f)
            .setEase(easeType)
            .setDelay(1.5f);

        LeanTween.moveY(endCardUI, 0f, 1f)
            .setEase(easeType)
            .setDelay(2.5f);
        
    }

    public void StopTutorial()
    {
        LeanTween.cancel(animId);
        LeanTween.alpha(fingerObj, 0f, 1f)
            .setEase(easeType);
    }

    public void OpenWebPage(string webPage)
    {
        Application.OpenURL(webPage);
    }
}
