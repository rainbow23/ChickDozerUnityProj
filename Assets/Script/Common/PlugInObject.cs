/*
 * Copyright (C) 2011 Keijiro Takahashi
 * Copyright (C) 2012 GREE, Inc.
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty.  In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would be
 *    appreciated but is not required.
 * 2. Altered source versions must be plainly marked as such, and must not be
 *    misrepresented as being the original software.
 * 3. This notice may not be removed or altered from any source distribution.
 */

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Callback = System.Action<string>;

#if UNITY_EDITOR || UNITY_STANDALONE_OSX
public class UnitySendMessageDispatcher
{
	public static void Dispatch(string name, string method, string message)
	{
		GameObject obj = GameObject.Find(name);
		if (obj != null)
			obj.SendMessage(method, message);
	}
}
#endif


public class PlugInObject : SingletonMonoBehaviour<PlugInObject>
{

	public void Awake (){
		//	DontDestroyOnLoad (this.gameObject);
	}
	
	Callback callback;
	#if UNITY_EDITOR || UNITY_STANDALONE_OSX
	//IntPtr webView;
	
	////////////////////////////////////////////////////////////////////////////////////////////////////
	
	bool adstirVisibility;
	//IntPtr asutarisuku;
	////////////////////////////////////////////////////////////////////////////////////////////////////	
	
	bool visibility;
	//Rect rect;
	Texture2D texture;
	string inputString;
	
	#elif UNITY_IPHONE
	IntPtr webView;
	////////////////////////////////////////////////////////////////////////////////////////////////////
	IntPtr adstirView;
	IntPtr webViewGoodiaAd;
	IntPtr asutarisuku;
	////////////////////////////////////////////////////////////////////////////////////////////////////
	#elif UNITY_ANDROID
	AndroidJavaObject GoodiaPlugin;
	#endif
	
	#if UNITY_EDITOR || UNITY_STANDALONE_OSX
	[DllImport("WebView")]
	private static extern IntPtr _WebViewPlugin_Init(
		string gameObject, int width, int height, bool ineditor);

	[DllImport("WebView")]
	private static extern void _WebViewPlugin_SetRect(
		IntPtr instance, int width, int height);
	[DllImport("WebView")]
	private static extern void _WebViewPlugin_SetVisibility(
		IntPtr instance, bool visibility);
	[DllImport("WebView")]
	private static extern void _WebViewPlugin_LoadURL(
		IntPtr instance, string url);
	[DllImport("WebView")]
	private static extern void _WebViewPlugin_EvaluateJS(
		IntPtr instance, string url);
	[DllImport("WebView")]
	private static extern void _WebViewPlugin_Update(IntPtr instance,
	                                                 int x, int y, float deltaY, bool down, bool press, bool release,
	                                                 bool keyPress, short keyCode, string keyChars, int textureId);
	////////////////////////////////////////////////////////////////////////////////////////////////////
	[DllImport("WebView")]
	public static extern IntPtr _adstirPlugin_Init(string gameObject);
	[DllImport("WebView")]
	public static extern void _adstirPlugin_SetVisibility (IntPtr instance, bool setVisibility);
	[DllImport("WebView")]
	public static extern void _webViewGoodiaAdSetVisibility (IntPtr instance,bool setVisibility);
	[DllImport("WebView")]
	public static extern IntPtr _webViewGoodiaInit(string gameObject);
	
	[DllImport("WebView")]
	private static extern  IntPtr _AsutarisukuLoad(string gameObject);
	[DllImport("WebView")]
	private static extern void  _ShowIconAd(IntPtr instance,int scene, bool enable);
	
	
	////////////////////////////////////////////////////////////////////////////////////////////////////
	[DllImport("WebView")]
	private static extern void  _ShowLeaderBoard (IntPtr instance);
	[DllImport("WebView")]
	private static extern void  _SendTweetBestScore (IntPtr instance, int bestScore);
	[DllImport("WebView")]
	private static extern void  _SendTweetScore (IntPtr instance, int score);
	[DllImport("WebView")] 
	private static extern void  _ShowReviewPopUp (IntPtr instance);
	[DllImport("WebView")]
	private static extern void  _RankingReportScore (IntPtr instance, int currentScore);	
	
