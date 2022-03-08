using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ProductToBasketScript : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] private RectTransform logoUI;
    [SerializeField] private RectTransform mainMenuUI, endCardUI;
    [SerializeField] private GameObject gameUI;
    
    [Header("Tween Params")]
    [SerializeField] private LeanTweenType easeType;
    private int animId;

    [Header("Game Params")]
    [SerializeField] private bool isPlaying = false;
    [SerializeField] private GameObject[] productObjects;
    public int productCount = 3;

    void Start()
    {
        animId = LeanTween.moveY(logoUI, -42, 0.25f)
            .setEase(LeanTweenType.easeOutSine)
            .setLoopPingPong().id;

    }

    void Update()
    {
        if(productCount <= 0 && isPlaying)
        {
            EndSequence();
        }
    }

    public void PlaySequence()
    {
        LeanTween.cancel(animId);
        LeanTween.moveY(mainMenuUI, -480f, 1f)
            .setEase(easeType);
        LeanTween.moveY(logoUI, -480f, 1f)
            .setEase(easeType);
        LeanTween.moveY(gameUI, 0f, 1f)
            .setEase(easeType)
            .setOnComplete(()=>isPlaying = true);
        
        for(int i = 0; i < 3; i++)
        {
            LeanTween.scale(productObjects[i], new Vector3(1.2f,1.2f,1.2f), 0.5f)
                .setDelay(1f)
                .setEase(easeType);
        }
    }

    private void EndSequence()
    {
        isPlaying = false;
        LeanTween.moveY(gameUI, -8f, 1f)
            .setEase(easeType)
            .setDelay(1f);
        LeanTween.moveY(endCardUI, 0f, 1f)
            .setEase(easeType)
            .setDelay(1f);
        LeanTween.scale(endCardUI, new Vector3(1.25f,1.25f,1.25f), 1f)
            .setEase(LeanTweenType.easeOutSine)
            .setLoopPingPong()
            .setDelay(2f);
    }

}
