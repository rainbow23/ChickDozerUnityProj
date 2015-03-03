using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(UISprite))]
public class FunctionManager : MonoBehaviour {
	public enum FuncType{ 
								HelpFunc = 0, 
								OnOffSound,
								ShowOsusumeApps,
								ShowRanking,
								GameStart
						};

	public FuncType funcType;

	public bool showHelp = true;
	private bool checkAudioIcon = true;
	public GameObject helpGameObj;
	private UISprite thisUISprite;

	private const string offSoundBtnSpriteName = "title_btn_speaker_off";
	private const string onSoundBtnSpriteName = "title_btn_speaker_on";

	AudioManager audioManager;

	void Start()
	{
		switch (funcType) 
		{
		case FuncType.OnOffSound:
			thisUISprite = GetComponent<UISprite>();
			audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();


			if(audioManager.IsMute)
			{ 
				audioManager.StopBGM();
				thisUISprite.spriteName = offSoundBtnSpriteName;
			}
			else 
			{
				thisUISprite.spriteName = onSoundBtnSpriteName;
			}


			break;
		}
	}

	void OnPress(bool isDown)
	{
		if(isDown)
		{
			switch (funcType) 
			{
			case FuncType.HelpFunc:
				helpFunc();
				break;
			case FuncType.OnOffSound:
				onOffSound();
				break;
			case FuncType.ShowOsusumeApps:
				
				break;
			case FuncType.ShowRanking:
				
				break;
			case FuncType.GameStart:
				FadeManager.Instance.LoadLevel(SCENE.GAME, FadeManager.Interval);
				break;
			default:
			break;
			}

		}
	}


	void helpFunc()
	{
		if(showHelp)
		{
			helpGameObj.transform.setLocalPosition(0f, 0f, 0f);
		}
		else
		{
			helpGameObj.transform.setLocalPosition(1000f, 0f, 0f);
		}
	}

	void onOffSound()
	{
		if(thisUISprite.spriteName.Contains("on") )
		{
			audioManager.SetMute(true);
			thisUISprite.spriteName = offSoundBtnSpriteName;
			//print ("thisUISprite.spriteName: " + thisUISprite.spriteName);
		}
		else
		{
			audioManager.SetMute(false);
			thisUISprite.spriteName = onSoundBtnSpriteName;
			//print ("soundOn: " + thisUISprite.spriteName);
		}
	}




	void Update () 
	{
		
	}
}