	//[DllImport("WebView")]
	//private static extern void  _ShowAppliPromotion (IntPtr instance);
	
	[DllImport("WebView")]
	private static extern void  _ShowGameFeat (IntPtr instance);
	
	[DllImport("WebView")]
	private static extern void  _ShowAddRanking (IntPtr instance);
	
	[DllImport("WebView")]
	private static extern void  _ReportGameStartToFlurry (IntPtr instance);
	
	[DllImport("WebView")]
	private static extern void   _ShowIMobileInterstatial(IntPtr instance);
	
	[DllImport("WebView")]
	private static extern void   _Show_mMediaInterstatial(IntPtr instance);
	
	[DllImport("WebView")]
	private static extern void   _ShowGoodAd(IntPtr instance, bool visibility);
	
	[DllImport("WebView")]
	private static extern void   _ShowAppliPromotionPreload(IntPtr instance, bool visibility);
	
	[DllImport("WebView")]
	private static extern void   _ShowSplashAd(IntPtr instance);

	[DllImport("WebView")]
	private static extern void   _PushPauseButton(IntPtr instance);

	[DllImport("WebView")]
	private static extern void   _PushToTitleBtnInPause(IntPtr instance);

	[DllImport("WebView")]
	private static extern void   _PushReturnGameBtnInPause(IntPtr instance);

	[DllImport("WebView")]
	private static extern void   _PushQuestionButtonTitle(IntPtr instance);

	[DllImport("WebView")]
	private static extern void   _PushHowtoButtonTitle(IntPtr instance);
	
	////////////////////////////////////////////////////////////////////////////////////////////////////
	#elif UNITY_IPHONE
	[DllImport("__Internal")]
	private static extern IntPtr _WebViewPlugin_Init(string gameObject);

	[DllImport("__Internal")]
	private static extern void _WebViewPlugin_SetMargins(
		IntPtr instance, int left, int top, int right, int bottom);
	[DllImport("__Internal")]
	private static extern void _WebViewPlugin_SetVisibility(
		IntPtr instance, bool visibility);
	[DllImport("__Internal")]
	private static extern void _WebViewPlugin_LoadURL(
		IntPtr instance, string url);
	[DllImport("__Internal")]
	private static extern void _WebViewPlugin_EvaluateJS(
		IntPtr instance, string url);
	////////////////////////////////////////////////////////////////////////////////////////////////////
	
	[DllImport("__Internal")]
	public static extern IntPtr _adstirPlugin_Init(string gameObject);
	[DllImport("__Internal")]
	public static extern void _webViewGoodiaAdSetVisibility (IntPtr instance,bool setVisibility);
	[DllImport("__Internal")]
	public static extern void _adstirPlugin_SetVisibility (IntPtr instance,bool setVisibility);
	
	[DllImport("__Internal")]
	public static extern IntPtr _webViewGoodiaInit(string gameObject);
	
	[DllImport("__Internal")]
	private static extern  IntPtr _AsutarisukuLoad(string gameObject);
	
	////////////////////////////////////////////////////////////////////////////////////////////////////
	[DllImport("__Internal")]
	private static extern void  _ShowLeaderBoard (IntPtr instance);
	[DllImport("__Internal")]
	private static extern void  _SendTweetBestScore (IntPtr instance, int bestScore);
	[DllImport("__Internal")]
	private static extern void  _SendTweetScore (IntPtr instance, int Score);
	[DllImport("__Internal")] 
	private static extern void  _ShowReviewPopUp (IntPtr instance);
	[DllImport("__Internal")]
	private static extern void  _RankingReportScore (IntPtr instance, int currentScore);	

