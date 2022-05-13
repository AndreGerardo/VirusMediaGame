using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatFoodPlatformerScript : MonoBehaviour
{
    [SerializeField] private RectTransform logoUI, mainMenuUI, endCardUI;
    [SerializeField] private GameObject instructionText, catFoodObj, fingerObj;
    [SerializeField] private string webPage;

    [Header("Tween Params")]
    [SerializeField] private LeanTweenType easeType;
    private int animId;

    [Header("Game Params")]
    public bool isPlaying = false;


    void Start()
    {
        animId = LeanTween.moveY(logoUI, -36, 0.25f)
            .setEase(LeanTweenType.easeOutSine)
            .setLoopPingPong().id;

        LeanTween.scale(catFoodObj, new Vector3(1.25f,1.25f,1.25f), 0.25f)
            .setEase(LeanTweenType.easeOutSine)
            .setLoopPingPong();

    }

    public void PlaySequence()
    {
        LeanTween.cancel(animId);
        LeanTween.moveY(mainMenuUI, -720, 1f)
            .setEase(easeType);
        LeanTween.alpha(fingerObj, 1f, 1f)
            .setEase(easeType);
        LeanTween.moveX(fingerObj, 2.5f, 0.5f)
            .setEase(easeType);
        LeanTween.moveX(fingerObj, -2.5f, 1f)
            .setEase(easeType)
            .setDelay(0.5f)
            .setLoopPingPong();
        LeanTween.alpha(instructionText, 1f, 1f)
            .setEase(easeType);
        LeanTween.moveY(instructionText, -4.56f, 1f)
            .setEase(easeType)
            .setDelay(1.5f)
            .setOnComplete(()=>isPlaying = true);
        
    }

    public void EndSequence()
    {
        isPlaying = false;
        LeanTween.moveY(endCardUI, 0f, 1f)
            .setEase(easeType)
            .setDelay(1f);
    }

    public void OpenWebPage()
    {
        Application.OpenURL(webPage);
    }
}
