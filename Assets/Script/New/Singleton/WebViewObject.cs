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


public class WebViewObject : SingletonMonoBehaviour<WebViewObject>
{
	
	public void Awake (){
		if (this != Instance) {
			Destroy(this);
			Destroy (this.gameObject);
			return;
		}
		else{
			DontDestroyOnLoad (this.gameObject);
		}
	}
	void OnLevelWasLoaded(int level) {
        if (level == 1){
			
		}
	}
	
	
	Callback callback;
	#if UNITY_EDITOR || UNITY_STANDALONE_OSX
	IntPtr webView;
	
	////////////////////////////////////////////////////////////////////////////////////////////////////
	IntPtr adstirView;
	bool adstirVisibility;
	IntPtr webViewGoodiaAd;
	IntPtr asutarisuku;
	////////////////////////////////////////////////////////////////////////////////////////////////////	
	
	bool visibility;
	Rect rect;
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
	AndroidJavaObject webView;
	Vector2 offset;
	#endif

	#if UNITY_EDITOR || UNITY_STANDALONE_OSX
	[DllImport("WebView")]
	private static extern IntPtr _WebViewPlugin_Init(
		string gameObject, int width, int height, bool ineditor);
	[DllImport("WebView")]
	private static extern int _WebViewPlugin_Destroy(IntPtr instance);
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
	public static extern IntPtr _webViewGoodiaInit(string gameObject);
	[DllImport("WebView")]
	public static extern void _webViewGoodiaAdSetVisibility (IntPtr instance,bool setVisibility);
	
	[DllImport("WebView")]
	private static extern  IntPtr _AsutarisukuLoad(string gameObject);
	[DllImport("WebView")]
	private static extern void  _AsutarisukuVisibility (IntPtr instance,bool setVisibility);
	
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
	private static extern void  _SendBestScore (IntPtr instance, int bestScore);	

	[DllImport("WebView")]
	private static extern void  _ShowAppliPromotion (IntPtr instance);

	[DllImport("WebView")]
	private static extern void  _ShowGameFeat (IntPtr instance);

	[DllImport("WebView")]
	private static extern void  _ShowiMobileIconTitle (IntPtr instance);
	[DllImport("WebView")]
	private static extern void  _HideiMobileIconTitle (IntPtr instance);

	[DllImport("WebView")]
	private static extern void  _ShowiMobileIconEnding (IntPtr instance);
	[DllImport("WebView")]
	private static extern void  _HideiMobileIconEnding (IntPtr instance);
	[DllImport("WebView")]
	private static extern void  _ShowAddRanking (IntPtr instance);
	
	//InAppPurchase
	[DllImport("WebView")]
	private static extern void  _InAppPurchase (IntPtr instance);

	////////////////////////////////////////////////////////////////////////////////////////////////////
	#elif UNITY_IPHONE
	[DllImport("__Internal")]
	private static extern IntPtr _WebViewPlugin_Init(string gameObject);
	[DllImport("__Internal")]
	private static extern int _WebViewPlugin_Destroy(IntPtr instance);
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
	public static extern void _adstirPlugin_SetVisibility (IntPtr instance,bool setVisibility);

	[DllImport("__Internal")]
	public static extern IntPtr _webViewGoodiaInit(string gameObject);
	[DllImport("__Internal")]
	public static extern void _webViewGoodiaAdSetVisibility (IntPtr instance,bool setVisibility);
	
	[DllImport("__Internal")]
	private static extern  IntPtr _AsutarisukuLoad(string gameObject);
	[DllImport("__Internal")]
	private static extern void  _AsutarisukuVisibility (IntPtr instance,bool setVisibility);
	
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
	private static extern void  _SendBestScore (IntPtr instance, int bestScore);	
	[DllImport("__Internal")]
	private static extern void  _ShowAppliPromotion (IntPtr instance);

	[DllImport("__Internal")]
	private static extern void  _ShowGameFeat (IntPtr instance);

