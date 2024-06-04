using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Added this line to use UI.Image

public class InvincibleMode : MonoBehaviour
{
	private int i = 0;
	private Image guiTex; // Changed GUITexture to Image
	public Color PressedColor;
	public Color UnpressedColor;
	public bool isPressed = false;
	private bool newTouch = false;
	public bool longPressDetected = false;
	private float touchTime;
	private int finger = 0;
	// Use this for initialization
	void Start ()
	{

		// Move to right side of screen
		guiTex = GetComponent<Image>();
	}


	// Update is called once per frame
	void Update()
	{

		if (i > 10)
			GameManager.instance.invincibleMode = true;

		if (i > 30)
			GameManager.instance.OpenAllLevelsMode = true;


		if (Input.GetMouseButtonDown(0) && RectTransformUtility.RectangleContainsScreenPoint(guiTex.rectTransform, Input.mousePosition))
		{ 
			i += 1;
			return;
		}


		foreach (Touch touch in Input.touches)
		{



			//User touched the button
			if (touch.phase == TouchPhase.Began && RectTransformUtility.RectangleContainsScreenPoint(guiTex.rectTransform, touch.position))
			{ // Changed guiTex.HitTest to RectTransformUtility.RectangleContainsScreenPoint
				i += 1;
				//User lefted his finger
			}
			else if (touch.phase == TouchPhase.Ended && touch.fingerId == finger || touch.phase == TouchPhase.Canceled && touch.fingerId == finger)
			{
				//								newTouch = false;
				//								longPressDetected = false;
				//								isPressed = false;
				//								guiTex.color = UnpressedColor;
				//User kept his finger for a longer timer
			}
			else if (touch.phase == TouchPhase.Stationary && RectTransformUtility.RectangleContainsScreenPoint(guiTex.rectTransform, touch.position))
			{ // Changed guiTex.HitTest to RectTransformUtility.RectangleContainsScreenPoint
			  //								newTouch = false;
			  //								longPressDetected = true;
			  //								isPressed = true;
			  //								guiTex.color = PressedColor;
			  //User moved his finger while touching 
			}
			else if (touch.phase == TouchPhase.Moved && RectTransformUtility.RectangleContainsScreenPoint(guiTex.rectTransform, touch.position))
			{ // Changed guiTex.HitTest to RectTransformUtility.RectangleContainsScreenPoint
			  //								newTouch = false;
			  //								longPressDetected = true;
			  //								isPressed = true;
			  //								guiTex.color = PressedColor;

			}
		}
	}
}
