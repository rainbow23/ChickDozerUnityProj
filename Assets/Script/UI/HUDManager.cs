using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour {
	GameObject ChickScoreGrp;
	UISprite ChickScoreUISprite;
	GameObject chickPoint;
	GameObject LevelUpSprite;

	//GameObject GameControlManager;
	//GameControlManager GameControlManagerScript;
	int multiplyScore = 1;
	
	void Start (){
		//GameControlManager = GameObject.Find("GameControlManager");
		//GameControlManagerScript = GetComponent<GameControlManager>();
		LevelUpSprite = GameObject.Find("LevelUpSprite");		
	}
	
	//[HideInInspector]
	public GameObject objHudPointDisplay;
	
	void displayChickScore(int receiveChickScore, Transform receiveHitPos){
			//Instantiate HUD obj
			/*
			Vector3 posHudPoint = new Vector3(posChick.transform.position.x, -1f, 6f);
			objHudPointDisplay = Instantiate(Resources.Load("HUD/ChickScoreHUD"), posHudPoint, Quaternion.identity) as GameObject;	  //HUD X position: バスケットの位置 x
			objHudPointDisplay.name = Resources.Load("HUD/ChickScoreHUD").name;
			objHudPointDisplay.transform.rotation = Quaternion.Euler(0, 180, 0);
			objHudPointDisplay.transform.parent = GameObject.Find("GameObject").transform;
			
			iTween.MoveTo(objHudPointDisplay, iTween.Hash("position", new Vector3(posHudPoint.x, posHudPoint.y + 1f, posHudPoint.z), "time", 1.0f, "oncomplete", "destroyObj")); //animation
			*/
	}
	
	IEnumerator deleteTime(){
		yield return new WaitForSeconds(1f);
		ChickScoreGrp.SetActive (false);
	}
	
	void levelUp(int levelCounts){
		LevelUpSprite.animation["LevelUp_"].speed = 0.5f;
		LevelUpSprite.animation.Play("LevelUp_");
	}
	
	void Update (){
		
	}
	void OnEnable(){
		BasketManager._hitChickBasket2 += displayChickScore;
		//GameControlManager._levelUpTiming += levelUp;
	}
	void OnDisable(){
		UnSubscribeEvent();
	}
	void OnDestroy(){
		UnSubscribeEvent();
	}
	void UnSubscribeEvent(){		
		BasketManager._hitChickBasket2 -= displayChickScore;
		//GameControlManager._levelUpTiming -= levelUp;
	}
}
