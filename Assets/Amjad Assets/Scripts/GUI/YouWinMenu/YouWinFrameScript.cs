using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class YouWinFrameScript : MonoBehaviour
{
		private UnityEngine.UI.Image guiTex;
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
				guiTex = GetComponent<UnityEngine.UI.Image> ();

		}
	


		
}
