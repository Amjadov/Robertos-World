using UnityEngine;
using System.Collections;

public class FPSDisplay : MonoBehaviour
{
	float deltaTime = 0.0f;
	private float fps = 60f;
	private int counter = 0;


//	void Start()
//		
//	{
//		
//		//StartCoroutine(CheckQuality());
//		
//	}
//
//	IEnumerator CheckQuality()
//		
//	{
//
//		while (true) {
//						yield return new WaitForSeconds (10);
//
//						if (fps < 50f && QualitySettings.GetQualityLevel () > 0) {
//			
//								QualitySettings.DecreaseLevel ();   
//			
//			
//						} 
//				}
//
//	}
//
//	void Update()
//	{
//		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
//	}
//	
////	void OnGUI()
////	{
////		GUI.contentColor = Color.black;
////		int w = Screen.width, h = Screen.height;
////		
////		GUIStyle style = new GUIStyle();
////		
////		Rect rect = new Rect(10, 90, w, h * 2 / 100);
////		style.alignment = TextAnchor.UpperLeft;
////		style.fontSize = h * 3 / 100;
////		style.normal.textColor = Color.yellow; //new Color (0.0f, 0.0f, 0.5f, 1.0f);
////		float msec = deltaTime * 1000.0f;
////		fps = 1.0f / deltaTime;
////		string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
////		GUI.Label(rect, text + ",Q: "+ QualitySettings.GetQualityLevel(), style);
////
////
////
////
////
////	}

}