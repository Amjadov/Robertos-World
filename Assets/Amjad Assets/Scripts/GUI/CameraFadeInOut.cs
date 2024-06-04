using UnityEngine;
using System.Collections;

public class CameraFadeInOut : MonoBehaviour
{
		// FadeInOut
	
		public Texture2D fadeTexture;
		public float  fadeSpeed = 0.2f;
		public int drawDepth = -1000;
		private float fadeDir = -1;
		public bool FadeIN = false;
		public bool FadeOUT = false;
		private float FadeOutalpha = 0.0f;
		private float FadeInalpha = 1.0f;
		// Use this for initialization

		void  OnGUI ()
		{

				if (FadeIN || FadeOUT) {
						if (FadeIN) {
								FadeInalpha += fadeDir * fadeSpeed * Time.deltaTime;
								FadeInalpha = Mathf.Clamp01 (FadeInalpha);
								Color c = new Color (GUI.color.r, GUI.color.g, GUI.color.b, FadeInalpha);
								GUI.color = c;
								if (FadeInalpha == 0f){
										FadeIN = false;
										FadeInalpha = 1.0f;
				}
				} else if (FadeOUT) { 
								FadeOutalpha -= fadeDir * fadeSpeed * Time.deltaTime;
								FadeOutalpha = Mathf.Clamp01 (FadeOutalpha);
								Color c = new Color (GUI.color.r, GUI.color.g, GUI.color.b, FadeOutalpha);
								GUI.color = c;
//								if (FadeOutalpha == 1f){
//										FadeOUT = false;
//										FadeOutalpha = 0.0f;
//				}
						}
						
					

		
						GUI.depth = drawDepth;
		
						GUI.DrawTexture (new Rect (0, 0, Screen.width + 1f, Screen.height + 1f), fadeTexture);
				}
		}
}




	
	