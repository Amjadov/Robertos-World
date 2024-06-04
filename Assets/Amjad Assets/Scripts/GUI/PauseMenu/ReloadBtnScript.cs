using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ReloadBtnScript : MonoBehaviour
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
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && RectTransformUtility.RectangleContainsScreenPoint(img.rectTransform, Input.mousePosition))
        {
            DoAction();
            return;
        }

        foreach (Touch touch in Input.touches)
        {
            //User touched the button
            if (touch.phase == TouchPhase.Began && RectTransformUtility.RectangleContainsScreenPoint(img.rectTransform, touch.position))
            {
                DoAction();
            }
            //User lefted his finger
            else if (touch.phase == TouchPhase.Ended && touch.fingerId == finger || touch.phase == TouchPhase.Canceled && touch.fingerId == finger)
            {
                newTouch = false;
                longPressDetected = false;
                isPressed = false;
                img.color = UnpressedColor;
            }
            //User kept his finger for a longer timer
            else if (touch.phase == TouchPhase.Stationary && RectTransformUtility.RectangleContainsScreenPoint(img.rectTransform, touch.position))
            {
                newTouch = false;
                longPressDetected = true;
                isPressed = true;
                img.color = PressedColor;
            }
            //User moved his finger while touching 
            else if (touch.phase == TouchPhase.Moved && RectTransformUtility.RectangleContainsScreenPoint(img.rectTransform, touch.position))
            {
                newTouch = false;
                longPressDetected = true;
                isPressed = true;
                img.color = PressedColor;
            }
        }
    }

    void DoAction()
    {
        AudioListener.pause = false;
        GameManager.instance.CurrentLevel = Application.loadedLevelName;
        Application.LoadLevel("Loading");
    }

}
