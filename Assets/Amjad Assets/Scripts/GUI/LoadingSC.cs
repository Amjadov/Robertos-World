using UnityEngine;
using System.Collections;

public class LoadingSC : MonoBehaviour
{
		private int LoadStory1 = 0;
		private int LoadStory2 = 0;
		private int LoadStory3 = 0;
		private int LoadStory4 = 0;
		private int LoadStory5 = 0;
		// Use this for initialization
		void Start ()
		{
				LoadStory1 = PlayerPrefs.GetInt ("LoadStory1");
				LoadStory2 = PlayerPrefs.GetInt ("LoadStory2");
				LoadStory3 = PlayerPrefs.GetInt ("LoadStory3");
				LoadStory4 = PlayerPrefs.GetInt ("LoadStory4");
				LoadStory5 = PlayerPrefs.GetInt ("LoadStory5");
				if (GameManager.instance.CurrentLevel == null) {
						GameManager.instance.CurrentLevel = "Main";
				}

		GameObject.Find ("LevelDescription").guiText.text = GameManager.instance.GetLevelDescription ();  
		GameObject.Find ("LevelDescriptionShadow").guiText.text = GameManager.instance.GetLevelDescription ();  
//		Debug.Log (GameManager.instance.CurrentLevel);
//		Debug.Log (LoadStory1);

				if (GameManager.instance.CurrentLevel == "S01_E01" && LoadStory1 == 0) {
			
						PlayerPrefs.SetInt ("LoadStory1", 1);
						PlayerPrefs.Save ();
						Application.LoadLevel ("Story");
				} else if (GameManager.instance.CurrentLevel == "S01_E04" && LoadStory2 == 0) {

						PlayerPrefs.SetInt ("LoadStory2", 1);
						PlayerPrefs.Save ();
						Application.LoadLevel ("Story2");
				} else if (GameManager.instance.CurrentLevel == "S01_E09" && LoadStory3 == 0) {
			
						PlayerPrefs.SetInt ("LoadStory3", 1);
						PlayerPrefs.Save ();
						Application.LoadLevel ("Story3");
				} else if (GameManager.instance.CurrentLevel == "S01_E14" && LoadStory4 == 0) {
			
						PlayerPrefs.SetInt ("LoadStory4", 1);
						PlayerPrefs.Save ();
						Application.LoadLevel ("Story4");
		} else if (GameManager.instance.CurrentLevel == "S01_E19" && LoadStory5 == 0) {
			
			PlayerPrefs.SetInt ("LoadStory5", 1);
			PlayerPrefs.Save ();
			Application.LoadLevel ("Story5");
				} else {
						Application.LoadLevel (GameManager.instance.CurrentLevel);
				}
	
		
		
		}
	

}
