using UnityEngine;
using System.Collections;

public class Story4 : MonoBehaviour {
	public Transform player;
	public Transform Bull;
	public Transform Kong;
	private Animator playerAnim;
	private Animator BullAnim;
	private Animator KongAnim;
	public Transform Background;
	public Transform Speech1;
	public Transform Speech2;
	public Transform Speech3;

	IEnumerator Start() {
		playerAnim = player.GetComponent<Animator> (); 
		BullAnim = Bull.GetComponent<Animator> (); 
		KongAnim = Kong.GetComponent<Animator> (); 
		StartCoroutine("Routine1");
		yield return new WaitForSeconds(5);
		StopCoroutine("Routine1");
		StartCoroutine("PrepareForDialogue");
		ShowDialogue (1);

		yield return new WaitForSeconds(14);

		StartCoroutine("Routine1");
		yield return new WaitForSeconds (4);
		StopCoroutine("Routine1");
		StartCoroutine("PrepareForDialogue");
		ShowDialogue (2);

		yield return new WaitForSeconds(5);

		StartCoroutine("Routine1");
		yield return new WaitForSeconds (4);
		StopCoroutine("Routine1");
		StartCoroutine("PrepareForDialogue");
		ShowDialogue (3);
		yield return new WaitForSeconds (21);


		StartCoroutine("Routine1");
		yield return new WaitForSeconds (3);
		Camera.main.GetComponent<CameraFadeInOut> ().FadeOUT = true;  
		yield return new WaitForSeconds (5);
		StopCoroutine("Routine1");

		SkipBtn.current.EndScene ();  

	}
	IEnumerator Routine1() {
		if (!facingRight (player))
						Flip (player);
		playerAnim.SetFloat ("Speed", 1f);
		BullAnim.SetTrigger ("Charge");
		KongAnim.SetTrigger ("Run");
		Background.GetComponent<ScrollScript_Auto>().AutoScroll_Style2 = true; 
		//Background.GetComponent<ScrollScript_Auto> ().Scroll (1f); 
		yield return null;
	}
	IEnumerator PrepareForDialogue() {
		if (facingRight (player))
			Flip (player);
		playerAnim.SetFloat ("Speed", 0);
		BullAnim.SetTrigger  ("Idle");
		KongAnim.SetTrigger ("Idle");
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
		case 3:
			Speech3.gameObject.SetActive(true);
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
