var damagetaken:int;
var damageinterval:float;

private var intervalcounter:float=0;
function OnTriggerEnter (other:Collider) {
	if(other.gameObject.tag=="Player" && Time.time>=intervalcounter)
	{
		other.SendMessageUpwards("ApplyDamage",damagetaken,SendMessageOptions.DontRequireReceiver);
		intervalcounter=Time.time+damageinterval;
	}
}