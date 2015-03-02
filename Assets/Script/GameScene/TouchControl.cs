﻿using UnityEngine;
using System.Collections;

public class TouchControl : MonoBehaviour {	
	private float timer = 0f;
	private Vector3 touchToWorldPos;
	private int touchCount = 0;
	private bool touchlimit = false;

	public void On_TouchStart(Gesture gesture)
	{
		if(gesture.touchCount==1){
			if(touchlimit == true){return;}	
			
			//timer += Time.deltaTime;
			if(ScoreManager.point <= 0){return;}
			touchToWorldPos = (Vector3)(gesture.GetTouchToWordlPoint(20f, false));
			if( touchToWorldPos.x >= -1.7f){ touchToWorldPos.x = -1.7f; }
			if( touchToWorldPos.x <= -6.7f){ touchToWorldPos.x = -6.7f; }
			touchToWorldPos = new Vector3(touchToWorldPos.x, 5.8f, -5.2f);
			
			//連続タッチ時 Z位置をずらす
			touchCount += 1;
			if(touchCount > 1){touchCount = 0;}
			
			float offsetZ = touchToWorldPos.z + (1.6f * touchCount);
			touchToWorldPos = new Vector3(touchToWorldPos.x, touchToWorldPos.y, offsetZ);

			GameController.touchPos.Value = touchToWorldPos;

			StartCoroutine(waitTime());
		}	
	}
		
	IEnumerator waitTime(){
		touchlimit = true;
		yield return new WaitForSeconds(0.8f);
		touchlimit = false;
	}

	void Awake(){}

	void Start () {}
	
	void Update () {}

	
	void OnEnable(){
		EasyTouch.On_TouchStart += On_TouchStart; 
	}

	void OnDisable(){
		UnSubscribeEvent();
	}
	void OnDestroy(){
		UnSubscribeEvent();
	}
	void UnSubscribeEvent(){
		EasyTouch.On_TouchStart -= On_TouchStart; 
	}

}