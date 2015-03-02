using UnityEngine;
using System.Collections;

public class SwitchVolumeSprite : MonoBehaviour {
	
	UISprite volumeUISprite;
	
	void Awake(){
		volumeUISprite = GetComponent<UISprite>();
	}

	void Start () {
		//if volume is off
		if(PlayerPrefs.GetInt("volumeSwitch") == 0){
			volumeUISprite.spriteName = "01 title/title_icon_speaker_off";
		}
		else{
			volumeUISprite.spriteName = "01 title/title_icon_speaker_on";
		}
	}
	
	void switchvolumeUISprite(bool onVolume){
		if(onVolume){
			volumeUISprite.spriteName = "01 title/title_icon_speaker_on";
		}
		else{
			volumeUISprite.spriteName = "01 title/title_icon_speaker_off";
		}
	}
	

	void Update () {
	
	}
	
	#region Delegate
	void OnEnable(){
//		SoundManager._volumeSwitch += switchvolumeUISprite;
	}
	void OnDisable(){
		UnSubscribeEvent();
	}
	void OnDestroy(){
		UnSubscribeEvent();
	}
	void UnSubscribeEvent(){
//		SoundManager._volumeSwitch -= switchvolumeUISprite;
	}
	#endregion

	
}
