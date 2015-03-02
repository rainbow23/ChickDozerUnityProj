using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UISprite))]
public class SpritePositionManager : MonoBehaviour {

	public Vector2 iPhone4s;
	public Vector2 iPhone5;
	public Vector2 iPad;

	private float posZ;

	public float hoge;

	void OnEnable()
	{
		UIRootManager._setPosition +=setSpritePos;
	}

	void Awake()
	{
		posZ = this.transform.localPosition.z;
	}

	void setSpritePos () 
	{
		if(UIRootManager.Aspect >= (int)ScreenAspect.iPad )
		{
			transform.setLocalPosition(iPad.x, iPad.y, posZ);	
		}
		else if(UIRootManager.Aspect >= (int)ScreenAspect.inch4 ) //4s
		{
			transform.setLocalPosition(iPhone4s.x, iPhone4s.y, posZ);	
		}
		else //5
		{
			transform.setLocalPosition(iPhone5.x, iPhone5.y, posZ);	
		}
	}


	void Disable()
	{
		UnsubscribeEvent();
	}
	void OnDestroy(){
		UnsubscribeEvent();
	}
	void UnsubscribeEvent(){
		UIRootManager._setPosition -=setSpritePos;
	}
}
