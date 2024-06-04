using UnityEngine;
using System.Collections;

public class DontShowAdverts : MonoBehaviour {
	public static DontShowAdverts current;
	public bool DontShowAdds = false;

	void Awake()
	{
		current = this;
	}
	public void RemoveAds()
	{
		DontShowAdds = true;
	}
}