	[DllImport("__Internal")]
	private static extern void  _ShowiMobileIconTitle (IntPtr instance);
	[DllImport("__Internal")]
	private static extern void  _HideiMobileIconTitle (IntPtr instance);

	[DllImport("__Internal")]
	private static extern void  _ShowiMobileIconEnding (IntPtr instance);
	[DllImport("__Internal")]
	private static extern void  _HideiMobileIconEnding (IntPtr instance);

	[DllImport("__Internal")]
	private static extern void  _ShowAddRanking (IntPtr instance);
	//InAppPurchase
	[DllImport("__Internal")]
	private static extern void  _InAppPurchase (IntPtr instance);
	////////////////////////////////////////////////////////////////////////////////////////////////////
	//
	#endif	
	
	#if UNITY_EDITOR || UNITY_STANDALONE_OSX
	private void CreateTexture(int x, int y, int width, int height)
	{
		int w = 1;
		int h = 1;
		while (w < width)
			w <<= 1;
		while (h < height)
			h <<= 1;
		rect 	= new Rect(x, y, width, height);
		texture = new Texture2D(w, h, TextureFormat.ARGB32, false);
	}
	#endif
	//Astrsk

	public void showAstrskView(){
		asutarisukuVisibility(true);
	}	
	public void hideAstrskView(){
		asutarisukuVisibility(false);
	}

	
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
		CreateTexture(0, 0, Screen.width, Screen.height);
		webView = _WebViewPlugin_Init(name, Screen.width, Screen.height,
			Application.platform == RuntimePlatform.OSXEditor);
		#elif UNITY_IPHONE
		webView = _WebViewPlugin_Init(name);
		#elif UNITY_ANDROID
		offset = new Vector2(0, 0);
		webView = new AndroidJavaObject("net.gree.unitywebview.WebViewPlugin");
		webView.Call("Init", name);
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

