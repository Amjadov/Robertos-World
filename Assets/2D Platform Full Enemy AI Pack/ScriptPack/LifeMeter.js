//ENG: Simple Health script. you can use it in all destroyable objets(playres, enemies, background elements...) and its chunks. You can Instantiate a Object´s replacement if you want (ragdolls or chunks)
//ESP: Simple script de salud, puedes usarlo con cualquier objeto destruible (jugadores, enemigos, objetos de escenario...) y sus "trozos". puedes instanciar un reemplazo si quieres (ragdolls o cascotes).
public var Life:int;	//ENG: Object´s Life
							//ESP: Salud del objeto
var Replacement:GameObject; 	//ENG: Object´s replacement when dies
											//ESP: reemplazo del objeto cuando muere
function Update(){
//ENG: if Life is under or equal zero, the object is destroyed, and if have replacement, this is instantiated.
//ESP: si la vida es igual o menos a cero, el objeto es destruido y, si tiene reemplazo, éste es instanciado.
	if(Life<=0)
	{
		Destroy(this.gameObject);
		if(Replacement)
		{
			Instantiate(Replacement,transform.position,transform.rotation);
		}			
	}
}
//ENG: When the function is called, rest a determinate part of the health
//ESP: Cuando la funcion es llamada, resta una determinada parte de la salud
function ApplyDamage (liferested:int) {

	Life-=liferested;

}