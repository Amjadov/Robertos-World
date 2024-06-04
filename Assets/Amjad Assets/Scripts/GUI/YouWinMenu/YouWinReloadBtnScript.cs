using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class YouWinReloadBtnScript : MonoBehaviour
{
	private Image img;
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
		img = GetComponent<Image>();
	}


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && RectTransformUtility.RectangleContainsScreenPoint(img.rectTransform, Input.mousePosition))
        {
            GameManager.instance.CurrentLevel = Application.loadedLevelName;
            Application.LoadLevel("Loading");
            return;
        }

        foreach (Touch touch in Input.touches)
        {
            // User touched the button
            if (touch.phase == TouchPhase.Began && RectTransformUtility.RectangleContainsScreenPoint(img.rectTransform, touch.position))
            {
                GameManager.instance.CurrentLevel = Application.loadedLevelName;
                Application.LoadLevel("Loading");
            }
            // User lifted his finger
            else if ((touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) && touch.fingerId == finger)
            {
                newTouch = false;
                longPressDetected = false;
                isPressed = false;
                img.color = UnpressedColor;
            }
            // User kept his finger for a longer time
            else if (touch.phase == TouchPhase.Stationary && RectTransformUtility.RectangleContainsScreenPoint(img.rectTransform, touch.position))
            {
                newTouch = false;
                longPressDetected = true;
                isPressed = true;
                img.color = PressedColor;
            }
            // User moved his finger while touching 
            else if (touch.phase == TouchPhase.Moved && RectTransformUtility.RectangleContainsScreenPoint(img.rectTransform, touch.position))
            {
                newTouch = false;
                longPressDetected = true;
                isPressed = true;
                img.color = PressedColor;
            }
        }
    }
}