	[DllImport("__Internal")]
	private static extern void  _ShowWallAd (IntPtr instance, int scene, int type);
	
	[DllImport("__Internal")]
	private static extern void  _ShowGameFeat (IntPtr instance);
	
	[DllImport("__Internal")]
	private static extern void  _ShowAddRanking (IntPtr instance);
	
	[DllImport("__Internal")]
	private static extern void  _ReportGameStartToFlurry (IntPtr instance);
	
	[DllImport("__Internal")]
	private static extern void  _ShowIconAd(IntPtr instance,int scene, bool enable);
	
	[DllImport("__Internal")]
	private static extern void   _ShowIMobileInterstatial(IntPtr instance);
	
	[DllImport("__Internal")]
	private static extern void   _Show_mMediaInterstatial(IntPtr instance);
	
	[DllImport("__Internal")]
	private static extern void   _ShowGoodAd(IntPtr instance, bool visibility);
	
	[DllImport("__Internal")]
	private static extern void   _ShowAppliPromotionPreload(IntPtr instance, bool visibility);
	
	[DllImport("__Internal")]
	private static extern void   _ShowSplashAd(IntPtr instance);

	[DllImport("__Internal")]
	private static extern void _PushPauseButton(IntPtr instance);

	[DllImport("__Internal")]
	private static extern void   _PushToTitleBtnInPause(IntPtr instance);
	
	[DllImport("__Internal")]
	private static extern void   _PushReturnGameBtnInPause(IntPtr instance);

	[DllImport("__Internal")]
	private static extern void   _PushQuestionButtonTitle(IntPtr instance);
	
	[DllImport("__Internal")]
	private static extern void   _PushHowtoButtonTitle(IntPtr instance);
	////////////////////////////////////////////////////////////////////////////////////////////////////
	#endif	
	
	#if UNITY_EDITOR || UNITY_STANDALONE_OSX
	
	#endif


	
	public void showGoddiaHedderAd(){
		webViewGoodiaAd_SetVisibility(true);
	}
	public void hideGoddiaHedderAd(){
		webViewGoodiaAd_SetVisibility(false);
	}
	
	public void showAdStir(){
		AdstirSetVisibility(true);	
	}
	public void hideAdStir(){
		AdstirSetVisibility(false);	
	}
	
	
	public void Init(Callback cb = null)
	{
		callback = cb;
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		//		webView = _WebViewPlugin_Init(name, Screen.width, Screen.height,
		//			Application.platform == RuntimePlatform.OSXEditor);
		#elif UNITY_IPHONE
		webView = _WebViewPlugin_Init(name);
		#elif UNITY_ANDROID                                                                      
		GoodiaPlugin = new AndroidJavaObject("jp.co.goodia.NinjaPingPong.GoodiaPlugin");
		#endif
	}
	
	public void asutarisukuLoad(Callback cb = null)
	{
		callback = cb;
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		//	asutarisuku = _AsutarisukuLoad(name);
		#elif UNITY_IPHONE
		asutarisuku = _AsutarisukuLoad(name);
		#endif
	}
	
	public void webViewGoodiaInit(Callback cb = null){
		callback = cb;
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		//		webViewGoodiaAd = _webViewGoodiaInit(name);
		#elif UNITY_IPHONE
		webViewGoodiaAd = _webViewGoodiaInit(name);
		#endif
	}
	
	public  void webViewGoodiaAd_SetVisibility(bool setVisibility){
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		//print ("_webViewGoodiaAdSetVisibility: " + setVisibility);
		#elif UNITY_IPHONE
		if(webViewGoodiaAd == IntPtr.Zero){
			return;
		}
		else{
			_webViewGoodiaAdSetVisibility(webViewGoodiaAd, setVisibility);
		}
		#endif
	}
	
	
	public void AdstirInit(Callback cb = null){
		callback = cb;
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		//adstirView = _adstirPlugin_Init(name);
		#elif UNITY_IPHONE
		adstirView = _adstirPlugin_Init(name);
		#endif
	}

