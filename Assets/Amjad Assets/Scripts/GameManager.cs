
using UnityEngine;
using System.Collections;
using StartApp;
[System.Serializable]
public class GLevels
{
	// Scene.Track Information
	public string		Name		=	"";					// Name of the Level (for GUI display purposes)
	//public int			Track		=	0;					// Index into the track data array decribing the track to use for racing
	public int			HighScore		=	0;					// Number of Laps for this race
	
//	public GI_RaceTrackManager_TimeOfDay	TimeOfDay;		// Time of Day
//	public GI_RaceTrackManager_Weather		Weather;		// Weather
//	public GI_RaceTrackManager_Visibility	Visibility;		// Visibility

	public bool			Locked		=	true;				// Is this race intially locked from Quick Race Selection Screen
	// and must be unlocked by placing in top three during season.
}

public class GameManager : MonoBehaviour {
	public bool ApplyInspectorChangesToFile = false;
	public string CurrentLevel;
	public string NextLevel;
	public static GLevels[] GameLevels;
	public bool invincibleMode = false;
	public bool OpenAllLevelsMode = false;
	public bool DontShowAds = false;
	private static GameManager s_Instance = null;
	public GameObject AdmobAndroidAds;
	public int MusicMute = 0; //1 is Muted and 0 is Not
	[HideInInspector]
	public bool BackToMain = false;

	private float totalscore;
	public float TotalScore
	{
		get
		{
			return totalscore; //this.totalscore;
		}
		set
		{
			totalscore = value; //this.totalscore = value;

		}
	}
//			void OnGUI()
//			{
//		GUI.Label(new Rect(10, 200, 300, 20), "MusicMute="+ PlayerPrefs.GetInt("MusicMute").ToString() );
//				//GUI.Label(new Rect(10, 40, 300, 20), "IsPressed="+IsPressed);
//				//GUI.Label(new Rect(10, 10, 300, 20), "Mabtn.Pressed="+abtn.Pressed);
//				}
	
	void Awake () {
		GameObject obj = GameObject.Find ("Game Manager");
		if (obj && obj != this.gameObject) {
			Destroy(gameObject);
				}


		if (DontShowAds)  
						GameObject.Find ("DontShowAds").GetComponent<DontShowAdverts> ().RemoveAds ();   

		

		   

		Application.targetFrameRate = 60; 
		DontDestroyOnLoad (this);
		#if UNITY_ANDROID && !UNITY_EDITOR
		if (DontShowAds)
			return;

		if (!GameObject.Find("AdvertismentManagerPrefab"))  
			Instantiate (AdmobAndroidAds,transform.position,Quaternion.identity);

		AdvertisementHandler.EnableAds ();
		AdvertisementHandler.ShowAds ();


		//this is for StartApp
		StartAppWrapper.loadAd();
		#endif

	}
	public int GetLevelState(string LevelName)
	{
		return PlayerPrefs.GetInt(LevelName); 


		}

	public static GameManager instance {
		get {
			if (s_Instance == null) {
				// This is where the magic happens.
				//  FindObjectOfType(...) returns the first AManager object in the scene.
				s_Instance =  FindObjectOfType(typeof (GameManager)) as GameManager;
			}
			
			// If it is still null, create a new instance
			if (s_Instance == null) {
				GameObject obj = new GameObject("GameManager");
				s_Instance = obj.AddComponent(typeof (GameManager)) as GameManager;
				Debug.Log ("Could not locate an GameManager object.GameManager was Generated Automaticly.");
			}
			
			return s_Instance;
		}
	}


	public void ResetGamePrefs()
	{

		PlayerPrefs.DeleteAll(); 
		PlayerPrefs.Save();
		Debug.Log ("Settings Reset..."); 
		}

	public void ResetGameScore()
	{
		
		PlayerPrefs.SetFloat("TotalScore",0f);  
		PlayerPrefs.Save();
		Debug.Log ("Score Reset..."); 
	}

	public string GetNextLevel()
	{
		switch (CurrentLevel) {

		case "S01_E01": return "S01_E02";
		case "S01_E02": return "S01_E03";
		case "S01_E03": return "Story2";
		case "Story2": return "S01_E04";
		case "S01_E04": return "S01_E05";
		case "S01_E05": return "S01_E06";
		case "S01_E06": return "S01_E07";
		case "S01_E07": return "S01_E08";
		case "S01_E08": return "Story3";
		case "Story3": return "S01_E09";
		case "S01_E09": return "S01_E10";
		case "S01_E10": return "S01_E11";
		case "S01_E11": return "S01_E12";
		case "S01_E12": return "S01_E13";
		case "S01_E13": return "Story4";
		case "Story4": return "S01_E14";
		case "S01_E14": return "S01_E15";
		case "S01_E15": return "S01_E16";
		case "S01_E16": return "S01_E17";
		case "S01_E17": return "S01_E18";
		case "S01_E18": return "Story5";
		case "Story5": return "S01_E19";
		case "S01_E19": return "S01_E20";
		case "S01_E20": return "Story6";
		case "Story6": return "Main";
		case "S01_E21": return "S01_E22";
		case "S01_E22": return "S01_E23";
		case "S01_E23": return "S01_E24";
		case "S01_E24": return "S01_E25";
		case "S01_E25": return "S01_E26";
		default: return "S01_E01";
		}

		}

