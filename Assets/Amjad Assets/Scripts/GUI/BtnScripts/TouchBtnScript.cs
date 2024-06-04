using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TouchBtnScript : MonoBehaviour
{
    private Image image;
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

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began && RectTransformUtility.RectangleContainsScreenPoint(image.rectTransform, touch.position))
            {
                isPressed = true;
                image.color = PressedColor;
                newTouch = true;
                touchTime = Time.time;
                finger = touch.fingerId;
            }
            else if (touch.phase == TouchPhase.Ended && touch.fingerId == finger || touch.phase == TouchPhase.Canceled && touch.fingerId == finger)
            {
                newTouch = false;
                longPressDetected = false;
                isPressed = false;
                image.color = UnpressedColor;
            }
            else if (touch.phase == TouchPhase.Stationary && RectTransformUtility.RectangleContainsScreenPoint(image.rectTransform, touch.position))
            {
                newTouch = false;
                longPressDetected = true;
                isPressed = true;
                image.color = PressedColor;
            }
            else if (touch.phase == TouchPhase.Moved && RectTransformUtility.RectangleContainsScreenPoint(image.rectTransform, touch.position))
            {
                newTouch = false;
                longPressDetected = true;
                isPressed = true;
                image.color = PressedColor;
            }
        }
    }

}
