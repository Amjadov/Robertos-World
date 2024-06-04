using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReScaleGUI: MonoBehaviour {
	
	
	
	private float pxX;
	private float pxY;
	private float pxWidth;
	private float pxHeight;
	private UnityEngine.UI.Image guiTex;
	public float SpaceFromRight = 0f;
	public float SpaceFromTop = 0f;
	private float sWidth;
	private float sHeight;
	private float guiWRatio;
	private float guiHRatio;
	private static float DesignedScaleWidth = 800;
	private static float DesignedScaleHeight = 480;

	void Start ()
	{
		
		// Move to right side of screen
		guiTex = GetComponent<UnityEngine.UI.Image> ();
		getRatio ();
		pxWidth = guiTex.pixelInset.width * guiWRatio;
		pxHeight = guiTex.pixelInset.height * guiHRatio;
		
		if (SpaceFromRight == 0)
			SpaceFromRight = guiTex.pixelInset.x;
		
		if (SpaceFromTop == 0)
			SpaceFromTop = guiTex.pixelInset.y;
		
		
		
		pxX = (SpaceFromRight * guiWRatio);
		pxY = (SpaceFromTop * guiHRatio);

		if (guiTex.pixelInset.width == guiTex.pixelInset.height) { 
			if (pxWidth > pxHeight)
			{pxHeight = pxWidth;}
			else {pxWidth = pxHeight;}

				}
		
		guiTex.pixelInset = new Rect (pxX,  pxY, pxWidth, pxHeight);
	}
	
	void getRatio ()
	{
		
		sWidth = Screen.width;
		
		//Get the screen's width
		
		sHeight = Screen.height;
		
		//Calculate the scale ratio. Divide the current screen resolution by the resolution you originally designed the UI on.
		
		guiWRatio = sWidth / DesignedScaleWidth;
		
		guiHRatio = sHeight / DesignedScaleHeight;
		
		
		
	}
	
}