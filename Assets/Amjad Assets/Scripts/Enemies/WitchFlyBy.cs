using UnityEngine;
using System.Collections;

public class WitchFlyBy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DestroyObject (gameObject, 10);  
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position = new Vector3 (transform.position.x + 0.3f, transform.position.y, transform.position.z); 
	
	}
}
