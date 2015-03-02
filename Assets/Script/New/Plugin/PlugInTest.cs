using UnityEngine;
using System.Collections;

public class PlugInTest : MonoBehaviour {
	
	WebViewObject WebViewObjectScript;
	
	void Start () {
		WebViewObjectScript = GameObject.Find("webViewGameObject").GetComponent<WebViewObject>();
	}
	
	void InAppPurchase(){
		WebViewObjectScript.inAppPurchase();
	}
	
	void Update () {
	
	}
}
