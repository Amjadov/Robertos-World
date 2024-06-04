using UnityEngine;
using System.Collections;

public class SpawnRock : MonoBehaviour {
	private float lastSpawnTime;
	public float DelayBetweenSpawns = 2f;
	public float DelayBetweenGroupSpawns = 2f;
	public Rigidbody2D RockObject;
	public Transform RockSpawner;
	public AudioClip AngryScream;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void Spawn() {
		//if (Time.time > lastSpawnTime + DelayBetweenSpawns) {

		Rigidbody2D bulletInstance = Instantiate (RockObject, RockSpawner.position, Quaternion.Euler (new Vector3 (0, 0, 0))) as Rigidbody2D;
			Camera.main.GetComponent<PerlinShake>().PlayShake();  
			//lastSpawnTime = Time.time;

				//}
	}
	public void AudioHitFloor()
	{
		audio.Play ();

	}
	public void AudioAngryScream()
	{
		AudioSource.PlayClipAtPoint (AngryScream, transform.position,1f); 
		
	}
}
