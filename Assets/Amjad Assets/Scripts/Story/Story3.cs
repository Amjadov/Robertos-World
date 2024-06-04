using UnityEngine;
using System.Collections;

public class Story3 : MonoBehaviour {
	public Transform player;
	public Transform Bull;
	private Animator playerAnim;
	private Animator BullAnim;
	public Transform Background;
	public Transform Speech1;
	public Transform Speech2;
	IEnumerator Start() {
		playerAnim = player.GetComponent<Animator> (); 
		BullAnim = Bull.GetComponent<Animator> (); 

		StartCoroutine("PrepareForDialogue");
		yield return new WaitForSeconds(5);
		ShowDialogue (1);

		yield return new WaitForSeconds(35);

		ShowDialogue (2);

		yield return new WaitForSeconds(17);


		Camera.main.GetComponent<CameraFadeInOut> ().FadeOUT = true;  
		yield return new WaitForSeconds (5);


		SkipBtn.current.EndScene ();  

	}
	IEnumerator Routine1() {
		if (!facingRight (player))
						Flip (player);
		playerAnim.SetFloat ("Speed", 1f);
		BullAnim.SetTrigger ("Charge");
		Background.GetComponent<ScrollScript_Auto>().AutoScroll_Style2 = true; 
		//Background.GetComponent<ScrollScript_Auto> ().Scroll (1f); 
		yield return null;
	}
	IEnumerator PrepareForDialogue() {
		if (facingRight (player))
			Flip (player);
		playerAnim.SetFloat ("Speed", 0);
		BullAnim.SetTrigger  ("Idle");
		Background.GetComponent<ScrollScript_Auto>().AutoScroll_Style2 = false; 
		yield return null;
	}
	void ShowDialogue(int DialogueNo) {
		switch (DialogueNo)
		{
		case 1:
			Speech1.gameObject.SetActive(true);  
			break;
		case 2:
			Speech2.gameObject.SetActive(true);
			break;
		default:
//			Console.WriteLine("Default case");
			break;
		}
	}

	bool facingRight (Transform target)
	{
		if (target.localScale.x < 0) {
			return true;
		} else {
			return false;
		}
	}
	
	public void Flip (Transform target)
	{
		// Multiply the x component of localScale by -1.
		Vector3 xScale = target.localScale;
		xScale.x *= -1;
		target.localScale = xScale;
	}


}
