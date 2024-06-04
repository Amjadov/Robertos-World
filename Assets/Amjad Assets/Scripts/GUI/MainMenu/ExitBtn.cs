using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Import the UI namespace to use UI.Image

public class ExitBtn : MonoBehaviour
{
    private Image image; // Use UI.Image instead of GUITexture
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
        // Move to right side of screen
        image = GetComponent<Image>();
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && image.rectTransform.rect.Contains(Input.mousePosition))
        {
            DoAction();
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape)) { DoAction(); return; }


        foreach (Touch touch in Input.touches)
        {


            //User touched the button
            if (touch.phase == TouchPhase.Began && image.rectTransform.rect.Contains(touch.position))
            {
                DoAction();
                //User lefted his finger
            }
            else if (touch.phase == TouchPhase.Ended && touch.fingerId == finger || touch.phase == TouchPhase.Canceled && touch.fingerId == finger)
            {
                //newTouch = false;
                //longPressDetected = false;
                //isPressed = false;
                //image.color = UnpressedColor;
                //User kept his finger for a longer timer
            }
            else if (touch.phase == TouchPhase.Stationary && image.rectTransform.rect.Contains(touch.position))
            {
                //newTouch = false;
                //longPressDetected = true;
                //isPressed = true;
                //image.color = PressedColor;
                //User moved his finger while touching 
            }
            else if (touch.phase == TouchPhase.Moved && image.rectTransform.rect.Contains(touch.position))
            {
                //newTouch = false;
                //longPressDetected = true;
                //isPressed = true;
                //image.color = PressedColor;

            }
        }
    }

    void DoAction()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
    if (GameObject.Find("DontShowAds").GetComponent<DontShowAdverts>().DontShowAdds)
    {
    Application.Quit();
    return;
    }
    StartAppBackPlugin.current.showAdAndExit();
#else
        Application.Quit();
#endif
    }
}
