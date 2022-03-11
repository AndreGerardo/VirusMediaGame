using UnityEngine;
using TMPro;

public class CatchGameScript : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] private TMP_Text remainingSnackText;
    [SerializeField] private RectTransform logoUI, mainMenuUI, gameUI, remainingTextUI;
    [SerializeField] private RectTransform[] endCardUI;
    private Camera cam;
    
    [Header("Tween Params")]
    [SerializeField] private LeanTweenType easeType;
    private int animId;

    [Header("Game Params")]
    [SerializeField] private int _numOfSnacks = 10;
    public int NumOfSnacks{
        get { return _numOfSnacks; } set { _numOfSnacks = value; remainingSnackText.text = _numOfSnacks.ToString(); }
    }
    [SerializeField] private GameObject boxObj, SpawnerObj;
    [SerializeField] private GameObject[] snacks;
    private float timer = 0f;
    [SerializeField] bool isPlaying = false;


    void Start()
    {
        animId = LeanTween.moveY(logoUI, -36, 0.25f)
            .setEase(LeanTweenType.easeOutSine)
            .setLoopPingPong().id;

        cam = Camera.main;
    }

    void Update()
    {
        if(isPlaying)
        {
            timer += Time.deltaTime;
            Vector3 screenMouspos = cam.ScreenToWorldPoint(Input.mousePosition);

            if(Input.touchCount > 0)
            {
                screenMouspos = Input.GetTouch(0).position;
            }

            boxObj.transform.position = new Vector3(screenMouspos.x, boxObj.transform.position.y, boxObj.transform.position.z);

            if(timer >= 0.783666f)
            {
                SpawnSnacks();
                timer = 0f;
            }
        }

        if(NumOfSnacks <= 0 && isPlaying)
        {
            EndSequence();
        }
    }

    public void PlaySequence()
    {
        LeanTween.cancel(animId);
        LeanTween.moveY(mainMenuUI, -480f, 1f)
            .setEase(easeType);
        LeanTween.moveY(boxObj, -4.26f, 1f)
            .setEase(easeType);
        
        LeanTween.moveY(gameUI, 0f, 1f)
            .setEase(easeType)
            .setOnComplete(()=>isPlaying = true);
    }

    private void EndSequence()
    {
        isPlaying = false;
        LeanTween.moveY(gameUI, -480f, 1f)
            .setEase(easeType);
        LeanTween.moveY(boxObj, -10f, 1f)
            .setEase(easeType);
        LeanTween.moveY(endCardUI[0], 0f, 1f)
            .setEase(easeType);
        LeanTween.moveY(endCardUI[1], 123f, 1f)
            .setEase(easeType);
        LeanTween.moveY(endCardUI[2], 0f, 1f)
            .setEase(easeType);
        LeanTween.moveY(endCardUI[1], 143f, 0.25f)
            .setEase(LeanTweenType.easeOutSine)
            .setLoopPingPong()
            .setDelay(1f);
    }

    private void SpawnSnacks()
    {
        int rnd = Random.Range(0, snacks.Length);
        float rndPos = Random.Range(-3.1f, 3.1f);

        GameObject obj = (GameObject) Instantiate(snacks[rnd]);
        obj.transform.position = SpawnerObj.transform.position + new Vector3(rndPos,0,0);
        obj.transform.rotation = Quaternion.identity;
    }
}
