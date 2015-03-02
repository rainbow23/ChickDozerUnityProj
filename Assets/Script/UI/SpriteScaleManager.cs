using UnityEngine;
using System.Collections;

public class SpriteScaleManager : MonoBehaviour {

	UISprite thisSprite;
	public bool FitToScreenX;
	public bool FitToScreenY;

	private Vector2 thisSpriteAspect;
	private Vector2 thisSpriteScaleValue;
	private static  Vector2 screenSize;
	private int margin = 5;

	void OnEnable()
	{
		UIRootManager._setPosition += fitSpriteScaleToScreen;
	}
	
	void Awake () 
	{
		thisSprite = this.GetComponent<UISprite>();
		thisSpriteAspect = new Vector2( 
											(float)(thisSprite.width /(float)thisSprite.height), 
		                               		(float)(thisSprite.height /(float)thisSprite.width)
		                               	);

		//print ("thisSpriteAspect: " + thisSpriteAspect);
											
		thisSpriteScaleValue  = new Vector2(thisSprite.width, thisSprite.height);
	}

	void fitSpriteScaleToScreen()
	{

		screenSize = new Vector2(UIRootManager.panelWidth + margin, UIRootManager.panelHeight + margin);

		if(FitToScreenY && FitToScreenX) 
		{
			thisSpriteScaleValue = screenSize;
		}
		else if(FitToScreenX && !FitToScreenY)
		{
			if(thisSprite.width !=  UIRootManager.panelWidth)
				thisSpriteScaleValue = new Vector2(screenSize.x, screenSize.x*thisSpriteAspect.y);
		}
		else if(FitToScreenY && !FitToScreenX)
		{
			if(thisSprite.height != UIRootManager.panelHeight)
				thisSpriteScaleValue = new Vector2(screenSize.y*thisSpriteAspect.x, screenSize.y);
		
		}
		else return;

		thisSprite.SetDimensions((int)thisSpriteScaleValue.x, (int)thisSpriteScaleValue.y);
	}

	#region Delegate
	
	void Disable()
	{
		UnsubscribeEvent();
	}
	void OnDestroy(){
		UnsubscribeEvent();
	}
	void UnsubscribeEvent(){
		UIRootManager._setPosition -= fitSpriteScaleToScreen;
	}
	#endregion
}
