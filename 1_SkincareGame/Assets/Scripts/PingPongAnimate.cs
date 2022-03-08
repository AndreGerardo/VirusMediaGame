using UnityEngine;

public class PingPongAnimate : MonoBehaviour
{
    //Animation Parameter
    [SerializeField] private LeanTweenType easeType;
    [SerializeField] private float s = 2f;
    [SerializeField] private float time = 1f;
    private int animId;

    void OnEnable()
    {
        //Handle Animation
        animId = LeanTween.scale(gameObject, new Vector3(s,s,s), time)
            .setLoopPingPong()
            .setEase(easeType).id;
    }

    void OnDisable()
    {
        LeanTween.scale(gameObject, Vector3.one, 0.1f);
        
        LeanTween.cancel(animId);
    }

    public void FadeInAnim()
    {
        LeanTween.alpha(GetComponent<RectTransform>(), 1f, 1f);
    }
    public void FadeOutAnim()
    {
        LeanTween.alpha(GetComponent<RectTransform>(), 0f, 1f);
        gameObject.SetActive(false);
    }

}
