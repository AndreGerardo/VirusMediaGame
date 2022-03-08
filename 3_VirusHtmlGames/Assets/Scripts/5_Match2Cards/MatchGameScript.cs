using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MatchGameScript : MonoBehaviour
{
    
    [Header("References")]
    [SerializeField] private RectTransform logo;
    [SerializeField] private RectTransform[] mainMenuItems;
    [SerializeField] private RectTransform gameItem;
    [SerializeField] private RectTransform[] endCardItems;

    [Header("Game Params")]
    [SerializeField] private bool isPlaying = false;
    [SerializeField] private TMP_Text timerText;
    private float _timer = 60f;
    private float Timer{
        get { return _timer; }
        set { _timer = value; timerText.text = "Timer\n" + Mathf.Floor(value).ToString(); }
    }
    private int animID;
    [SerializeField] private LeanTweenType easeType;
    private float s = 1.15f;

    [Header("Game Logic")]
    [SerializeField] private int solvedCount = 0;
    public List<GameObject> cardButtons;
    [SerializeField] private int[] cardIDs;
    [SerializeField] public Sprite[] cardImages;
    [SerializeField] private List<int> faceIndexes = new List<int>{ 0,0,1,1,2,2,3,3,4,4,5,5,6,6,7,7};
    [SerializeField] private List<int> chosenCardIndex = new List<int>();
    [SerializeField] private List<int> chosenCard = new List<int>();
    [SerializeField] GameObject BLOCK;

    private void Start()
    {
        animID = LeanTween.scale(logo, new Vector3(s,s,s), 1f)
            .setEase(easeType)
            .setLoopPingPong().id;

        faceIndexes.Shuffle();

        for(int i = 0; i < 16; i++)
        {
            cardIDs[i] = faceIndexes[i];
            cardButtons[i].transform.GetChild(0).GetComponent<Image>().sprite = cardImages[faceIndexes[i]];
        }
    }

    private void Update()
    {
        if(isPlaying && _timer > 0)
        {
            Timer -= Time.deltaTime;
        }

        if(Timer <= 0f && isPlaying)
        {
            isPlaying = false;
            EndSequence();
        }

        if(solvedCount == 8 && isPlaying)
        {
            isPlaying = false;
            EndSequence();
        }
    }

    public void PlaySequence()
    {
        LeanTween.pause(animID);
        LeanTween.moveY(logo, 480f, 1f)
            .setEase(easeType);
        LeanTween.moveY(mainMenuItems[0], 480f, 1f)
            .setEase(easeType);
        LeanTween.moveY(mainMenuItems[1], -480f, 1f)
            .setEase(easeType);

        LeanTween.moveY(gameItem, 0, 1f)
            .setEase(easeType)
            .setOnComplete(() => isPlaying = true);
    }

    public void EndSequence()
    {
        LeanTween.moveY(logo, -24f, 1f)
            .setEase(easeType)
            .setOnComplete(()=>LeanTween.resume(animID));
        LeanTween.moveY(gameItem, 480f, 1f)
            .setEase(easeType);
        LeanTween.moveY(endCardItems[0], -15f, 1f)
            .setEase(easeType);
        LeanTween.moveY(endCardItems[1], 0f, 1f)
            .setEase(easeType);
        LeanTween.scale(endCardItems[1], new Vector3(s,s,s), 1f)
            .setDelay(1f)
            .setEase(easeType)
            .setLoopPingPong();
    }

    //Game Logics
    public void ChooseCard(int idx)
    {
        chosenCardIndex.Add(faceIndexes[idx]);
        chosenCard.Add(idx);
        LeanTween.rotateY(cardButtons[idx], 90, 0.25f)
            .setEase(easeType)
            .setOnComplete(CheckCards);
    }

    private void CheckCards()
    {
        if(chosenCardIndex.Count == 2)
        {
            if(chosenCardIndex[0] == chosenCardIndex[1])
            {
                chosenCardIndex.Clear();
                chosenCard.Clear();
                solvedCount++;
                Debug.Log("MATCH");
            }else
            {
                Debug.Log("NO MATCH");
                BLOCK.SetActive(true);
                for(int i = 0; i < 2; i++)
                {
                    LeanTween.rotateY(cardButtons[chosenCard[i]].gameObject, 0, 0.25f)
                        .setEase(easeType)
                        .setOnComplete(()=>{
                            BLOCK.SetActive(false);
                            chosenCard.Clear();
                            chosenCardIndex.Clear();
                        });
                }
            }
        }
    }

}
