using System.Collections;
using UnityEngine;
enum BtnType{osusumeBtn = 1, osusumeIcon = 0};

[RequireComponent(typeof(PlugInObject))]
public class AdManager: SingletonMonoBehaviour<AdManager>
{
	PlugInObject PlugInObjectScript;

	#if UNITY_EDITOR || UNITY_STANDALONE_OSX
	#elif UNITY_ANDROID

	AndroidJavaObject GoodiaPlugin;
	#endif
	
	void Awake()
	{
		if (this != Instance) {
			Destroy (this.gameObject);
			return;
		}
		DontDestroyOnLoad (this.gameObject);

		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		#elif UNITY_ANDROID
		GoodiaPlugin = new AndroidJavaObject("jp.co.goodia.NinjaPingPong.GoodiaPlugin");
		Debug.Log("Call Goodia Plugin");
		#endif
	}


	
	void Start(){
		PlugInObjectScript = GetComponent<PlugInObject>();

		//init iOS only
		PlugInObjectScript.Init();
		PlugInObjectScript.AdstirInit();
		PlugInObjectScript.webViewGoodiaInit();	
		PlugInObjectScript.asutarisukuLoad();

		//iOS only
		PlugInObjectScript.showGoddiaHedderAd(); 
		showAppliPromotionPreload(false);
		PlugInObjectScript.AdstirSetVisibility(true);	
		
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		#elif UNITY_ANDROID
		GoodiaPlugin.Call("genuineAdPreparation");
		showIconAd(0, true);
		#endif
	}

	//iOS _needToDisplayIconAdを調べた後、iOSから実行命令される Startで書くと先に実行されてしまう対策
	void showIconAdInStart(string level)
	{
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		#elif UNITY_IPHONE
		showIconAd(int.Parse(level), true);
		#endif
	}
	
	void OnLevelWasLoaded(int level)
	{
		if(level == 0)//title
		{
			if (this != Instance) return;
			//iOS Android
			showIconAd(level, true);
				
			//iOS only
			showGoodiaHeaderAd(true);
			showAppliPromotionPreload(false);
			//Android	
			showGenuineAdInAndroid(false);
		}
		//game
		if(level == 1)
		{
			if (this != Instance) return;
			//iOS Android
			showIconAd(level, true);
			//iOS only
			showGoodiaHeaderAd(false);
			PlugInObjectScript.reportGameStartToFlurry();
			//Android	
			showGenuineAdInAndroid(false);
		}
		//Ending
		if(level == 2)
		{
			if (this != Instance) return;
			//iOS Android
			showIconAd(level, true);
			//iOS only
			//クリア画面に行ったらハイスコアのタイミングでreviewPopUp関数を呼ぶ
			if(PlayerPrefs.GetFloat("finalScore", 0) > PlayerPrefs.GetFloat ("bestTime", 0))
			{
				showReviewPopUp();
			}
			sendScoreToLeaderBoard();
			showGoodiaHeaderAd(true);
			showSplashAd();			
			//Android	
			showGenuineAdInAndroid(true);
		}
		
		else
		{
			//Android	
			showGenuineAdInAndroid(false);
		}
	}
	
	void OnApplicationPause(bool paused){
		if(paused == false){
			#if UNITY_EDITOR
			//Debug.Log("ShowGoodAd");
			#elif UNITY_IPHONE
			//showGoodAd();
			#endif
		}
	}
	
	void Update()
	{
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		#elif UNITY_ANDROID
		if (Input.GetKeyDown(KeyCode.Escape)) GoodiaPlugin.Call("showExitAd");
		#endif
	}

	public void ShowTwitterWithBestScore()
	{
		int bestScore = (int)(PlayerPrefs.GetFloat ("bestTime", 0));		
		PlugInObjectScript.SendTweetBestScore(bestScore);
	}
	
	public void ShowTwitterWithScore()
	{
		int currentScore = (int)(PlayerPrefs.GetFloat("finalScore", 0));
		PlugInObjectScript.SendTweetScore(currentScore);
	}
	
	/// <summary>
	/// iOS GameCenter
	/// </summary>
	public void showGameCenter()
	{
		//load PlugInObjectScript.sendBestScore
		PlugInObjectScript.showLeaderBoard();
	}
	
	public void sendScoreToLeaderBoard()
	{
		int currentScore = (int)(PlayerPrefs.GetFloat ("finalScore", 0));
		PlugInObjectScript.rankingReportScore(currentScore);
	}
	
	//Android iOS
	private void showIconAd(int scene, bool enable)
	{
		PlugInObjectScript.showIconAd(scene, enable);		
	}
	
	private void showGenuineAdInAndroid(bool visible)
	{
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		#elif UNITY_IPHONE
		#elif UNITY_ANDROID
		GoodiaPlugin.Call("showGenuineAd", visible);
		#endif
	}

	public void showGameFeat(){
		//Beadをかぶせる Native
		PlugInObjectScript.ShowGameFeat();
	}
	
	public void showWallAd(int scene, int type){
		PlugInObjectScript.ShowWallAd(scene, type);
	}
	
	public void showAppliPromotionPreload(bool visibility){
		PlugInObjectScript.ShowAppliPromotionPreload(visibility);
	}
	
	public void showGoodiaWeb(){
		PlugInObjectScript.SetVisibility(true);
	}
	
	//Interstatial
	public void ShowiMobileInterstatial(){
		PlugInObjectScript.ShowiMobileInterstatial();
	}
	
	public void show_mMediaInterstatial(){
		PlugInObjectScript.Show_mMediaInterstatial();
	}
	
	public void showGoodAd(){
		PlugInObjectScript.ShowGoodAd(true);
	}
	
	public void showGoodiaHeaderAd(bool visibility){
		PlugInObjectScript.webViewGoodiaAd_SetVisibility(visibility);
	}
	
	public void showReviewPopUp(){
		PlugInObjectScript.showReviewPopUp();
	}
	
	public void showSplashAd(){
		PlugInObjectScript.ShowSplashAd();
	}

	public void pushPauseButton()
	{
		PlugInObjectScript.pushPauseButton();
	}

	public void pushToTitleBtnInPause()
	{
		PlugInObjectScript.pushToTitleBtnInPause();
	}
	public void pushReturnGameBtnInPause()
	{
		PlugInObjectScript.pushReturnGameBtnInPause();
	}



	public void pushQuestionButtonTitle()
	{
		PlugInObjectScript.pushQuestionButtonTitle();

	}

	public void pushHowtoButtonTitle()
	{
		PlugInObjectScript.pushHowtoButtonTitle();
	}



}		