	public  void asutarisukuVisibility(bool setVisibility)
	{	
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		if (asutarisuku == IntPtr.Zero){ return; }
		else{
			_AsutarisukuVisibility(asutarisuku, setVisibility);
		}
		#elif UNITY_IPHONE
		if(asutarisuku == IntPtr.Zero){
			return;
		}
		else{
		_AsutarisukuVisibility(asutarisuku, setVisibility);
		}
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
		if (webViewGoodiaAd == IntPtr.Zero){
			return;
		}
		else
		{
		_webViewGoodiaAdSetVisibility(webViewGoodiaAd, setVisibility);
		}
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
	
	public void inAppPurchase(){
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		if (asutarisuku == IntPtr.Zero){
			return;
		}
		else{
			//print ("Unity Log: _InAppPurchase");
			_InAppPurchase(asutarisuku);	
		}
		#elif UNITY_IPHONE
		if (asutarisuku == IntPtr.Zero)
		{
			print ("asutarisuku == IntPtr.Zero");
			return;
		}
		else{
			//print ("Unity Log: CheckInAppPurchaseIsDisable");
			_InAppPurchase(asutarisuku);	
		}
		#endif		
	}
	
	//////////////////////////////////////////////////////////////////////////////////////////////
	public void ShowAppliPromotion(){
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		if (asutarisuku == IntPtr.Zero){
			return;
		}
		else{
			_ShowAppliPromotion(asutarisuku);	
		}
		#elif UNITY_IPHONE
		if (asutarisuku == IntPtr.Zero)
		{
			return;
		}
		else{
			_ShowAppliPromotion(asutarisuku);	
		}
		#endif			
	}

	public void ShowGameFeat(){
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		if (asutarisuku == IntPtr.Zero){
			return;
		}
		else{
			_ShowGameFeat(asutarisuku);
		}
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

	public void ShowiMobileIconTitle(){
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		if (asutarisuku == IntPtr.Zero){
			return;
		}
		else{
			_ShowiMobileIconTitle(asutarisuku);
		}
		#elif UNITY_IPHONE
		if (asutarisuku == IntPtr.Zero)
		{
			return;
		}
		else{
			_ShowiMobileIconTitle(asutarisuku);
		}
		#endif			
	}
	public void HideiMobileIconTitle(){
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		if (asutarisuku == IntPtr.Zero){
			return;
		}
		else{
			_HideiMobileIconTitle(asutarisuku);
		}
		#elif UNITY_IPHONE
		if (asutarisuku == IntPtr.Zero)
		{
			return;
		}
		else{
			_HideiMobileIconTitle(asutarisuku);
		}
		#endif			
	}

	public void ShowiMobileIconEnding(){
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		if (asutarisuku == IntPtr.Zero){
			return;
		}
		else{
			_ShowiMobileIconEnding(asutarisuku);
		}
		#elif UNITY_IPHONE
		if (asutarisuku == IntPtr.Zero)
		{
			return;
		}
		else{
			_ShowiMobileIconEnding(asutarisuku);
		}
		#endif			
	}
	public void HideiMobileIconEnding(){
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		if (asutarisuku == IntPtr.Zero){
			return;
		}
		else{
			_HideiMobileIconEnding(asutarisuku);
		}
		#elif UNITY_IPHONE
		if (asutarisuku == IntPtr.Zero)
		{
			return;
		}
		else{
			_HideiMobileIconEnding(asutarisuku);
		}
		#endif			
	}
	public void ShowAddRanking(){
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		if (asutarisuku == IntPtr.Zero){
			print ("asutarisuku == IntPtr.Zero");
			return;
		}
		else{
			_ShowAddRanking(asutarisuku);
		}
		#elif UNITY_IPHONE
		if (asutarisuku == IntPtr.Zero){	
			print ("asutarisuku == IntPtr.Zero");
			return;
		}
		else{
			_ShowAddRanking(asutarisuku);
		}
		#endif			
	}

	public void SendTweetScore(int scores)
	{
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		if (asutarisuku == IntPtr.Zero)
		{		
			return;
		}
		else
		{
			_SendTweetScore(asutarisuku, scores);	
		}		
		#elif UNITY_IPHONE
		if (asutarisuku == IntPtr.Zero)
		{
			return;
		}
		else{
			_SendTweetScore(asutarisuku, scores);	
		}
		#endif			
	}
	public void SendTweetBestScore(int bestScore)
	{
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		if (asutarisuku == IntPtr.Zero)
		{		
			return;
		}
		else
		{
			_SendTweetBestScore(asutarisuku, bestScore);	
		}		
		#elif UNITY_IPHONE
		if (asutarisuku == IntPtr.Zero)
		{
			return;
		}
		else{
			_SendTweetBestScore(asutarisuku, bestScore);	
		}
		#endif			
	}
	
	
	public void sendBestScore(int bestScore)
	{
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		if (asutarisuku == IntPtr.Zero)
		{		
			return;
		}
		else
		{
			_SendBestScore(asutarisuku, bestScore);	
		}		
		#elif UNITY_IPHONE
		if (asutarisuku == IntPtr.Zero)
		{
			return;
		}
		else{
			_SendBestScore(asutarisuku, bestScore);	
		}
		#endif			
	}	
	public void showReviewPopUp()
	{
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		if (asutarisuku == IntPtr.Zero)
		{		
			return;
		}
		else
		{
			_ShowReviewPopUp(asutarisuku);	
		}		
		#elif UNITY_IPHONE
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
		if (asutarisuku == IntPtr.Zero)
		{		
			return;
		}
		else
		{
			_ShowLeaderBoard(asutarisuku);	
		}
		#elif UNITY_IPHONE
		if (asutarisuku == IntPtr.Zero)
		{
			return;
		}
		else{
			_ShowLeaderBoard(asutarisuku);	
		}
		#endif			
	}
	////////////////////////////////////////////////////////////////////////////////////////////////////
	public  void AdstirSetVisibility(bool v)
	{
		 //print (adstirView +" adstirView Unity");
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		if (adstirView == IntPtr.Zero)
		{		
			return;
		}
		else
		{
			//	adstirVisibility = v;
			_adstirPlugin_SetVisibility(adstirView, v);	
		}
		//_WebViewPlugin_SetVisibility(webView, v);
		
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


	#region webView		

	void OnDestroy()
	{
	#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		if (webView == IntPtr.Zero)
			return;
		_WebViewPlugin_Destroy(webView);
	#elif UNITY_IPHONE
		if (webView == IntPtr.Zero)
			return;
		_WebViewPlugin_Destroy(webView);
	#elif UNITY_ANDROID
		if (webView == null)
			return;
		webView.Call("Destroy");
	#endif
	}


	public void SetMargins(int left, int top, int right, int bottom)
	{
	#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		if (webView == IntPtr.Zero)
			return;
		int width = Screen.width - (left + right);
		int height = Screen.height - (bottom + top);
		CreateTexture(left, bottom, width, height);
		_WebViewPlugin_SetRect(webView, width, height);
	#elif UNITY_IPHONE
		if (webView == IntPtr.Zero)
			return;
		_WebViewPlugin_SetMargins(webView, left, top, right, bottom);
	#elif UNITY_ANDROID
		if (webView == null)
			return;
		offset = new Vector2(left, top);
		webView.Call("SetMargins", left, top, right, bottom);
	#endif
	}

	public void SetVisibility(bool v)
	{
	#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		if (webView == IntPtr.Zero)
			return;
		visibility = v;
		_WebViewPlugin_SetVisibility(webView, v);
	#elif UNITY_IPHONE
		if (webView == IntPtr.Zero)
			return;
		_WebViewPlugin_SetVisibility(webView, v);
	#elif UNITY_ANDROID
		if (webView == null)
			return;
		webView.Call("SetVisibility", v);
	#endif
	}

	public void LoadURL(string url)
	{
	#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_IPHONE
		if (webView == IntPtr.Zero)
			return;
		_WebViewPlugin_LoadURL(webView, url);
	#elif UNITY_ANDROID
		if (webView == null)
			return;
		webView.Call("LoadURL", url);
	#endif
	}

	public void EvaluateJS(string js)
	{
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_IPHONE
		if (webView == IntPtr.Zero)
			return;
		_WebViewPlugin_EvaluateJS(webView, js);
	#elif UNITY_ANDROID
		if (webView == null)
			return;
		webView.Call("LoadURL", "javascript:" + js);
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

	void OnGUI()
	{
		if (webView == IntPtr.Zero || !visibility)
			return;

		Vector3 pos = Input.mousePosition;
		bool down = Input.GetButton("Fire1");
		bool press = Input.GetButtonDown("Fire1");
		bool release = Input.GetButtonUp("Fire1");
		float deltaY = Input.GetAxis("Mouse ScrollWheel");
		bool keyPress = false;
		string keyChars = "";
		short keyCode = 0;
		if (inputString.Length > 0) {
			keyPress = true;
			keyChars = inputString.Substring(0, 1);
			keyCode = (short)inputString[0];
			inputString = inputString.Substring(1);
		}
		_WebViewPlugin_Update(webView,
			(int)(pos.x - rect.x), (int)(pos.y - rect.y), deltaY,
			down, press, release, keyPress, keyCode, keyChars,
			texture.GetNativeTextureID());
		GL.IssuePluginEvent((int)webView);
		Matrix4x4 m = GUI.matrix;
		GUI.matrix = Matrix4x4.TRS(new Vector3(0, Screen.height, 0),
			Quaternion.identity, new Vector3(1, -1, 1));
		GUI.DrawTexture(rect, texture);
		GUI.matrix = m;
	}
	#endif

	#endregion
}
