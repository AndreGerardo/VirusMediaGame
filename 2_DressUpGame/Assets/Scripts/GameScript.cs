using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    [SerializeField] private RectTransform[] tops;
    [SerializeField] private RectTransform[] bottoms;

    [SerializeField] private int currentTop = 0;
    [SerializeField] private int currentBottom = 0;


    [Header("Tween Params")]
    public LeanTweenType tweenType;
    [SerializeField] private float moveLength = 200f;
    [SerializeField] private float animTime = 0.25f;
    [SerializeField] private RectTransform[] bubbleAnims;
    [SerializeField] private float s = 2f;
    [SerializeField] private float time = 1f;

    //End Card Params
    [SerializeField] private RectTransform[] deactivateObjects;
    [SerializeField] private RectTransform activateObject;
    [SerializeField] private float deactivatePos = -250f;
    [SerializeField] private float endCardTime = 0.5f;


    private void OnEnable()
    {
        for(int i = 0; i < bubbleAnims.Length; i++)
        {
            LeanTween.scale(bubbleAnims[i], new Vector3(s,s,s), time)
                .setLoopPingPong()
                .setEase(tweenType);
        }
    }

    public void ChangeTop(int dir) //dir -1 left, 1 right
    {
        LeanTween.moveX(tops[currentTop], moveLength * dir, animTime)
            .setEase(tweenType);
        LeanTween.alpha(tops[currentTop], 0f, animTime);

        currentTop += dir;
        if(currentTop == tops.Length) currentTop = 0;
        if(currentTop < 0) currentTop = tops.Length-1;

        LeanTween.moveX(tops[currentTop], moveLength * -dir, 0.1f);
        LeanTween.moveX(tops[currentTop], 0, animTime)
            .setEase(tweenType)
            .setDelay(0.1f);
        LeanTween.alpha(tops[currentTop], 1f, animTime)
            .setDelay(0.1f);;
    }

    public void ChangeBottom(int dir) //dir -1 left, 1 right
    {
        LeanTween.moveX(bottoms[currentBottom], moveLength * dir, animTime)
            .setEase(tweenType);
        LeanTween.alpha(bottoms[currentBottom], 0f, animTime);

        currentBottom += dir;
        if(currentBottom == bottoms.Length) currentBottom = 0;
        if(currentBottom < 0) currentBottom = bottoms.Length-1;

        LeanTween.moveX(bottoms[currentBottom], moveLength * -dir, 0.1f);
        LeanTween.moveX(bottoms[currentBottom], 0, animTime)
            .setEase(tweenType)
            .setDelay(0.1f);
        LeanTween.alpha(bottoms[currentBottom], 1f, animTime)
            .setDelay(0.1f);;
    }

    public void NextSequence()
    {
        for(int i = 0; i < deactivateObjects.Length; i++)
        {
            LeanTween.moveY(deactivateObjects[i], deactivatePos, endCardTime)
                .setEase(tweenType)
                .setDelay(endCardTime);
        }

        LeanTween.moveY(activateObject, 15f, endCardTime)
                .setEase(tweenType)
                .setDelay(endCardTime);
    }

}
