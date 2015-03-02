using UnityEngine;
using System.Collections;

public class UI_FeverTime : MonoBehaviour {
	public delegate void feverTimeHandler();
    public static event feverTimeHandler  _feverTimeTimingEnd;
	GameObject FeverTimeRainbowSprite;
	GameObject FeverTimeStarSprite;
	GameObject[] sideWall = new GameObject[2];
	UISpriteAnimation[] FeverTimeStarSprite2UISpriteAnimation = new UISpriteAnimation[6];
	public static float FeverTimeCountSecond{set; get; }//ScoreManager.csで保存
	
	bool feverTimeMode = false;
	
	void Start (){
		getComponentFeverTimeSprite();
 		sideWall[0] = GameObject.Find("Left");
		sideWall[1] = GameObject.Find("Right");
		
		sideWall[0].transform.setPositionX(9.5f); 
		sideWall[1].transform.setPositionX(-18f); 
		//feverTime();
	}
	
	void loadDataFromScoreManagerScript(){
		if(ScoreManager.feverTimeFlag){
			rainbowAnimation(0, 2.0f, true);
			if(sideWall[0] == null){GameObject.Find("Left");}
			if(sideWall[1] == null){GameObject.Find("Right");}
			sideWall[0].transform.setPositionX(0.78f); 
			sideWall[1].transform.setPositionX(-9.1f); 
		}
	}
	
	
	void getComponentFeverTimeSprite(){
		if(FeverTimeRainbowSprite == null){
			FeverTimeRainbowSprite = GameObject.Find("FeverTimeRainbowSprite");
		}
		if(FeverTimeStarSprite == null){
			FeverTimeStarSprite = GameObject.Find("FeverTimeStarSprite");
		}
		
		for(int i = 0; i < 6; i++){
			if(FeverTimeStarSprite2UISpriteAnimation[i] == null){
				FeverTimeStarSprite2UISpriteAnimation[i] = GameObject.Find("FeverTimeStarSprite" + i.ToString()).GetComponent<UISpriteAnimation>();	
			}
		}
	}
	
	void feverTimeEventStart(){
		feverTimeMode = true;
		//Animation Start
		
		feverTimeAnimationIsStart();
		sideWallAnimation();
	}
	
	void feverTimeAnimationIsStart(){
		//animation
		getComponentFeverTimeSprite();
		
		rainbowAnimation(0, 1.0f, true);
		FeverTimeStarSprite.animation["FeverTimeStar"].wrapMode =WrapMode.Once;
		FeverTimeStarSprite.animation.Play("FeverTimeStar");
		//FeverTimeStarSprite2UISpriteAnimation.enabled = true;
		
		for(int i = 0; i < 6; i++){
			FeverTimeStarSprite2UISpriteAnimation[i].enabled = true;
		}
	}
	
	float time = 2f;
	void sideWallAnimation(){
	//	yield return new WaitForSeconds(2.5f);
		iTween.MoveTo( sideWall[1], 
				iTween.Hash("x", -9.1f,
									"islocal", true,
									"time", time, 								
									"easeType", "linear", 
									"delay", 0));
		iTween.MoveTo( sideWall[0], 
				iTween.Hash("x", 0.78f,
									"islocal", true,
									"time", time, 
									"easeType", "linear", 
									"delay", 0));
	}

	void setWallAtOriginalPos(){
		iTween.MoveTo(sideWall[1],
				iTween.Hash("x", -18f,
									"islocal", true,
									"time", time, 
									"easeType", "linear", 
									"delay", 0));
		
		iTween.MoveTo(sideWall[0],
				iTween.Hash("x", 9.5f,
									"islocal", true,
									"time", time, 									
									"easeType", "linear", 
									"delay", 0));
	}
	
	void feverTimeEndEvent(){
		_feverTimeTimingEnd(); //Delegate
		rainbowAnimation(1, -1.0f, false);
		setWallAtOriginalPos();
	}
	
	void rainbowAnimation(int time, float speed, bool enabled){
		if(FeverTimeRainbowSprite == null){
			FeverTimeRainbowSprite = GameObject.Find("FeverTimeRainbowSprite");
		}
		
		feverTimeMode = false;
		FeverTimeRainbowSprite.animation["FeverTimeRainbow"].time = time;
		FeverTimeRainbowSprite.animation["FeverTimeRainbow"].speed = speed;
		FeverTimeRainbowSprite.animation.Play("FeverTimeRainbow");
		//FeverTimeStarSprite2UISpriteAnimation.enabled = enabled;
		for(int i = 0; i < 6; i++){
			FeverTimeStarSprite2UISpriteAnimation[i].enabled = enabled;
		}
	}
	
	
	bool feverTimerActive = false; 
	//HowToが隠れたら呼ばれる, call it scene transition from Collection 
	
	/*
	void switchFeverTimerActive(){
		feverTimerActive = true;
	}
	*/
	
	float timer = 0f;
	void Update (){
		//HowToが出ていないときはFeverTimerが進む
		if(ScoreManager.feverTimeFlag == true ){
			timer += Time.deltaTime;
			if(timer > 1.0f){
				FeverTimeCountSecond +=1f;
				print ("FeverTimeCountSecond: " + FeverTimeCountSecond);
//				print ("FeverTimeCountSecond: " + FeverTimeCountSecond);
				
				if(FeverTimeCountSecond > 20f){
					feverTimeEndEvent();
					FeverTimeCountSecond = 0f;
				}
				
				timer = 0f;
			}
		}
	}
	
	
	
	#region Delegate
	void OnEnable(){
		ScoreManager._afterLoadGameData += loadDataFromScoreManagerScript;
		UI_GameScene._feverTimeEvent += feverTimeEventStart;
		//SoundManager._isMoveTimer += switchFeverTimerActive;
		//UI_GameScene._isMoveChick += switchFeverTimerActive;
	}
	void OnDisable(){
		UnsubscribeEvent();
	}
	void OnDestroy(){
		UnsubscribeEvent();
	}
	
	void UnsubscribeEvent(){
		ScoreManager._afterLoadGameData -= loadDataFromScoreManagerScript;
		UI_GameScene._feverTimeEvent -= feverTimeEventStart;
		//SoundManager._isMoveTimer -= switchFeverTimerActive;
		//UI_GameScene._isMoveChick -= switchFeverTimerActive;
	}
	#endregion
}
