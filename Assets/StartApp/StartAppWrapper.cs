using UnityEngine;
using System.Collections;

namespace StartApp {

public class StartAppWrapper : MonoBehaviour {
		#if UNITY_ANDROID && !UNITY_EDITOR

	private static string developerId;
	private static string applicatonId;
    
	private static AndroidJavaClass jc;
	private static AndroidJavaObject jo;
	private static AndroidJavaObject wrapper;

		public interface AdEventListener{
			void onReceiveAd();
			void onFailedToReceiveAd();
		}

		public interface AdDisplayListener{
			void adHidden();
			void adDisplayed();
		}

		/* Implementation of Ad Event Listener for Unity */
		private class ImplementationAdEventListener : AndroidJavaProxy{
			private AdEventListener listener = null;
			public ImplementationAdEventListener(AdEventListener listener) : base("com.startapp.android.publish.AdEventListener"){
				this.listener = listener;
			}
			
			void onReceiveAd(AndroidJavaObject ad){
				if (listener != null){
					listener.onReceiveAd();
				}
			}

			void onFailedToReceiveAd(AndroidJavaObject ad){
				if (listener != null){
					listener.onFailedToReceiveAd();
				}
			}
		}

		/* Implementation of Ad Display Listener for Unity */
		private class ImplementationAdDisplayListener : AndroidJavaProxy{
			private AdDisplayListener listener = null;
			public ImplementationAdDisplayListener(AdDisplayListener listener) : base("com.startapp.android.publish.AdDisplayListener"){
				this.listener = listener;
			}

			void adHidden(AndroidJavaObject ad){
				if (listener != null){
					listener.adHidden();
				}
			}

			void adDisplayed(AndroidJavaObject ad){
				if (listener != null){
					listener.adDisplayed();
				}
			}
		}
		
	public enum BannerPosition{
		BOTTOM,
		TOP
	};
	
	public enum BannerType{
		AUTOMATIC,
		STANDARD,
		THREED
	};

	public static AndroidJavaObject getWrapper() {
		if (wrapper == null) {
			jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); 
			jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
			wrapper = new AndroidJavaObject("com.apperhand.unity.wrapper.InAppWrapper", jo);
				
			init ();
		}
		return wrapper;
		
		}

		public static void loadAd(AdEventListener listener) {
			getWrapper();
			wrapper.Call("loadAd", new []{new ImplementationAdEventListener(listener)});
		}
		
		public static void loadAd() {
			loadAd (null);
		}
		
		public static bool showAd(AdDisplayListener listener) {
			getWrapper();
			return wrapper.Call<bool>("showAd", new object[] {new ImplementationAdDisplayListener(listener)});
		}
		
		public static bool showAd() {
			getWrapper();
			return wrapper.Call<bool>("showAd");
		}
		
		public static bool showAdAndExit() {
			if (GameObject.Find ("DontShowAds").GetComponent<DontShowAdverts> ().DontShowAdds){
				return false;
			}
			getWrapper();
			return wrapper.Call<bool>("showAd", true);
		}
	
	public static void doHome() {
		getWrapper();
		wrapper.Call("doHome");
	}
	
	public static void addBanner() {
		addBanner(BannerType.AUTOMATIC, BannerPosition.BOTTOM);
	}
	
	public static void addBanner(BannerType bannerType, BannerPosition position) {
		int pos = 1;
		int type = 1;
		// Select position
		switch(position){
			case BannerPosition.BOTTOM:
				pos = 1;
				break;
			case BannerPosition.TOP:
				pos = 2;
				break;
		}
		AndroidJavaObject objPosition = new AndroidJavaObject("java.lang.Integer", pos);
			
			
		// Select type
		switch(bannerType){
			case BannerType.AUTOMATIC:
				type = 1;
				break;
			case BannerType.STANDARD:
				type = 2;
				break;
			case BannerType.THREED:
				type = 3;
				break;
		}
		AndroidJavaObject objType = new AndroidJavaObject("java.lang.Integer", type);
			
			
		getWrapper();
		wrapper.Call("addBanner", new []{ objType, objPosition });
	}
		
	public static void init(){	
		if (!initUserData()){
				throw new System.ArgumentException("Error in initializing Application ID or Developer ID, Verify you initialized them in StartAppData in resources");
		}
			
		AndroidJavaObject jAppId = new AndroidJavaObject("java.lang.String", applicatonId);
		AndroidJavaObject jDevId = new AndroidJavaObject("java.lang.String", developerId);
			
		wrapper.Call("init", jDevId, jAppId);
	}
		
	public static bool initUserData(){
		bool result = false;
		int assigned = 0;
			
		TextAsset data = (TextAsset)Resources.Load("StartAppData");
		string userData = data.ToString();
			
		string[] splitData = userData.Split('\n');
		string[] singleData;
			
		for (int i = 0; i < splitData.Length; i++){
			singleData = splitData[i].Split('=');
			if (singleData[0].ToLower().CompareTo("applicationid") == 0){
				assigned++;
				applicatonId = singleData[1].ToString().Trim();
			}
				
			if (singleData[0].ToLower().CompareTo("developerid") == 0){
				assigned++;
				developerId = singleData[1].ToString().Trim();
			}
		}
			
		if (assigned == 2){
			result = true;
		}
		return result;

	}
		#endif
}

}