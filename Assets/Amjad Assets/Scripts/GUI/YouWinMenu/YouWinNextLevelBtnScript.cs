using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class YouWinNextLevelBtnScript : MonoBehaviour
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
		void Start ()
		{

				// Move to right side of screen
				guiTex = GetComponent<Image> ();

		}



		// Update is called once per frame
		void Update ()
		{

		if (Input.GetMouseButtonDown(0) && guiTex.HitTest (Input.mousePosition ) ){
			DoAction ();
			return;
		}

				foreach (Touch touch in Input.touches) {
		
						

						//User touched the button
						if (touch.phase == TouchPhase.Began && guiTex.HitTest (touch.position)) {
				DoAction ();
								//User lefted his finger
						} else if (touch.phase == TouchPhase.Ended && touch.fingerId == finger || touch.phase == TouchPhase.Canceled && touch.fingerId == finger) {
//								newTouch = false;
//								longPressDetected = false;
//								isPressed = false;
//								guiTex.color = UnpressedColor;
								//User kept his finger for a longer timer
						} else if (touch.phase == TouchPhase.Stationary && guiTex.HitTest (touch.position)) {
//								newTouch = false;
//								longPressDetected = true;
//								isPressed = true;
//								guiTex.color = PressedColor;
								//User moved his finger while touching 
						} else if (touch.phase == TouchPhase.Moved && guiTex.HitTest (touch.position)) {
//								newTouch = false;
//								longPressDetected = true;
//								isPressed = true;
//								guiTex.color = PressedColor;

						} 
				}
			
				


		}
	void DoAction()
	{

		GameManager.instance.CurrentLevel = GameManager.instance.NextLevel;
		Debug.Log (GameManager.instance.CurrentLevel);
		Application.LoadLevel ("Loading");
	}
}
