public var jumpforce:float=10;
public var speed:float=6;
public var jumpeveryseconds:float=3;
public var jumpeverysecondsmin:float=2;
public var jumpeverysecondsmax:float=3;
public var patrolseconds:float=3;
public var changedirectiondistance:float=5;
public var stopswhenshoottime:float=1;
public var timenoshoot:float=0.5;
public var shootingdistance:float=10;
public var proyectilevelocity:float=10;
private var facingRight: boolean = true;
public var isjumping:boolean;
public var isjumpingeventually:boolean;
public var israndomjumping:boolean;
public var ispatrol:boolean;
public var ischasing:boolean;
public var isshooting:boolean;
public var leftorright:boolean;
public var stopswhenshoot:boolean;
public var directionaloraccurateshot:boolean;
public var lookAtTarget:boolean=false;
public var DoNotFlip:boolean = false;
var proyectile:GameObject;
var GunPos:GameObject;
private var secondscounter:float=0;
private var timewithoutshoot:float = 0;
private var timestoppedwhenshoot:float=0;
private var jumpinseconds:float=0;
private var chasetarget:GameObject;
private var patroltime:float=0;
private var chaseorpatrolprevious:int;
public var SpriteRen:SpriteRenderer;

function Start()
{	
	chasetarget = GameObject.FindWithTag("Player");
	//Physics.IgnoreCollision(this.collider, chasetarget.collider);
	jumpinseconds=Random.Range(jumpeverysecondsmin,jumpeverysecondsmax);
}
function FixedUpdate () {
	
	if(ischasing)
		Chasing(chasetarget);
	if(ispatrol)
		Patrol();
	if(isshooting)
		Shooting(chasetarget);
	if(!isjumping)
		if(isjumpingeventually)
		{secondscounter+=Time.deltaTime;
			if(israndomjumping)
			{
				if(secondscounter>=jumpinseconds)
				{
					rigidbody2D.velocity.y=jumpforce;
					isjumping=true;
					secondscounter=0;
					jumpinseconds=Random.Range(jumpeverysecondsmin,jumpeverysecondsmax);
				}
			}
			else
				if(secondscounter>=jumpeveryseconds)
				{
					rigidbody2D.velocity.y=jumpforce;
					isjumping=true;
					secondscounter=0;
				}
		}
		if (lookAtTarget)
		{
		
		lookAtTheTarget(chasetarget);
		}
		
		
				
				
}
function lookAtTheTarget(target:GameObject)
{
if(transform.position.x<=target.transform.position.x-changedirectiondistance)
	{
		if (facingRight)
		{Flip();}
		
	}
	if(transform.position.x>=target.transform.position.x+changedirectiondistance)
	{
		if (!facingRight)
		{Flip();}
	}


}
function Patrol()
{
	patroltime+=Time.deltaTime;
	if(leftorright)
		{rigidbody2D.velocity.x=speed;
		if (!facingRight)
		{Flip();}
		}
	else{
		rigidbody2D.velocity.x=-speed;
		if (facingRight)
		{Flip();}
		}
	
	
	
	if(patroltime>=patrolseconds)
	{
		if(leftorright)
			leftorright=false;
		else
			leftorright=true;
		patroltime=0;
	}
	
}

function Chasing(target:GameObject)
{
	if(leftorright)
		{rigidbody2D.velocity.x=speed;
		if (!facingRight)
		{Flip();}
		}
	else{
		rigidbody2D.velocity.x=-speed;
		if (facingRight)
		{Flip();}
		}
		
	if(transform.position.x<=target.transform.position.x-changedirectiondistance)
	{
		leftorright=true;
	}
	if(transform.position.x>=target.transform.position.x+changedirectiondistance)
	{
		leftorright=false;
	}
}

function Shooting(target:GameObject)
{
	if(directionaloraccurateshot)
		var shootdirection=target.transform.position-transform.position;
	
		if(stopswhenshoot)
		{
			if(timestoppedwhenshoot==0)
			{
				if(Vector3.Distance(target.transform.position,transform.position)<=shootingdistance)
				{	
					if(ischasing)
					{
						chaseorpatrolprevious = 1;
						ischasing=false;
					}
					else if(ispatrol)
					{
						chaseorpatrolprevious = 2;
						ispatrol=false;
					}
					if(directionaloraccurateshot)
						Shot(shootdirection);
					else
						Shot();

					timestoppedwhenshoot+=Time.deltaTime;
				}
			}
			if(timestoppedwhenshoot>0)
				timestoppedwhenshoot+=Time.deltaTime;
			if(timestoppedwhenshoot>=stopswhenshoottime)
			{
				if(chaseorpatrolprevious==1)
					ischasing=true;
				if(chaseorpatrolprevious==2)
					ispatrol=true;
				timestoppedwhenshoot=0;
			}
		}
	else if (!stopswhenshoot)
	if(Vector3.Distance(target.transform.position,transform.position)<=shootingdistance)
	{
		timewithoutshoot+=Time.deltaTime;
		if(timewithoutshoot>=timenoshoot)
		{
			if(directionaloraccurateshot)
				Shot(shootdirection);
			else
				Shot();
			timewithoutshoot=0;
		}
	}	
}

function Shot(){
	if (SpriteRen)
	if (SpriteRen.enabled == false)
	return;
	 
	var Proyectile:GameObject;
	if (!GunPos)
	GunPos = gameObject; 
	
	Proyectile= Instantiate(proyectile,GunPos.transform.position,Quaternion.LookRotation(Vector3.right));
	//Physics.IgnoreCollision(transform.root.collider, Proyectile.collider);
	if(leftorright)
		Proyectile.rigidbody2D.velocity.x=proyectilevelocity;
	else
		Proyectile.rigidbody2D.velocity.x=-proyectilevelocity;
}

function Shot(direction:Vector3)
{
	var Proyectile:GameObject;
	if (!GunPos)
	GunPos = gameObject; 
	
	Proyectile= Instantiate(proyectile,GunPos.transform.position,Quaternion.LookRotation(direction));
	Proyectile.rigidbody2D.velocity=direction.normalized*proyectilevelocity;
	//Physics.IgnoreCollision(transform.root.collider, proyectile.collider);
}

function OnCollisionStay(collision:Collision)
{
	for (var contact : ContactPoint in collision.contacts) 
	{
		if(contact.normal.y >0.5)
			isjumping=false;
	}
}

function Flip ()
	{
	if (!DoNotFlip){
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		var theScale: Vector3;
		theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		}
	}