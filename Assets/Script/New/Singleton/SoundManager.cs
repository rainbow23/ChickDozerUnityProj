using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : SingletonMonoBehaviour<SoundManager>{
	
	public delegate void volumeOnOffHandler(bool onVolume);
	public static event volumeOnOffHandler _volumeSwitch;
	
	
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

	public static AudioClip BGM_Title {get; private set;}
	public static AudioClip BGM_InGame {get; private set;}
	public static AudioClip BGM_Result {get; private set;}
	

	public static AudioSource audioSource {get; private set;}
	UISprite volumeUISprite;
	
	public  enum storeStageName{
		Title = 0,
		StageCastle2,
		Collection
	}

	public  storeStageName storeStageNameType ;

	void Start () {
		audioSource = gameObject.GetComponent<AudioSource>();
		BGM_Title = Resources.Load("BGM/BGM_Title", typeof(AudioClip)) as AudioClip;
		BGM_InGame = Resources.Load("BGM/BGM_InGame", typeof(AudioClip)) as AudioClip;
		BGM_Result = Resources.Load("BGM/BGM_Result", typeof(AudioClip)) as AudioClip;
		
		//PlayerPrefs.SetInt("volumeSwitch",1);

		//initialize
		if(PlayerPrefs.GetInt("volumeSwitch") != 0){// if volume is not off
			if(Application.loadedLevelName == "Title"){
				switchPlayBGM(BGM_Title);
			}
			else if(Application.loadedLevelName == "StageCastle2"){
				switchPlayBGM(BGM_InGame);
			}
		}
	}

	 int count =100;

	public  void OnLevelWasLoaded(int level) {
		////Awakeより先に呼ばれるので登録されていないinstanceは削除
		if (this != Instance) {
			Destroy(this);
			Destroy (this.gameObject);
			return;
		}
		else{
			DontDestroyOnLoad (this.gameObject);
		}

		switch(level){
			case(0)://Title
				switchPlayBGM(BGM_Title);
				break;
			case(1)://StageCastle2
				switchPlayBGM(BGM_InGame);
				storeStageNameType = storeStageName.StageCastle2;
				break;
			case(2)://Collection
				storeStageNameType = storeStageName.Collection;
				break;
		}
	}

	
	
	public  void switchPlayBGM(AudioClip audioClip){
		if(PlayerPrefs.GetInt("volumeSwitch") == 1){// volume on
			audioSource.volume = 1.0f;
			audioSource.clip = audioClip;
			audioSource.loop = true;
			audioSource.Play ();	
		}
		else{
			audioSource.Pause ();
		}
	}

	void volumeOnOff(){
		if(PlayerPrefs.GetInt("volumeSwitch")== 0){// if volume off
			audioSource.volume = 1.0f;
			audioSource.loop = true;
			audioSource.Play ();	
			PlayerPrefs.SetInt("volumeSwitch",1); // set volume on
//			_volumeSwitch(true); //delegate SwitchVolumeSprite.cs
			print("volumeOn");
		}
		else{
			audioSource.Pause ();
			PlayerPrefs.SetInt("volumeSwitch",0); // set volume off
//			_volumeSwitch(false); //delegate SwitchVolumeSprite.cs
			print("volumeOff");
		}
	}


	void stopBGM(){
		audioSource.Stop();
	}

	#region Delegate
	void OnEnable(){
		//TimerManager._TimeUp += stopBGM;
		//PlayerEvent._ballTouchGoal += stopBGM;
	}
	void OnDisable(){
		unSubscribeEvent();
	}
	void OnDestroy(){
		unSubscribeEvent();
	}
	void unSubscribeEvent(){
		//TimerManager._TimeUp -= stopBGM;
		//PlayerEvent._ballTouchGoal -= stopBGM;
	}
	#endregion

	void Update () {

	}
}
