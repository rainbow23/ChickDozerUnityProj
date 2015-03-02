/*
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

public class ControlPlugIn : SingletonMonoBehaviour<ControlPlugIn>
{
	public static bool dontDestroyWebView = false;
	public static bool dontDestroyWebViewGameObject = true;
	private string Url = "http://goodiaappcan.appspot.com/appinfo/appinfo.html";
	//public WebViewObject webViewObject;
	
	public WebViewObject webViewGameObject;

	void Awake(){
		webViewGameObject = GameObject.Find("webViewGameObject").GetComponent<WebViewObject>();
		if(!dontDestroyWebView){
			DontDestroyOnLoad(this.gameObject);
			dontDestroyWebView = true;
		}
		else{
			Destroy(this.gameObject);	
		}
	}

	void OnLevelWasLoaded(int level){
		if(level == 0){
			//webview show
			webViewGameObject.hideGoddiaHedderAd();
			//Header Ad
			webViewGameObject.HideiMobileIconTitle();
			webViewGameObject.webViewGoodiaAd_SetVisibility(false);	
			webViewGameObject.asutarisukuVisibility(false);
		}
		//game
        if(level == 1){
			//webview hidden
			webViewGameObject.webViewGoodiaAd_SetVisibility(false);
			
			//AstriskとなっているがiMobileアイコン広告の表示設定
			webViewGameObject.asutarisukuVisibility(true);
			
			//Title Ending のどっちかを出すかスイッチしている
			
			webViewGameObject.ShowiMobileIconEnding();
		}
		if(level ==2){
			//webview show
			webViewGameObject.HideiMobileIconTitle();
			webViewGameObject.webViewGoodiaAd_SetVisibility(false);
			webViewGameObject.asutarisukuVisibility(false);
		}
	}
	
	void Start(){
		//webview show
//		WebViewObjectScript.webViewGoodiaInit();
//		WebViewObjectScript.webViewGoodiaAd_SetVisibility(true);
		/*
		if(webViewObject ==null ){
			webViewObject =
				(new GameObject("WebViewObject")).AddComponent<WebViewObject>();
			webViewObject.AdstirInit();
			webViewGameObject.webViewGoodiaInit();	
			webViewGameObject.asutarisukuLoad();
			webViewGameObject.asutarisukuVisibility(false);
			webViewGameObject.hideGoddiaHedderAd();
			webViewGameObject.LoadURL(Url);//?
		}
		*/
		
			webViewGameObject.AdstirInit();
			webViewGameObject.webViewGoodiaInit();	
			webViewGameObject.asutarisukuLoad();
			webViewGameObject.asutarisukuVisibility(false);
			webViewGameObject.hideGoddiaHedderAd();
			webViewGameObject.LoadURL(Url);//?

		webViewGameObject.Init((msg)=>{
			Debug.Log(string.Format("CallFromJS[{0}]", msg));	
			print ("webViewGameObject.Init Unity");
		});
	}
	
	
}
