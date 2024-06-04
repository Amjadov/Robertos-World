
using UnityEngine;
using UnityEditor;
using System.Collections; 

[CustomEditor(typeof(GameManager))]
class DefaultPlayerPrefs : Editor {
	public override void OnInspectorGUI() {
		DrawDefaultInspector (); 


		GameManager myScript = (GameManager)target;
		if (GUILayout.Button ("Reset Game Prefs"))
						myScript.ResetGamePrefs ();

		if (GUILayout.Button ("Reset Game Score"))
						myScript.ResetGameScore ();   
	}
}