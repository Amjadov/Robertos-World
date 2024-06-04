using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelNoScript : MonoBehaviour
{
    private float pxX;
    private float pxY;
    private float pxWidth;
    private float pxHeight;
    private Image guiTex;
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

    // Move to right side of screen
    void Start()
    {
        guiTex = GetComponent<Image>();
    }

    void Update()
    {
        // your code
    }
}