	//////////////////////////////////////////////////////////////////////////////////////////////
	public void ShowWallAd(int scene, int type){
		switch(scene)
		{
			case 1: scene = 0; break;
			case 0: scene = 1; break;
		}
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		print ("ShowWallAd");
		#elif UNITY_IPHONE
		if (asutarisuku == IntPtr.Zero)
		{
			return;
		}
		else{
			_ShowWallAd(asutarisuku, scene, type);	
		}
		#elif UNITY_ANDROID
		GoodiaPlugin.Call("showAppliPromotion");
		#endif			
	}
	
	public void ShowAppliPromotionPreload(bool visibility){
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		//print ("_ShowAppliPromotionPreload: " + visibility);
		#elif UNITY_IPHONE
		if (asutarisuku == IntPtr.Zero)
		{
			return;
		}
		else{
			_ShowAppliPromotionPreload(asutarisuku, visibility);	
		}
		#endif			
	}
	
	public void ShowGameFeat(){
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		print ("_ShowGameFeat");
		#elif UNITY_IPHONE
		if (asutarisuku == IntPtr.Zero)
		{
			return;
		}
		else{
			_ShowGameFeat(asutarisuku);
		}
		#endif			
	}

	
	public  void showIconAd(int scene, bool enable)
	{	
		switch(scene)
		{
		case 0:
			scene = 1;
			break;
		case 1:
			scene = 0;
			break;
		}
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
			//print ("_ShowIconAd: scene" + scene + "enable: " + enable);
		#elif UNITY_IPHONE
			if(asutarisuku == IntPtr.Zero){
				return;
			}
			else{
				_ShowIconAd(asutarisuku, scene, enable);
			}
		#elif UNITY_ANDROID
			GoodiaPlugin.Call("showIconAd", scene);
		#endif
	}
	
	public  void ShowGoodAd(bool setVisibility)
	{	
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		//print ("_ShowGoodAd");
		#elif UNITY_IPHONE
		if(asutarisuku == IntPtr.Zero){
			return;
		}
		else{
			_ShowGoodAd(asutarisuku, setVisibility);
		}
		#endif
	}
	
	public  void ShowiMobileInterstatial()
	{	
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		//print ("ShowiMobileInterstatial");
		#elif UNITY_IPHONE
		if(asutarisuku == IntPtr.Zero){
			return;
		}
		else{
			_ShowIMobileInterstatial(asutarisuku);
		}
		#endif
	}
	
	public  void Show_mMediaInterstatial()
	{	
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		//print ("Show_mMediaInterstatial");
		#elif UNITY_IPHONE
		if(asutarisuku == IntPtr.Zero){
			return;
		}
		else{
			_Show_mMediaInterstatial(asutarisuku);
		}
		#elif UNITY_ANDROID
		GoodiaPlugin.Call("showMMediaOptionalAd");
		#endif
	}
	
	public  void ShowSplashAd()
	{	
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		//print ("ShowSplashAd");
		#elif UNITY_IPHONE
		if(asutarisuku == IntPtr.Zero){
			return;
		}
		else{
			_ShowSplashAd(asutarisuku);
		}
		#elif UNITY_ANDROID
		GoodiaPlugin.Call("showOptionalAd");
		#endif
	}
	
