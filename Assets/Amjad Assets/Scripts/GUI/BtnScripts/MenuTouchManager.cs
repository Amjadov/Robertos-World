using UnityEngine;
using System.Collections;


public class MenuTouchManager : MonoBehaviour
{
		private GameManager GameManager = new GameManager ();
		private Vector3 ScreenPoint;
		private string TouchObjName;
		private bool paused = false;

		void Awake ()
		{
				Camera.main.transform.FindChild ("SelectLevel").gameObject.SetActive (false);
				Camera.main.transform.FindChild ("MainMenu").gameObject.SetActive (true);
				//GameManager =  GameManager.GetComponent<GameManager> ();

		}

		void Update ()
		{
				if (Input.GetMouseButtonDown (0)) {
						Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
						RaycastHit hit;
						if (Physics.Raycast (ray, out hit)) {
//								Debug.Log ("Name = " + hit.collider.name);
//								Debug.Log ("Tag = " + hit.collider.tag);
//								Debug.Log ("Hit Point = " + hit.point);
//								Debug.Log ("Object position = " + hit.collider.gameObject.transform.position);
//								Debug.Log ("--------------");
								TouchObjName = hit.collider.name;
								ManageTouch (hit);
						}
				}

				foreach (Touch touch in Input.touches) {
						if (Input.GetMouseButtonDown (0)) {
								if (touch.phase == TouchPhase.Began) {
										Ray ray = Camera.main.ScreenPointToRay (touch.position);
										RaycastHit hit;
										if (Physics.Raycast (ray, out hit)) {
//												Debug.Log ("Name = " + hit.collider.name);
//												Debug.Log ("Tag = " + hit.collider.tag);
//												Debug.Log ("Hit Point = " + hit.point);
//												Debug.Log ("Object position = " + hit.collider.gameObject.transform.position);
//												Debug.Log ("--------------");
												TouchObjName = hit.collider.name;
												ManageTouch (hit);
										}
								}
						}
				}
		}

		void OnMouseDown ()
		{
				ScreenPoint = Camera.main.WorldToScreenPoint (gameObject.transform.position);
				Debug.Log ("Mouse Down");


		}

//		void OnGUI ()
//		{
//				GUI.Label (new Rect (10, 100, 500, 50), "Obj: " + TouchObjName);
//				//GUI.Label(new Rect(10,40,500,50),"9 ButtonYAxis: "+ Input.mousePosition.y);
//				//GUI.Label(new Rect(10,70,540,50),"JS Connected: "+GLOBAL.isJSConnected);
//		}

		void ManageTouch (RaycastHit hit)
		{
				switch (hit.collider.gameObject.transform.parent.name) {
				case "StageBase1":
						audio.Play ();
						GameManager.CurrentLevel = "S01_E01";
						GameManager.NextLevel = "S01_E02";
						Application.LoadLevel (GameManager.CurrentLevel);
						break;
				case "StageBase2":
						audio.Play ();
						GameManager.CurrentLevel = "S01_E02";
						GameManager.NextLevel = "S01_E03";
						Application.LoadLevel (GameManager.CurrentLevel);
						break;
				case "StageBase3":
						audio.Play ();
						GameManager.CurrentLevel = "S01_E03";
						GameManager.NextLevel = "S01_E04";
						Application.LoadLevel (GameManager.CurrentLevel);
						break;
		case "StageBase4":
			audio.Play ();
			GameManager.CurrentLevel = "S01_E04";
			GameManager.NextLevel = "S01_E05";
			Application.LoadLevel (GameManager.CurrentLevel);
			break;
				case "Backbtn":
						audio.Play ();
						Camera.main.transform.FindChild ("SelectLevel").gameObject.SetActive (false);
						Camera.main.transform.FindChild ("MainMenu").gameObject.SetActive (true);
						break;
				}
				if (hit.collider.name == "Play") {
						audio.Play ();
						Camera.main.transform.FindChild ("SelectLevelWindow").gameObject.SetActive (true);
						Camera.main.transform.FindChild ("MainMenu").gameObject.SetActive (false);
						
				} 
				if (hit.collider.name == "Exit") {
						audio.Play ();
						Application.Quit (); 
				} 
				if (hit.collider.name == "RetryLevel") {
						audio.Play ();
						Application.LoadLevel (Application.loadedLevelName);
				}
				if (hit.collider.name == "ResumeLevel") {
						audio.Play ();
						Camera.main.transform.Find ("Paused").gameObject.SetActive (false);  
						GameObject.Find ("Pauser").GetComponent<Pauser> ().paused = false;   
				}
				if (hit.collider.name == "MainMenu") {
						audio.Play ();
						Application.LoadLevel ("Main");
				}
				if (hit.collider.name == "NextLevel") {
						audio.Play ();
						Application.LoadLevel (GameManager.NextLevel);
				}
				if (hit.collider.name == "PauseLevel") {
					
						audio.Play ();
						Camera.main.transform.Find ("Paused").gameObject.SetActive (true);  
						GameObject.Find ("Pauser").GetComponent<Pauser> ().paused = true;   
				}

		}

		void OnMouseDrag ()
		{
				Vector3 CurrentScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, ScreenPoint.z);
				Vector3 CurrentPos = Camera.main.ScreenToWorldPoint (CurrentScreenPoint);
				transform.position = CurrentPos;
		}
}
