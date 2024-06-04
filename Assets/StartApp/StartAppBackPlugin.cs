using UnityEngine;
using System.Collections;
using StartApp;

public class StartAppBackPlugin : MonoBehaviour{
	public static StartAppBackPlugin current;
	private DontShowAdverts ADVClass;
	#if UNITY_ANDROID && !UNITY_EDITOR
	void Awake()
	{
		current = this;
		
	}
	void Start () {
		ADVClass = GameObject.Find ("DontShowAds").GetComponent<DontShowAdverts> ();
			if (ADVClass.DontShowAdds)
			return;

		StartAppWrapper.loadAd();
		
    }
	void Update () {
		if (ADVClass.DontShowAdds)
			return;

		if (Input.GetKeyUp(KeyCode.Escape) == true ) {
			 showAdAndExit();
		}
    }
	
    public void showAdAndExit() {
		if (ADVClass.DontShowAdds)
			return;

		StartAppWrapper.showAdAndExit();
	}
	#endif

}