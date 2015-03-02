using UnityEngine;
using System.Collections;

public class UI_FeverTime : MonoBehaviour {
	public delegate void feverTimeHandler();
    public static event feverTimeHandler  _feverTimeTimingEnd;
	GameObject FeverTimeRainbowSprite;
	GameObject FeverTimeStarSprite;
	GameObject[] sideWall = new GameObject[2];
	UISpriteAnimation FeverTimeStarSprite2UISpriteAnimation;
	public static int FeverTimeCountSecond{set; get; }//ScoreManager.csで保存
	
	bool feverTimeMode = false;
	
	void Start (){
		getComponentFeverTimeSprite();
 		sideWall[0] = GameObject.Find("Left");
		sideWall[1] = GameObject.Find("Right");
		
		sideWall[0].transform.setPositionX(5f); 
		sideWall[1].transform.setPositionX(-5f); 
		//feverTime();
	}
	
	void loadDataFromScoreManagerScript(){
		if(ScoreManager.feverTimeFlag){
			rainbowAnimation(0, 2.0f, true);
			sideWall[0].transform.setPositionX(-1f); 
			sideWall[1].transform.setPositionX(-1f); 
		}
	}
	
	
	void getComponentFeverTimeSprite(){
		if(FeverTimeRainbowSprite == null){
			FeverTimeRainbowSprite = GameObject.Find("FeverTimeRainbowSprite");
		}
		if(FeverTimeStarSprite == null){
			FeverTimeStarSprite = GameObject.Find("FeverTimeStarSprite");
		}
		if(FeverTimeStarSprite2UISpriteAnimation == null){
			FeverTimeStarSprite2UISpriteAnimation = GameObject.Find("FeverTimeStarSprite2").GetComponent<UISpriteAnimation>();
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
		FeverTimeStarSprite2UISpriteAnimation.enabled = true;
	}
		
	void sideWallAnimation(){
	//	yield return new WaitForSeconds(2.5f);
		iTween.MoveTo( sideWall[1], 
				iTween.Hash("x", -1f,
									"time", 1, 								
									"easeType", "linear", 
									"delay", 0));
		iTween.MoveTo( sideWall[0], 
				iTween.Hash("x", -1f,
									"time", 1, 
									"easeType", "linear", 
									"delay", 0));
	}
	
	void setWallAtOriginalPos(){
		iTween.MoveTo(sideWall[1],
				iTween.Hash("x", sideWall[1].transform.localPosition.x -7f, 
									"time", 1, 
									//"oncomplete","feverTimeIsEnd",
									//"oncompletetarget", gameObject,
									"easeType", "linear", 
									"delay", 0));
		
		iTween.MoveTo(sideWall[0],
				iTween.Hash("x", sideWall[0].transform.localPosition.x +7f, 
									"time", 1, 
									//"oncomplete","feverTimeIsEnd",
									//"oncompletetarget", gameObject,
									"easeType", "linear", 
									"delay", 0));
	}
	
	void feverTimeEndEvent(){
		_feverTimeTimingEnd(); //Delegate
		rainbowAnimation(1, -1.0f, false);
		setWallAtOriginalPos();
	}
	
	void rainbowAnimation(int time, float speed, bool enabled){
		feverTimeMode = false;
		FeverTimeRainbowSprite.animation["FeverTimeRainbow"].time = time;
		FeverTimeRainbowSprite.animation["FeverTimeRainbow"].speed = speed;
		FeverTimeRainbowSprite.animation.Play("FeverTimeRainbow");
		FeverTimeStarSprite2UISpriteAnimation.enabled = enabled;
	}
	
	float timer = 0f;
	void Update (){
		if(ScoreManager.feverTimeFlag == true){
			timer += Time.deltaTime;
			if(timer > 1.0f){
				FeverTimeCountSecond +=1;
//				print ("FeverTimeCountSecond: " + FeverTimeCountSecond);
				
				if(FeverTimeCountSecond > 20){
					feverTimeEndEvent();
					FeverTimeCountSecond = 0;
				}
				
				timer = 0f;
			}
		}
	}
	
	
	
	#region Delegate
	void OnEnable(){
		ScoreManager._afterLoadGameData += loadDataFromScoreManagerScript;
		UI_GameScene._feverTimeEvent += feverTimeEventStart;
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
	}
	#endregion
}
