using UnityEngine;
using System.Collections;

public class AdControlManager : MonoBehaviour{
	WebViewObject WebViewObjectScript;
	private string Url = "http://goodiaappcan.appspot.com/appinfo/appinfo.html";
	
	void Start () {
		/*
		WebViewObjectScript = GetComponent<WebViewObject>();
		WebViewObjectScript.AdstirInit();
		WebViewObjectScript.showAdStir();	
		WebViewObjectScript.asutarisukuLoad();
		//Header Ad
		//WebViewObjectScript.webViewGoodiaInit();	
		
		WebViewObjectScript.LoadURL(Url);
		
		WebViewObjectScript.Init((msg)=>{
			Debug.Log(string.Format("CallFromJS[{0}]", msg));	
			print ("webViewGameObject.Init Unity");
		});
		*/
	}
	
	
	
	void buyPoint(){
	//	WebViewObjectScript.inAppPurchase();
	}
	
	
	void Update () {
	
	}
}
