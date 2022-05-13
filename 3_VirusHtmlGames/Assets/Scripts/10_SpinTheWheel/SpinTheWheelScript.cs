using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpinTheWheelScript : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] private Image endCardImg;
    [SerializeField] private RectTransform logoUI, endCardUI, spinBtn;
    [SerializeField] private GameObject gameUI;

    [Header("Tween Params")]
    [SerializeField] private LeanTweenType easeType;
    private int animId;

    [Header("Game Params")]
    [SerializeField] private GameObject wheelObj;
    public int selectedItem;
    private bool isSpinning = false;
    private int randomSpinAngle;
    [SerializeField] private Sprite[] endcardImgs;
    
    void Start()
    {
        animId = LeanTween.moveY(logoUI, -36, 0.25f)
            .setEase(LeanTweenType.easeOutSine)
            .setLoopPingPong().id;
    }

    public void SpinTheWheel()
    {
        if(isSpinning == false)
        {
            isSpinning = true;

            randomSpinAngle = Random.Range (90, 360);
            float spinAngle = wheelObj.transform.eulerAngles.z - 5 * 360 + randomSpinAngle;

            LeanTween.rotateZ(wheelObj, spinAngle, 3f)
                .setEase(LeanTweenType.easeOutSine)
                .setOnComplete(()=>{
                    SelectWheelItem();
                    isSpinning = false;
                });
        }
    }

    private void SelectWheelItem()
    {
        switch(selectedItem)
        {
            case 0 : Debug.Log("Phone");       
            break;
            case 1 : Debug.Log("Sunglasses");   
            break;
            case 2 : Debug.Log("Helmet");       
            break;
            case 3 : Debug.Log("Watch");        
            break;
        }

        endCardImg.sprite = endcardImgs[selectedItem];

        LeanTween.cancel(animId);
        LeanTween.moveY(gameUI, -16f, 1f)
            .setEase(easeType)
            .setDelay(1f);
        LeanTween.moveY(logoUI, 480f, 1f)
            .setEase(easeType)
            .setDelay(1f);
        LeanTween.moveY(spinBtn, -480f, 1f)
            .setEase(easeType)
            .setDelay(1f);
        LeanTween.moveY(endCardUI, 0f, 1f)
            .setEase(easeType)
            .setDelay(1f);
    }

    public void OpenWebPage(string webPage)
    {
        Application.OpenURL(webPage);
    }
}
