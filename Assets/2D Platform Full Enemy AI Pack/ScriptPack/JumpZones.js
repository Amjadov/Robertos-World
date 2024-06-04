var jumpforce:float=10;
var isdifierentjumpforcethancharacters:boolean=false;
function OnTriggerEnter (other:Collider) {
	if(other.rigidbody && other.gameObject.tag == "Enemy" && other.GetComponent(Complete2DPlatformEnemyAI).isjumping==false)
	{	
		if(isdifierentjumpforcethancharacters)
			other.rigidbody.velocity.y=jumpforce;
		else
			other.rigidbody.velocity.y=other.GetComponent(Complete2DPlatformEnemyAI).jumpforce;
		other.GetComponent(Complete2DPlatformEnemyAI).isjumping=true;
	}
}