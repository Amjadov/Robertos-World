using UnityEngine;
using System.Collections;

public class LadderScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.transform.tag == "Player") {

			col.transform.GetComponent<PlayerControl>().OnLadder = true;
				}

	}
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.transform.tag == "Player") {
			
			col.transform.GetComponent<PlayerControl>().OnLadder = false;
		}
		
	}
}
