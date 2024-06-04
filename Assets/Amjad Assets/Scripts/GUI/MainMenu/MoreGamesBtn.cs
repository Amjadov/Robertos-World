using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoreGamesBtn : MonoBehaviour {
	private Image image;
	public Color PressedColor;
	public Color UnpressedColor;
	public bool isPressed = false;
	private bool newTouch = false;
	public bool longPressDetected = false;
	private float touchTime;
	private int finger = 0;
	// Use this for initialization
	void Awake()
	{


		}
	void Start()
	{
		if (GameObject.Find("DontShowAds").GetComponent<DontShowAdverts>().DontShowAdds)
			transform.gameObject.SetActive(false);

		// Move to right side of screen
		image = GetComponent<Image>();
	}


	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0) && RectTransformUtility.RectangleContainsScreenPoint(image.rectTransform, Input.mousePosition))
		{
			DoAction();
			return;
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			DoAction();
			return;
		}

		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began && RectTransformUtility.RectangleContainsScreenPoint(image.rectTransform, touch.position))
			{
				DoAction();
			}
		}
	}
	void DoAction(){
		#if UNITY_ANDROID && !UNITY_EDITOR
		if (GameObject.Find ("DontShowAds").GetComponent<DontShowAdverts> ().DontShowAdds)
			return;

		GameManager.instance.ShowInterstitialAds ();  
		#endif
		 

	}
}
