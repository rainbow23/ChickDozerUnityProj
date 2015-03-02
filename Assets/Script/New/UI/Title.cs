using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {

	WebViewObject webViewObjectScript;
	
	void Awake(){
		if(GameObject.Find("webViewGameObject")  == true){
				webViewObjectScript = GameObject.Find("webViewGameObject").GetComponent<WebViewObject>();
		}
		#if UNITY_EDITOR
		
		#elif UNITY_IPHONE
		
		#endif

	}
	
	void Start () {
	
	}
	
	void tweetBestScore(){
		webViewObjectScript.SendTweetBestScore(PlayerPrefs.GetInt("maxStageClear",0));
	}
	
	void gameStart(){
		FadeManager.Instance.LoadLevel("StageCastle2", 0.5f);
		//toGameSceneSwitch = true;
	}
	void osusumeGameIcon(){
		if(GameObject.Find("webViewGameObject") == true){
			webViewObjectScript.ShowGameFeat();
		}
	}
	void osusumeGameButton(){
		if(GameObject.Find("webViewGameObject") == true){
			webViewObjectScript.ShowAppliPromotion();
		}
	}

	void sekaiRanking(){
		if(GameObject.Find("webViewGameObject") == true){
			webViewObjectScript.showLeaderBoard();
			//WebViewObjectScript.ShowAddRanking();
		}
	}
	void goodiaWeb(){
		webViewObjectScript.SetVisibility(true);
		//webViewObjectScript.SetVisibility(true);
	}
	
	void Update () {
	
	}
}
