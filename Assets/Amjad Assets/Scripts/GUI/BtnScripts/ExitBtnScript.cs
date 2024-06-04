using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ExitBtnScript : MonoBehaviour
{
	private float pxX;
	private Image guiImage;
	public Color PressedColor;
	public Color UnpressedColor;
	public bool isPressed = false;
	// Use this for initialization
	void Start()
	{
		// Move to right side of screen
		guiImage = GetComponent<Image>();
		pxX = Screen.width / 2f - (guiImage.rectTransform.rect.width / 2f);
		guiImage.rectTransform.anchoredPosition = new Vector2(pxX, guiImage.rectTransform.anchoredPosition.y);
	}
	// Update is called once per frame
	void Update()
	{

		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Stationary && RectTransformUtility.RectangleContainsScreenPoint(guiImage.rectTransform, touch.position))
			{
				isPressed = true;
				audio.Play();
				Application.Quit();
			}
		}
	}
}
