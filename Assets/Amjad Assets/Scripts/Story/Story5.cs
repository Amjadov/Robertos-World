using UnityEngine;
using System.Collections;

public class Story5 : MonoBehaviour {
	public Transform player;
	public Transform Bull;
	public Transform Kong;
	private Animator playerAnim;
	private Animator BullAnim;
	private Animator KongAnim;
	public Transform Background;
	public Transform Speech1;
	public Transform Speech2;

	IEnumerator Start() {
		playerAnim = player.GetComponent<Animator> (); 
		BullAnim = Bull.GetComponent<Animator> (); 
		KongAnim = Kong.GetComponent<Animator> (); 
		StartCoroutine("PrepareForDialogue_WithWoman");
		yield return new WaitForSeconds(3);
		ShowDialogue (1);
		yield return new WaitForSeconds(18);

		StartCoroutine("PrepareForDialogue");
		yield return new WaitForSeconds(5);

		ShowDialogue (2);
		StartCoroutine("PrepareForDialogue_WithWoman");
		yield return new WaitForSeconds(27);
		StartCoroutine("PrepareForDialogue");
		yield return new WaitForSeconds(5);

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
		yield return null;
	}
	IEnumerator PrepareForDialogue() {
		if (facingRight (player))
			Flip (player);
		playerAnim.SetFloat ("Speed", 0);
		BullAnim.SetTrigger  ("Idle");
		KongAnim.SetTrigger ("Idle");
		yield return null;
	}
	IEnumerator PrepareForDialogue_WithWoman() {
		if (!facingRight (player))
			Flip (player);
		playerAnim.SetFloat ("Speed", 0);
		BullAnim.SetTrigger  ("Idle");
		KongAnim.SetTrigger ("Idle");
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
//			Speech3.gameObject.SetActive(true);
			break;
		case 4:
//			Speech4.gameObject.SetActive(true);
			break;
		case 5:
//			Speech5.gameObject.SetActive(true);
			break;
		default:
//			Console.WriteLine("Default case");
			break;
		}
	}
//	void Start() {
//		playerAnim = player.GetComponent<Animator> (); 
//		BullAnim = Bull.GetComponent<Animator> (); 
////		StartCoroutine("Routine1");
////		yield return new WaitForSeconds(5);
////		StopCoroutine("PrepareForDialogue");
//		Invoke ("Routine1", 1f);
//		Invoke ("PrepareForDialogue", 5f);
//	}
//	void Routine1() {
//		if (!facingRight (player))
//			Flip (player);
//		playerAnim.SetFloat ("Speed", 1);
//		BullAnim.SetBool ("Charge", true);
//
//	}
//	void PrepareForDialogue() {
//		if (facingRight (player))
//			Flip (player);
//		playerAnim.SetFloat ("Speed", 0);
//		BullAnim.SetBool ("Charge", false);
//	}

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
