using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ReScaleGUIText : MonoBehaviour
{

    private float pxX;
    private float pxY;
    private float pxWidth;
    private Text guiText;
    public Color PressedColor;
    public Color UnpressedColor;
    public bool isPressed = false;
    private bool newTouch = false;
    public bool longPressDetected = false;
    public float SpaceFromRight = 0f;
    public float SpaceFromTop = 0f;
    private float touchTime;
    private float sWidth;
    private float sHeight;
    private float guiWRatio;
    private float guiHRatio;
    private static float DesignedScaleWidth = 800;
    private static float DesignedScaleHeight = 480;
    private int finger = 0;

    void Start()
    {

        // Move to right side of screen
        guiText = GetComponent<Text>();
        getRatio();
        pxWidth = guiText.fontSize * guiWRatio;

        if (SpaceFromRight == 0)
            SpaceFromRight = guiText.rectTransform.anchoredPosition.x;

        if (SpaceFromTop == 0)
            SpaceFromTop = guiText.rectTransform.anchoredPosition.y;

        pxX = (SpaceFromRight * guiWRatio);
        pxY = (SpaceFromTop * guiHRatio);

        guiText.rectTransform.anchoredPosition = new Vector2(pxX, pxY);
        guiText.fontSize = (int)Mathf.FloorToInt(pxWidth);
    }

    void getRatio()
    {

        sWidth = Screen.width;

        //Get the screen's width

        sHeight = Screen.height;

        //Calculate the scale ratio. Divide the current screen resolution by the resolution you originally designed the UI on.

        guiWRatio = sWidth / DesignedScaleWidth;

        guiHRatio = sHeight / DesignedScaleHeight;
    }
}
