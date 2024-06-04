using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Camera.main.transform.FindChild ("SelectLevelWindow").gameObject.SetActive (false);
		Camera.main.transform.FindChild ("SelectLevelWindow2").gameObject.SetActive (false);
		Camera.main.transform.FindChild ("WorldSelect").gameObject.SetActive (false);
		Camera.main.transform.FindChild ("MainMenu").gameObject.SetActive (true);
	}

}
