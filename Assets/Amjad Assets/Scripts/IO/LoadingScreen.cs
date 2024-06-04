using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
	public Texture2D texture;
	static LoadingScreen instance;
	
	void Awake()
	{
		if (instance)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;
		gameObject.AddComponent<GUITexture>().enabled = false;
		guiTexture.texture = texture;
		transform.position = new Vector3(0.5f, 0.5f, 0.0f);
		DontDestroyOnLoad(this);
	}
	
//	public static void Load(int index)
//	{
//		Debug.Log (NoInstance ());
//		if (NoInstance ()) {
//						return;
//				}
//		instance.guiTexture.enabled = true;
//		Application.LoadLevel(index);
//		instance.guiTexture.enabled = false;
//	}
	
	public  void Load(string name)
	{
		Debug.Log (NoInstance ());
		if (!NoInstance()) return;
		instance.guiTexture.enabled = true;
		Application.LoadLevel(name);
		instance.guiTexture.enabled = false;
	}
	
	static bool NoInstance()
	{
		if (!instance)
			Debug.LogError("Loading Screen is not existing in scene.");
		return instance;
	}
}