using UnityEngine;
using System.Collections;

public class SE_Sound : MonoBehaviour {
	AudioClip SE_Enter;
	AudioSource audioSource;
	BoxCollider touchCollider;

	void Start () {
		audioSource = GetComponent<AudioSource>();
		SE_Enter = Resources.Load("SE/SE_Enter", typeof(AudioClip)) as AudioClip;	
	}
	
	float waitTimer = 0.3f;

	/*
	void oneShotSE_Enter(){
		if(PlayerPrefs.GetInt("volumeSwitch") == 0){ // volume on
			audioSource.volume = 1.0f;
			audioSource.PlayOneShot(SE_Enter);
		}
	}
	*/	
	
	
	public void oneShotSE_Enter(GameObject obj){
		touchCollider = obj.GetComponent< BoxCollider>();
		touchCollider.enabled = false;
		StartCoroutine(colliderDisableTime());
		
		if(PlayerPrefs.GetInt("volumeSwitch") == 0){ return; } // if volume is off nothins is done.
			audioSource.volume = 1.0f;
			audioSource.PlayOneShot(SE_Enter);
	}
	
	IEnumerator colliderDisableTime(){
		yield return new WaitForSeconds (0.5f);
		touchCollider.enabled = true;
	}
	
	

	/*
	void OnEnable(){
		TrapTrigger._playerDeadByTrapTrigger += destroyPlayerSE;

		TimerManager._TimeUp += timeUpSound;
		PlayerEvent._playerHitWall += playerHitsWallSound;
		PlayerEvent._ballTouchGoal += playerHitsGoalSound;
		UIGameScene._gameStartFlag += CheerSound;
	}
	void OnDisable(){
		unSubscribeEvent();
	}
	void OnDestroy(){
		unSubscribeEvent();
	}
	void unSubscribeEvent(){
		TrapTrigger._playerDeadByTrapTrigger -= destroyPlayerSE;

		TimerManager._TimeUp -= timeUpSound;
		PlayerEvent._playerHitWall -= playerHitsWallSound;
		PlayerEvent._ballTouchGoal -= playerHitsGoalSound;
		UIGameScene._gameStartFlag -= CheerSound;
	}
	*/

	void Update () {
		
	}
}
