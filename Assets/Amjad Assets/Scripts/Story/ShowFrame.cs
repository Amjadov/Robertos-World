using UnityEngine;
using System.Collections;

public class ShowFrame : MonoBehaviour {
	public GameObject Frame1;
	public GameObject Frame2;
	public GameObject Frame3;
	public GameObject Frame4;



	public void ShowtheFrame()
	{
		if (transform.parent.gameObject.name == "Frame_1")
						Frame2.SetActive (true);//    .transform.Find("Part1_0").gameObject.SetActive(true);

		if (transform.parent.gameObject.name == "Frame_2")
			Frame3.SetActive (true);

		if (transform.parent.gameObject.name == "Frame_3")
			Frame4.SetActive (true);

		if (transform.parent.gameObject.name == "Frame_4")
			Invoke ("EndStory",5f);



	}
	void EndStory(){
		SkipBtn.current.EndScene (); 
	}
}
