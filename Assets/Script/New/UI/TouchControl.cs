using UnityEngine;
using System.Collections;

public class TouchControl : MonoBehaviour {
	public delegate void touchHandler(Vector3 _touchToWorldPos);
    public static event touchHandler  _touchPosition;
	UI_GameScene UI_GameSceneScript;
	Camera camera2D;
	
	void Awake(){
		UI_GameSceneScript = GameObject.Find("UI").GetComponent<UI_GameScene>();
		camera2D = GameObject.Find("2D Camera").GetComponent<Camera>();
	}
	void Start () {
	
	}
	
	void Update () {
	
	}
	
	float timer = 0f;
	Vector3 touchToWorldPos;
	int touchCount = 0;
	int num = -1;
	
	public void On_TouchStart(Gesture gesture){
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
			if(touchCount > 3){touchCount = 0;}
			
			/*
			num += 1;
			if(num == 4){ num = 0;}
			UI_GameSceneScript.StockChicksUISprite[num].enabled = false;
			*/
			
			float offsetZ = touchToWorldPos.z + (1.6f * touchCount);
			touchToWorldPos = new Vector3(touchToWorldPos.x, touchToWorldPos.y, offsetZ);
			
			_touchPosition(touchToWorldPos); //Delegate
			
			StartCoroutine(waitTime());
		}	
	}
	
	bool touchlimit = false;
	IEnumerator waitTime(){
		touchlimit = true;
		yield return new WaitForSeconds(0.8f);
		touchlimit = false;
	}
	
	
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
