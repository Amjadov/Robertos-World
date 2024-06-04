using UnityEngine;
using System.Collections;

public class Enemy_Throw : MonoBehaviour {
	public Rigidbody2D[] ObjectToThrow;
	public Transform Spawner;
	public float AimHigher = 0;                  // this is used if the enemy is shorter than the player and we need to aim higher
	public float speed = 10f;				// The speed the fireball will fire at.
	public bool UseObjectPooler = false;
	private Transform frontcheck;
	private bool PlayerFound = false;
	private float lastHitTime;
	private Animator anim;	
	private Vector3 shootingDirection;
	public float DelayBetweenShootings = 2f;
	private Transform target;
	private float t = 0f;
	private EnemyHealth enemyh;
	void Start () {
		frontcheck = transform.Find ("frontCheck");
		anim = GetComponent<Animator> ();
		target = GameObject.FindGameObjectWithTag ("Player").transform; 
		enemyh = GetComponent<EnemyHealth> ();  
	}


	void FixedUpdate () {

	
		if (Vector3.Distance (target.transform.position, transform.position) <= Vector3.Distance (frontcheck.position, transform.position)) {

						
						Fire ();

				}
	
	}
	void Fire()
	{
		if (enemyh.dead)
						return;

		if (Time.time > lastHitTime + DelayBetweenShootings) {

		shootingDirection = new Vector3(target.transform.position.x,target.transform.position.y + AimHigher,target.transform.position.z)  -transform.position;


			if ((target.position.x <= transform.position.x && !facingRight()) || (target.position.x > transform.position.x && facingRight())){
				if (anim)
			anim.SetTrigger ("Fire");
				if (UseObjectPooler)
				{
					GameObject obj = ObjectPoolerScript.current.GetPooledObject();
					obj.transform.position = Spawner.position;
					obj.GetComponent<Apple>().UseObjectPooler = true; 
					obj.SetActive(true);
					obj.rigidbody2D.velocity= shootingDirection.normalized * speed;

				}
				else{
				int i = Random.Range (0, ObjectToThrow.Length);
				Rigidbody2D bulletInstance = Instantiate (ObjectToThrow[i], Spawner.position,  Quaternion.identity) as Rigidbody2D; //Quaternion.Euler (new Vector3 (0, 0, 0))) as Rigidbody2D;//Quaternion.LookRotation(shootingDirection)) as Rigidbody2D;
				bulletInstance.rigidbody2D.velocity= shootingDirection.normalized * speed;
				}
			
			lastHitTime = Time.time;
			audio.Play(); 
				}
			}

	}

	bool facingRight()
	{
		if (transform.localScale.x < 0) {
		return true;
	}else {
		return false;}
}

	void FlipFireBall (Transform fireball)
	{
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		fireball.localScale = theScale;
	}
}
