using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NavBtn : MonoBehaviour
{
	private Image image;
	public Color PressedColor;
	public Color UnpressedColor;
	public bool isPressed = false;
	private bool newTouch = false;
	public bool longPressDetected = false;
	private float touchTime;
	private int finger = 0;
	public Transform WindowToShow;
	public Transform WindowToHide;
	// Use this for initialization
	void Start()
	{
		// Move to right side of screen
		image = GetComponent<Image>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0) && RectTransformUtility.RectangleContainsScreenPoint((RectTransform)transform, Input.mousePosition))
		{
			DoAction();
			return;
		}

		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began && RectTransformUtility.RectangleContainsScreenPoint((RectTransform)transform, touch.position))
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
			else if (touch.phase == TouchPhase.Stationary && RectTransformUtility.RectangleContainsScreenPoint((RectTransform)transform, touch.position))
			{
				//newTouch = false;
				//longPressDetected = true;
				//isPressed = true;
				//image.color = PressedColor;
				//User moved his finger while touching 
			}
			else if (touch.phase == TouchPhase.Moved && RectTransformUtility.RectangleContainsScreenPoint((RectTransform)transform, touch.position))
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
		WindowToShow.gameObject.SetActive(true);
		WindowToHide.gameObject.SetActive(false);
	}
}
