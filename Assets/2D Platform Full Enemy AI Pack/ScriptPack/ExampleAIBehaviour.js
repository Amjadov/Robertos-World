private var EnemyHealth:int;
private var EnemyHealthMax:int;

function Start(){
	EnemyHealthMax = EnemyHealth = GetComponent(LifeMeter).Life;
}

function Update () {
EnemyHealth = GetComponent(LifeMeter).Life;
	if(EnemyHealth >= EnemyHealthMax * 0.75)
	{
		GetComponent(Complete2DPlatformEnemyAI).isjumpingeventually=true;
		GetComponent(Complete2DPlatformEnemyAI).ischasing=true;
		GetComponent(Complete2DPlatformEnemyAI).israndomjumping=true;
	}
	
	if(EnemyHealth >= EnemyHealthMax * 0.5 && EnemyHealth < EnemyHealthMax * 0.75)
	{
		GetComponent(Complete2DPlatformEnemyAI).ischasing=false;
		GetComponent(Complete2DPlatformEnemyAI).isjumpingeventually=false;
		GetComponent(Complete2DPlatformEnemyAI).israndomjumping=false;
		GetComponent(Complete2DPlatformEnemyAI).ispatrol=true;
		GetComponent(Complete2DPlatformEnemyAI).isshooting=true;
		GetComponent(Complete2DPlatformEnemyAI).timenoshoot=2;
		GetComponent(Complete2DPlatformEnemyAI).directionaloraccurateshot=false;
	}
	
	if(EnemyHealth <= EnemyHealthMax * 0.25)
	{
		GetComponent(Complete2DPlatformEnemyAI).ischasing=true;
		GetComponent(Complete2DPlatformEnemyAI).isjumpingeventually=true;
		GetComponent(Complete2DPlatformEnemyAI).ispatrol=false;
		GetComponent(Complete2DPlatformEnemyAI).isshooting=true;
		GetComponent(Complete2DPlatformEnemyAI).timenoshoot=1;
		GetComponent(Complete2DPlatformEnemyAI).directionaloraccurateshot=true;
	}
}