using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackDIceScript : MonoBehaviour
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
    [SerializeField] private GameObject fruitteaObj, shatterObj, tutorialObj;
    private int crackNums = 5;
    [SerializeField] private GameObject[] cracksObj;
    private List<GameObject> crackList = new List<GameObject>();


    void Start()
    {
        animId = LeanTween.moveY(logoUI, 40f, 0.25f)
            .setEase(LeanTweenType.easeOutSine)
            .setLoopPingPong().id;

    }

    void Update()
    {
        if(isPlaying)
        {

            if(Input.GetMouseButtonDown(0))
            {
                CrackScreen(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }

            if(Input.touchCount > 0)
            {
                if(Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    CrackScreen(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position));
                }
            }

            if(crackNums <= 0)
            {
                ShatterScreen();
                EndSequence();
            }
        }

    }

    public void PlaySequence()
    {
        LeanTween.cancel(animId);
        LeanTween.moveY(mainMenuUI, -720f, 1f)
            .setEase(easeType);
        LeanTween.moveY(gameUI, 0f, 1f)
            .setEase(easeType)
            .setOnComplete(()=>isPlaying = true);
    }

    public void EndSequence()
    {
        isPlaying = false;
        foreach (var crack in crackList)
        {
            Destroy(crack);
        }
        
        LeanTween.alpha(tutorialObj, 0f, 1f)
            .setEase(easeType);

        LeanTween.scale(fruitteaObj, Vector3.one * 1.5f, 1f)
            .setEase(easeType)
            .setDelay(1f);

        LeanTween.moveY(endCardUI, 0f, 1f)
            .setEase(easeType)
            .setDelay(3f);
        
    }

    private void CrackScreen(Vector3 pos)
    {
        int rdm = Random.Range(0, cracksObj.Length);

        GameObject obj = (GameObject)Instantiate(cracksObj[rdm], new Vector3(pos.x, pos.y, 0f), Quaternion.identity);
        crackList.Add(obj);
        crackNums--;
    }

    private void ShatterScreen()
    {
        shatterObj.GetComponent<Explodable>().explode();
    }

    public void OpenWebPage(string webPage)
    {
        Application.OpenURL(webPage);
    }

}
