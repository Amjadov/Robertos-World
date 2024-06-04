using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TouchObjectScript : MonoBehaviour
{
		private float pxX;
		private UnityEngine.UI.Image guiTex;
		public Color PressedColor;
		public Color UnpressedColor;
		public bool isPressed = false;
		private bool newTouch = false;
		public bool longPressDetected = false;
		public float SpaceFromRight = 0f;
		private float touchTime;
		private bool selected;
		public Transform HitObject;
		private Transform OT;

		void Awake ()
		{

		}
		// Use this for initialization
		void Start ()
		{
				// Move to right side of screen

				guiTex = GetComponent<UnityEngine.UI.Image> ();
				pxX = Screen.width - (SpaceFromRight + guiTex.pixelInset.width);		
				guiTex.pixelInset = new Rect (pxX, guiTex.pixelInset.y, guiTex.pixelInset.width, guiTex.pixelInset.height);
		//GUI.matrix = ReScaleGUI.GUIMatrix();
		}

		// Update is called once per frame
		void Update ()
		{

				foreach (Touch touch in Input.touches) {
						//User touched the button
						if (touch.phase == TouchPhase.Began && guiTex.HitTest (touch.position)) {
								isPressed = true;
								guiTex.color = PressedColor;
								newTouch = true;
								touchTime = Time.time;
								//User lefted his finger
						} else if (touch.phase == TouchPhase.Ended && guiTex.HitTest (touch.position)) {
								newTouch = false;
								longPressDetected = false;
								isPressed = false;
								guiTex.color = UnpressedColor;
								//User kept his finger for a longer timer
						} else if (touch.phase == TouchPhase.Stationary && guiTex.HitTest (touch.position)) {
								newTouch = false;
								longPressDetected = true;
								isPressed = true;
								guiTex.color = PressedColor;
								//User moved his finger while touching 
						} else if (touch.phase == TouchPhase.Moved && guiTex.HitTest (touch.position)) {
								newTouch = false;
								longPressDetected = true;
								isPressed = true;
								guiTex.color = PressedColor;
						} //else {
						//	newTouch = false;
						//	longPressDetected = false;
						//	isPressed = false;
						//	guiTex.color = UnpressedColor;
						//}




			
				}
		}

}