	/// <summary>
	/// Show Splash Ad. Managed GAE
	/// </summary>
public void ShowAddRanking()
{
	#if UNITY_EDITOR || UNITY_STANDALONE_OSX
	print ("_ShowAddRanking");
	#elif UNITY_IPHONE
	if (asutarisuku == IntPtr.Zero){	
		print ("asutarisuku == IntPtr.Zero");
		return;
	}
	else
	{
		_ShowAddRanking(asutarisuku);
	}
	#endif			
}

public void SendTweetScore(int scores)
{
	#if UNITY_EDITOR || UNITY_STANDALONE_OSX
	print ("_SendTweetScore: "+ scores);
	#elif UNITY_IPHONE
	if (asutarisuku == IntPtr.Zero)
	{
		return;
	}
	else{
		_SendTweetScore(asutarisuku, scores);	
	}
	#elif UNITY_ANDROID
	// 0で今のスコア, 1でベストスコア
		string suffix = "iPhone/iPadゲーム「忍者ピンポン」http://itunes.apple.com/app/id%d #忍者ピンポン";
	GoodiaPlugin.Call("launchTwitter", 0, scores, suffix);
	#endif			
}
public void SendTweetBestScore(int bestScore)
{
	#if UNITY_EDITOR || UNITY_STANDALONE_OSX
	print ("_SendTweetBestScore: "+ bestScore);
	#elif UNITY_IPHONE
	if (asutarisuku == IntPtr.Zero)
	{
		return;
	}
	else{
		_SendTweetBestScore(asutarisuku, bestScore);	
	}
	
	#elif UNITY_ANDROID
	// 0で今のスコア, 1でベストスコア
		string suffix = "iPhone/iPadゲーム「忍者ピンポン」http://itunes.apple.com/app/id%d #忍者ピンポン";
	GoodiaPlugin.Call("launchTwitter", 1, bestScore, suffix);
	#endif			
}

/// <summary>
/// iOS GameCenter
/// </summary>
/// <param name='bestScore'>
/// Best score.
/// </param>
/// 
public void rankingReportScore(int currentScore)
{
	#if UNITY_EDITOR || UNITY_STANDALONE_OSX
	#elif UNITY_IPHONE
		if (asutarisuku == IntPtr.Zero)
		{
			return;
		}
		else{
				_RankingReportScore(asutarisuku, currentScore);	
		}
	#elif UNITY_ANDROID
	// mode = 0
		GoodiaPlugin.Call("rankingReportScore", currentScore, 0);
	#endif			
}	
	
public void reportGameStartToFlurry (){
	#if UNITY_EDITOR || UNITY_STANDALONE_OSX
	#elif UNITY_IPHONE
	if (asutarisuku == IntPtr.Zero)
	{
		print ("asutarisuku == IntPtr.Zero");
		return;
	}
	else{
		_ReportGameStartToFlurry(asutarisuku);
	}
	#elif UNITY_ANDROID
	GoodiaPlugin.Call("reportGameCountToFlurry");
	#endif		
}

public void showReviewPopUp()
{
	#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		print ("renew highscore"); 
	#elif UNITY_IPHONE
		print ("renew highscore");
		if (asutarisuku == IntPtr.Zero)
		{
			return;
		}
		else{
			_ShowReviewPopUp(asutarisuku);	
		}
	#endif			
}

public void showLeaderBoard()
{
	#if UNITY_EDITOR || UNITY_STANDALONE_OSX
	print ("_ShowLeaderBoard:");	
	#elif UNITY_IPHONE
	if (asutarisuku == IntPtr.Zero)
	{
		return;
	}
	else{
		_ShowLeaderBoard(asutarisuku);	
	}
	#elif UNITY_ANDROID
	GoodiaPlugin.Call("rankingLeaderBoard", 0);
	#endif			
}
////////////////////////////////////////////////////////////////////////////////////////////////////
public  void AdstirSetVisibility(bool v)
{
	//print (adstirView +" adstirView Unity");
	#if UNITY_EDITOR || UNITY_STANDALONE_OSX
	//print("_adstirPlugin_SetVisibility");
	#elif UNITY_IPHONE
	
	if (adstirView == IntPtr.Zero)
	{
		print ("adstirSetVisibility IntPtr.Zero Unity Editor");
		return;
	}
	else{
		print ("adstirSetVisibility else IntPtr.Zero Unity Editor");
		_adstirPlugin_SetVisibility(adstirView, v);
	}
	
	#endif
}

public void pushPauseButton()
{
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		#elif UNITY_IPHONE
		if (adstirView == IntPtr.Zero)
		{
			return;
		}
		else{
			_PushPauseButton(asutarisuku);
		}

		#elif UNITY_ANDROID
		GoodiaPlugin.Call("gotoPause");

		#endif
}
//タイトルへ戻る
public void pushToTitleBtnInPause()
{
	#if UNITY_EDITOR || UNITY_STANDALONE_OSX
	#elif UNITY_IPHONE
	if (adstirView == IntPtr.Zero)
	{
		return;
	}
	else{
		_PushToTitleBtnInPause(asutarisuku);
	}

	#elif UNITY_ANDROID
		GoodiaPlugin.Call("gotoTitle");
	#endif
}
	//ゲームへ戻る
	public void pushReturnGameBtnInPause()
	{
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		#elif UNITY_IPHONE
		if (adstirView == IntPtr.Zero)
		{
			return;
		}
		else{
			_PushReturnGameBtnInPause(asutarisuku);
		}

		#elif UNITY_ANDROID
		GoodiaPlugin.Call("gotoGame");
		#endif
	}



