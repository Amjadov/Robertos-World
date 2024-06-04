using UnityEngine;
using System.Collections;

public class Drag : MonoBehaviour {
	private Vector3 ScreenPoint;
	private string TouchObjName;


	void Update () {
				if (Input.GetMouseButtonDown (0)) {
						Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
						RaycastHit hit;
						if (Physics.Raycast (ray, out hit)) {
								Debug.Log ("Name = " + hit.collider.name);
								Debug.Log ("Tag = " + hit.collider.tag);
								Debug.Log ("Hit Point = " + hit.point);
								Debug.Log ("Object position = " + hit.collider.gameObject.transform.position);
								Debug.Log ("--------------");
						}
				}

				foreach (Touch touch in Input.touches) {
						if (Input.GetMouseButtonDown (0)) {
								if (touch.phase == TouchPhase.Began) {
										Ray ray = Camera.main.ScreenPointToRay (touch.position);
										RaycastHit hit;
										if (Physics.Raycast (ray, out hit)) {
												Debug.Log ("Name = " + hit.collider.name);
												Debug.Log ("Tag = " + hit.collider.tag);
												Debug.Log ("Hit Point = " + hit.point);
												Debug.Log ("Object position = " + hit.collider.gameObject.transform.position);
												Debug.Log ("--------------");
												TouchObjName =  hit.collider.name;
										}
								}
						}
				}
		}
	void OnMouseDown () {
		ScreenPoint = Camera.main.WorldToScreenPoint (gameObject.transform.position);
		Debug.Log ("Mouse Down");


	}
		void OnGUI()
		{
		GUI.Label(new Rect(10,10,500,50),"Obj: "+TouchObjName);
		//GUI.Label(new Rect(10,40,500,50),"9 ButtonYAxis: "+ Input.mousePosition.y);
				//GUI.Label(new Rect(10,70,540,50),"JS Connected: "+GLOBAL.isJSConnected);
		}
	void OnMouseDrag(){
		Vector3 CurrentScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, ScreenPoint.z);
		Vector3 CurrentPos = Camera.main.ScreenToWorldPoint (CurrentScreenPoint);
		transform.position = CurrentPos;
		}
}
