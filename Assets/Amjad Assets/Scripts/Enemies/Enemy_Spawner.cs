using UnityEngine;
using System.Collections;

public class Enemy_Spawner : MonoBehaviour
{
		public Rigidbody2D Enemy;
		private float lastSpawnTime;
		public float DelayBetweenSpawns = 2f;
		private Transform Spawner;
		private float t = 0f;
		public bool UseObjectPooler = false;
	public bool SpawnToRight = false;
		// Use this for initialization
		void Start ()
		{
				Spawner = transform.Find ("EnemySpawner");
				lastSpawnTime = Time.time;
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (Time.time > lastSpawnTime + DelayBetweenSpawns) {
						if (UseObjectPooler) {
								GameObject obj = ObjectPoolerScript.current.GetPooledObject ();
								if (obj) {
										obj.transform.position = Spawner.position;
										if (obj.GetComponent<EnemyHealth> ()) {
												obj.GetComponent<EnemyHealth> ().UseObjectPooler = true; 
										} else if (obj.GetComponent<BombScript> ()) {
												obj.GetComponent<BombScript> ().UseObjectPooler = true;
						obj.GetComponent<BombScript> ().Speed = 10f;
						if (SpawnToRight)
						{
							obj.GetComponent<BombScript> ().MoveLeft = false;
						}

										}
										obj.SetActive (true);
										lastSpawnTime = Time.time;
								}
				
						} else {
								Rigidbody2D bulletInstance = Instantiate (Enemy, Spawner.position, Quaternion.Euler (new Vector3 (0, 0, 0))) as Rigidbody2D;
								lastSpawnTime = Time.time;
						}
	
				}
		}
}
