using UnityEngine;
using System.Collections;
using UnityEngine.UI; // add this line to use Image component

public class MainMenuBtnScript : MonoBehaviour
{
    private Image img; // replace GUITexture with Image
    public Color PressedColor;
    public Color UnpressedColor;
    public bool isPressed = false;
    private bool newTouch = false;
    public bool longPressDetected = false;
    private float touchTime;
    private int finger = 0;
    // Use this for initialization
    void Start()
    {
        img = GetComponent<Image>(); // replace GUITexture with Image
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && img.rectTransform.rect.Contains(Input.mousePosition))
        {
            DoAction();
            return;
        }

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began && img.rectTransform.rect.Contains(touch.position))
            {
                DoAction();
            }
        }
    }

    void DoAction()
    {
        AudioListener.pause = false;
        GameManager.instance.CurrentLevel = "Main";
        Application.LoadLevel("Loading");
    }

}
