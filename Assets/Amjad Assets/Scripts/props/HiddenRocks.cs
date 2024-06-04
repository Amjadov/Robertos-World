using UnityEngine;
using System.Collections;

public class HiddenRocks : MonoBehaviour {
	public Transform RocksObject;
	private int iCounter  = 0;
	void Awake()
	{
		RocksObject.gameObject.SetActive (false); 
		}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.tag == "Player") {
			iCounter += 1;
				}

		if (iCounter > 10) {
			RocksObject.gameObject.SetActive (true); 
			Destroy (gameObject);  



				}

	}
}
