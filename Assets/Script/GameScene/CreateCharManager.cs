using UnityEngine;
using System.Collections;

public class CreateCharManager : SingletonMonoBehaviour<CreateCharManager> {

	int touchCount = 0;
	bool checkTimer = true;

	void Start()
	{

	}



	Vector3 Position( Vector3 createRange )
	{
		if(createRange.x >= -1.7f) createRange.Set(-1.7f ,5.8f, -5.2f);
		else if(createRange.x <= -6.7f) createRange.Set(-6.7f ,5.8f, -5.2f);
		
		//連続タッチ時 Z位置をずらす
		touchCount += 1;
		if(touchCount > 1) touchCount = 0;
		
		createRange.z = 1.6f * touchCount;
		return createRange;
	}

	void Instance()
	{

	}

	void On_TouchStart(Gesture gesture){
		if(gesture.touchCount==1 && checkTimer && GameController.score.Value > 0)
		{
			Position((Vector3)(gesture.GetTouchToWordlPoint(20f, false)));
		}

		WaitTime();
	}


	IEnumerator WaitTime(){
		checkTimer = false;
		yield return new WaitForSeconds(0.8f);
		checkTimer = true;
	}

	#region Delegate
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
	#endregion

}
