using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDeliveryScript : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] private RectTransform logoUI;
    [SerializeField] private RectTransform mainMenuUI, endCardUI;
    [SerializeField] private GameObject[] gameUI;
    
    [Header("Tween Params")]
    [SerializeField] private LeanTweenType easeType;
    private int animId;

    [Header("Game Params")]
    public bool isPlaying = false;
    [SerializeField] private Transform[] deliveryHouses;
    [SerializeField] private Transform deliveryPoint;
    [SerializeField] private GameObject[] foodOrders;
    public int[] houseFoodCode;
    [SerializeField] private GameObject bikeObj, fingerObj, instructionObj;
    public bool bikeCanDeliver = true;
    public Queue<int> deliveryQueue = new Queue<int>();
    public int deliveryCount = 3;

    void Start()
    {
        animId = LeanTween.moveY(logoUI, -42, 0.25f)
            .setEase(LeanTweenType.easeOutSine)
            .setLoopPingPong().id;        

    }

    void Update()
    {
        if(isPlaying)
        {

            if(deliveryCount == 2 && fingerObj.activeInHierarchy)
            {
                LeanTween.cancel(animId);
                fingerObj.SetActive(false);
            }

            if(deliveryQueue.Count > 0 && bikeCanDeliver)
            {
                switch (deliveryQueue.Dequeue())
                {
                    case 0 : House1Waypoint(); break;
                    case 1 : House2Waypoint(); break;
                    case 2 : House3Waypoint(); break;
                }
            }

            if(deliveryCount <= 0)
            {
                EndSequence();
            }
        }

        
    }

    public void PlaySequence()
    {
        for(int i = 0; i < foodOrders.Length; i++)
        {
            LeanTween.alpha(foodOrders[i], 0.5f, 1f)
                .setEase(easeType)
                .setDelay(1f);
        }

        LeanTween.cancel(animId);
        LeanTween.moveY(mainMenuUI, -480f, 1f)
            .setEase(easeType);
        LeanTween.moveY(logoUI, -480f, 1f)
            .setEase(easeType);
        LeanTween.alpha(gameUI[1], 1f, 1f)
            .setEase(easeType)
            .setDelay(1f);
        LeanTween.alpha(bikeObj, 1f, 1f)
            .setEase(easeType)
            .setDelay(1f);
        LeanTween.alpha(instructionObj, 1f, 1f)
            .setEase(easeType)
            .setDelay(1f);
        LeanTween.moveY(instructionObj, 3.59f, 1f)
            .setEase(easeType)
            .setDelay(2f);
        LeanTween.moveY(gameUI[0], -4.32f, 1f)
            .setEase(easeType)
            .setDelay(2f)
            .setOnComplete(()=>{
                isPlaying = true;
                LeanTween.alpha(fingerObj, 1f, 0.5f);
                animId = LeanTween.move(fingerObj, new Vector3(0f,-1.24f,0f), 1f)
                                .setEase(LeanTweenType.easeOutSine)
                                .setDelay(0.5f)
                                .setLoopCount(-1)
                                .id;
            });
    }

    private void EndSequence()
    {
        isPlaying = false;
        LeanTween.moveY(gameUI[2], -16f, 1f)
            .setEase(easeType)
            .setDelay(1f);
        LeanTween.moveY(endCardUI, 0f, 1f)
            .setEase(easeType)
            .setDelay(1f);
    }

    public void House1Waypoint()
    {
        Debug.Log("Delivering");
        bikeCanDeliver = false;
        LeanTween.moveY(bikeObj, deliveryHouses[0].position.y, 1f)
            .setOnComplete(()=>foodOrders[1].SetActive(false));
        LeanTween.moveY(bikeObj, deliveryPoint.position.y, 1f)
            .setDelay(2f)
            .setOnComplete(()=>{
                bikeCanDeliver = true; 
                deliveryCount--;
            });
    }

    public void House2Waypoint()
    {
        Debug.Log("Delivering");
        bikeCanDeliver = false;
        LeanTween.moveY(bikeObj, deliveryHouses[1].position.y, 1f)
            .setOnComplete(()=>foodOrders[2].SetActive(false));
        LeanTween.moveY(bikeObj, deliveryPoint.position.y, 1f)
            .setDelay(2f)
            .setOnComplete(()=>{
                bikeCanDeliver = true; 
                deliveryCount--;
            });    
    }

    public void House3Waypoint()
    {
        Debug.Log("Delivering");
        bikeCanDeliver = false;
        LeanTween.moveY(bikeObj, deliveryHouses[3].position.y, 0.5f);
        LeanTween.moveX(bikeObj, deliveryHouses[4].position.x, 1f)
            .setDelay(0.5f);
        LeanTween.moveY(bikeObj, deliveryHouses[2].position.y, 1f)
            .setDelay(1.5f)
            .setOnComplete(()=>foodOrders[0].SetActive(false));

        LeanTween.moveY(bikeObj, deliveryHouses[4].position.y, 1f)
            .setDelay(3.5f);
        LeanTween.moveX(bikeObj, deliveryHouses[3].position.x, 1f)
            .setDelay(4.5f);
        LeanTween.moveY(bikeObj, deliveryPoint.position.y, 1f)
            .setDelay(5.5f)
            .setOnComplete(()=>{
                bikeCanDeliver = true;
                deliveryCount--;
            });    
    }

    public void OpenWebPage(string webPage)
    {
        Application.OpenURL(webPage);
    }
}
