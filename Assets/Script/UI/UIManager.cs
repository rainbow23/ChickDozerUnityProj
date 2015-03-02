using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {
	
	/*
	float defaultWidth = 640f;
	float defaultHeight = 960f;
	
	float screenWidth;
	float screenHeight;
	
	float screenWidthAspect =0f;
	float screenHeightAspect = 0f;
	
	float changeHeight = 0f;
	float changeWidth = 0f;
	
	void Awake ()
	{
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		
		
		#if UNITY_EDITOR
		//UnityViewtest;
		screenWidth = 640f;
		screenHeight = 960f;
		
		//screenWidth = 640f;
		//screenHeight = 1136f;
		
		//screenWidth = 320f;
		//screenHeight = 480f;
		
		//screenWidth = 480f;
		//screenHeight = 320f;
		
		//screenWidth = 480f;
		//screenHeight = 800f;
		
		//screenWidth = 480f;
		//screenHeight = 854f;
		//print (screenHeight);
		#endif
		
		screenWidthAspect = screenWidth/defaultWidth;
		screenHeightAspect = screenHeight/defaultHeight;
		// 640:960
		//print (screenWidthAspect);//1
		//print (screenHeightAspect);//1
		
		// 9/11 縦と横の比を比べる
			// 9/11 もし縦の比の方が大きかったら画像を縦の比で拡大する
			if(screenHeightAspect > screenWidthAspect )
			{
				//print ("screenHeightAspect is Bigger than screenWidthAspect");
				if(screenHeightAspect > 1)
				{
					// 9/11 1.01fは画像が足りないかもしれないので余分に掛ける
					changeHeight =defaultHeight * screenHeightAspect * 1.01f;
					changeWidth = defaultWidth * screenHeightAspect * 1.01f;
					print (screenHeightAspect);
					
					//print (defaultHeight);
					//print (screenHeightAspect);
					//print (changeHeight);
					//print (changeWidth);
				
					// 9/11spriteの大きさを変える
					GameObject BG = GameObject.Find("BG");
					BG.transform.localScale = new Vector3(changeWidth, changeHeight, transform.localScale.z);
				}
			
				if(screenHeightAspect < 1)
				{
					return;
				}
			}
		
		
			else if((screenHeightAspect < screenWidthAspect ))
			{
				print ("screenWidthAspect is Bigger than screenHeightAspect");
				if(screenWidthAspect  > 1)
				{
					// 9/11 もし横の比の方が大きかったら画像を横の比で拡大する
					changeHeight = defaultHeight * screenWidthAspect * 1.01f;
					changeWidth = defaultWidth * screenWidthAspect* 1.01f;
					print (screenWidthAspect);
					// 9/11spriteの大きさを変える
					GameObject BG = GameObject.Find("BG");
					BG.transform.localScale = new Vector3(changeWidth, changeHeight, transform.localScale.z);
				}
			
				if(screenWidthAspect  < 1)
				{
					return;
				}
			}
		
			if(screenHeightAspect == screenWidthAspect)
			{
			//print ("screenHeighAspect and screenWidth is same aspect");
				return;
			}
	}
	
	void Update () 
	{
	
	}
	*/
	
	UISprite newChickSignBoardObjSprite;
	
	void Start(){
		newChickSignBoardObjSprite = GameObject.Find("newChickSignBoard").GetComponent<UISprite>();
		newChickSignBoardObjSprite.enabled = false;
	}
	
	void basketGetNewChickEvent(){
		newChickSignBoardObjSprite.enabled = true;
		newChickSignBoardObjSprite.animation.Play();
	}
	
	void newChickSignBoardHide(){
		newChickSignBoardObjSprite.enabled = false;
	}
	
	void Update(){
		if(newChickSignBoardObjSprite.animation.isPlaying ==false){
			newChickSignBoardObjSprite.enabled = false;
		}
	}
	
	void OnEnable(){
		//GameControlManager.GameData._BasketHitNewChick += basketGetNewChickEvent;
		//GameControlManager._BasketHitNewChick -= basketGetNewChickEvent;
	}
	void OnDisable(){
		//GameControlManager.GameData._BasketHitNewChick -= basketGetNewChickEvent;
	}
}