	public string GetLevelDescription()
	{
		switch (CurrentLevel) {
		case "Story": return "Roberto And The Witch";
		case "Main": return "        ";
		case "S01_E01": return "Episode 01";
		case "S01_E02": return "Episode 02";
		case "S01_E03": return "Episode 03";
		case "Story2": return "Roberto Meets Billy";
		case "S01_E04": return "Episode 04";
		case "S01_E05": return "Episode 05";
		case "S01_E06": return "Episode 06";
		case "S01_E07": return "Episode 07";
		case "S01_E08": return "Episode 08";
		case "Story3": return "Out Camping";
		case "S01_E09": return "Episode 09";
		case "S01_E10": return "Episode 10";
		case "S01_E11": return "Episode 11";
		case "S01_E12": return "Episode 12";
		case "S01_E13": return "Episode 13";
		case "Story4": return "Kong Joins The Gang";
		case "S01_E14": return "Episode 14";
		case "S01_E15": return "Episode 15";
		case "S01_E16": return "Episode 16";
		case "S01_E17": return "Episode 17";
		case "S01_E18": return "Episode 18";
		case "Story5": return "Honey, I...";
		case "S01_E19": return "Episode 19";
		case "S01_E20": return "Episode 20";
		case "Story6": return "Sightseeing";
		case "S01_E21": return "Episode 21";
		case "S01_E22": return "Episode 22";
		case "S01_E23": return "Episode 23";
		case "S01_E24": return "Episode 24";
		case "S01_E25": return "Episode 25";
		default: return "Episode 01";
		}
		
	}
	public string unlockNextLevel()
	{
		switch (CurrentLevel) {
			
		case "S01_E01": return "S01_E02";
		case "S01_E02": return "S01_E03";
		case "S01_E03": return "S01_E04";
		case "S01_E04": return "S01_E05";
		case "S01_E05": return "S01_E06";
		case "S01_E06": return "S01_E07";
		case "S01_E07": return "S01_E08";
		case "S01_E08": return "S01_E09";
		case "S01_E09": return "S01_E10";
		case "S01_E10": return "S01_E11";
		case "S01_E11": return "S01_E12";
		case "S01_E12": return "S01_E13";
		case "S01_E13": return "S01_E14";
		case "S01_E14": return "S01_E15";
		case "S01_E15": return "S01_E16";
		case "S01_E16": return "S01_E17";
		case "S01_E17": return "S01_E18";
		case "S01_E18": return "S01_E19";
		case "S01_E19": return "S01_E20";
		case "S01_E20": return "Main";
		//case "S01_E20": return "S01_E21";
		case "S01_E21": return "S01_E22";
		case "S01_E22": return "S01_E23";
		case "S01_E23": return "S01_E24";
		case "S01_E24": return "S01_E25";
		case "S01_E25": return "S01_E26";
		default: return "S01_E01";
		}


	}
		// Ensure that the instance is destroyed when the game is stopped in the editor.
	void OnApplicationQuit() {
		//  
		s_Instance = null;
	}
	public void ShowBannerAds (bool ShowAd = false)
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		if (DontShowAds)
			return;

		if (ShowAd == true) {
			AdvertisementHandler.EnableAds ();
			AdvertisementHandler.ShowAds ();
			
		} else {
			AdvertisementHandler.HideAds();
			AdvertisementHandler.DisableAds();
		}
		
		#endif
		
		
	}
	public void ShowInterstitialAds()
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		if (DontShowAds)
			return;
		StartAppWrapper.showAd();
		StartAppWrapper.loadAd();
		#endif

	}
	void OnLevelWasLoaded (int level)
	{
		//Debug.Log ("Loaded Level: " + Application.loadedLevelName);
		ShowHideBannerAds ();
	}
	public void ShowHideBannerAds ()
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		if (DontShowAds)
			return;
		if (Application.loadedLevelName == "Main") {  
			ShowBannerAds(true);
			ShowInterstitialAds();
		} else if(Application.loadedLevelName != "Main") {
			
			ShowBannerAds(false);
		}
		
		#endif
		
		
	}
}
