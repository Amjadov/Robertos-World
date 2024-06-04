using UnityEngine;
using System.Collections;

public class SpeechSpawner : MonoBehaviour {
	public Transform SpeechObject;
	void Awake()
	{
		SpeechObject.gameObject.SetActive (false); 
		}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.tag == "Player") {
						SpeechObject.gameObject.SetActive (true); 
						Destroy (gameObject);  
				}

	}
}
