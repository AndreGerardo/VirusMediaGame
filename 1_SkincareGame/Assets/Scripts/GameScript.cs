using UnityEngine;

public class GameScript : MonoBehaviour
{
    [Header("Game Params")]
    [SerializeField] private int faceStage = 0;
    public int currentTool = 0;
    [SerializeField] private RectTransform[] faces;

    [Header("End Card Params")]
    [SerializeField] private RectTransform[] endCardDeactivateObjects;
    [SerializeField] private RectTransform[] endCardActivateObjects;
    /*
    0 = face
    1 = hand
    2 = button
    3 = Sale
    */

    public void OnNextStage()
    {
        if(currentTool == faceStage)
        {
            //Change Face
            LeanTween.alpha(faces[faceStage+1], 1f, 0.5f)
                .setEase(LeanTweenType.easeInOutSine);
            LeanTween.alpha(faces[faceStage], 0f, 0.5f)
                .setEase(LeanTweenType.easeInOutSine);

            faceStage++;
        }else
        {
            Debug.Log("Incorrect Tool");
        }

        if(faceStage == 3)
        {
            EndCardTrigger();
            faceStage++;
        }
    }

    private void EndCardTrigger()
    {
        for(int i = 0; i < endCardDeactivateObjects.Length; i++)
        {
            LeanTween.alpha(endCardDeactivateObjects[i], 0f, 1f)
                .setEase(LeanTweenType.easeInOutSine);
        }

        LeanTween.moveX(endCardActivateObjects[0], 75f, 1f)
            .setEase(LeanTweenType.easeInOutSine)
            .setDelay(1f);
        LeanTween.moveY(endCardActivateObjects[1], -55f, 1f)
            .setEase(LeanTweenType.easeInOutSine)
            .setDelay(1f);
        LeanTween.moveY(endCardActivateObjects[2], 25f, 1f)
            .setEase(LeanTweenType.easeInOutSine)
            .setDelay(1f);
        LeanTween.moveY(endCardActivateObjects[3], 55, 1f)
            .setEase(LeanTweenType.easeInOutSine)
            .setDelay(1f);
    }

    public void ChangeTool(int toolID)
    {
        currentTool = toolID;
    }

    public void OpenWebPage(string webPage)
    {
        Application.OpenURL(webPage);
    }

}