	public void pushQuestionButtonTitle()
	{
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		#elif UNITY_IPHONE
		if (adstirView == IntPtr.Zero)
		{
			return;
		}
		else{
			_PushQuestionButtonTitle(asutarisuku);
		}
	
		#elif UNITY_ANDROID
		Debug.Log("showHelpInTitle");
		GoodiaPlugin.Call("showHelpInTitle");
		#endif
	}

	public void pushHowtoButtonTitle()
	{
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		#elif UNITY_IPHONE
		if (adstirView == IntPtr.Zero)
		{
			return;
		}
		else{
			_PushHowtoButtonTitle(asutarisuku);
		}

		#elif UNITY_ANDROID
		Debug.Log("hideHelpInTitle");
		GoodiaPlugin.Call("hideHelpInTitle");
		#endif
	}


#region webView		

void OnDestroy()
{
	#if UNITY_EDITOR || UNITY_STANDALONE_OSX
	//if (webView == IntPtr.Zero)return;
	#elif UNITY_IPHONE
	if (webView == IntPtr.Zero)
		return;
	#elif UNITY_ANDROID
	#endif
}


public void SetMargins(int left, int top, int right, int bottom)
{
	#if UNITY_EDITOR || UNITY_STANDALONE_OSX
	
	#elif UNITY_IPHONE
	//if (webView == IntPtr.Zero)return;
	_WebViewPlugin_SetMargins(webView, left, top, right, bottom);
	#elif UNITY_ANDROID
	//if (webView == null)return;
	#endif
}

public void SetVisibility(bool v)
{
	#if UNITY_EDITOR || UNITY_STANDALONE_OSX
	print ("Goodia web");
	#elif UNITY_IPHONE
	if (webView == IntPtr.Zero)
		return;
	_WebViewPlugin_SetVisibility(webView, v);
	#elif UNITY_ANDROID
	if(UnityEngine.Application.systemLanguage.ToString() == "Japanese")
		GoodiaPlugin.Call("launchUrlJPN");
	else
		GoodiaPlugin.Call("launchUrlUSA");
	#endif
}

public void LoadURL(string url)
{
	#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_IPHONE
	//if (webView == IntPtr.Zero)return;
	//_WebViewPlugin_LoadURL(webView, url);
	#elif UNITY_ANDROID
	#endif
}

public void EvaluateJS(string js)
{
	#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_IPHONE
	//if (webView == IntPtr.Zero)return;
	//_WebViewPlugin_EvaluateJS(webView, js);
	#elif UNITY_ANDROID
	#endif
}

public void CallFromJS(string message)
{
	if (callback != null)
		callback(message);
}

#if UNITY_EDITOR || UNITY_STANDALONE_OSX

void Update()
{
	inputString += Input.inputString;
}

#endif

#endregion

}
