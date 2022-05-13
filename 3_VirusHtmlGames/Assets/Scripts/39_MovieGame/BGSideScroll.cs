using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGSideScroll : MonoBehaviour
{
    [SerializeField] private RawImage bgLayer;
    [SerializeField] private float scrollSpeed;
    [SerializeField] private float scrollMultiplier = 1f;

    void Update()
    {
        bgLayer.uvRect = new Rect(bgLayer.uvRect.position + new Vector2(scrollSpeed, 0) * Time.deltaTime * scrollMultiplier, bgLayer.uvRect.size);
    }
}
