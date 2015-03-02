using UnityEngine;
using System.Collections;

public class StockChickBehaviour : MonoBehaviour {
	UISprite stockChickUISprite;
	
	void Start () {
		stockChickUISprite = GetComponent<UISprite>();
	}
	
	float timer = 0f;
	void Update () {
		/*
		if(stockChickUISprite.enabled == false){
			timer += Time.deltaTime;
		}
		if(timer > 1f){
			stockChickUISprite.enabled = true;
			timer = 0f;
		}
		print ("timer: " + timer);
		*/
	}
	
}
