using UnityEngine;
using UnityEngine.UI;

public class BGScroll : MonoBehaviour
{
    [SerializeField] private RawImage bgLayer;
    [SerializeField] private float scrollSpeed;
    [SerializeField] private float scrollMultiplier = 1f;

    void Update()
    {
        bgLayer.uvRect = new Rect(bgLayer.uvRect.position + new Vector2(0, scrollSpeed)*Time.deltaTime*scrollMultiplier, bgLayer.uvRect.size);
    }
}
