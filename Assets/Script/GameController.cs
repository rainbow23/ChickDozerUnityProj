using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

/// <summary>
/// ゲームのステータスや状況を管理
/// </summary>
public class GameController : MonoBehaviour//SingletonMonoBehaviour<GameController> 
{
	private const string TOTALSCOREKEY =  "totalScore";
	private const string LEVELKEY =  "Level";

	public static int  totalScore{private set; get;}
	public static int Level = 1;	
	private bool isLoadedGame = false;

	CreateCharManager createCharManager;
	AudioManager audioManager;

	private NotificationObject<int> _score = new NotificationObject<int>(0);
	public  NotificationObject<int> score
	{
		get{ return _score; }
	}

	private NotificationObject<Vector3> _touchPos = new NotificationObject<Vector3>();
	public NotificationObject<Vector3> touchPos
	{
		get{ return _touchPos; }
		//set{ Instance._touchPos = value; }
	}

	private NotificationObject<int> _createCharNum = new NotificationObject<int>(0);
	public NotificationObject<int> createCharNum
	{
		get{ return _createCharNum; }
	}

	void OnLevelWasLoaded(int level)
	{
		switch (Application.loadedLevelName) 
		{
		case SCENE.TITLE:
			Destroy(this);
			break;
		case SCENE.COLLECTION:
			Destroy(this);
			break;
		}
	}

	void Awake()
	{
		//PlayerPrefs.DeleteAll();
		Application.targetFrameRate = 60;
		isLoadedGame = true;
		audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
		createCharManager = GameObject.Find("CreateCharManager").GetComponent<CreateCharManager>();
	}

	void Start()
	{
		LoadElapseData();
		createCharManager.LoadCharDataFromDisc();
		score.AddListener(addScore);
		createCharNum.AddListener(audioManager.PlayTouchSE);
	}

	void addScore(int score)
	{ 
		totalScore += score; 
		UpdateLevel(totalScore);
	}

	 void UpdateLevel(int updateTotalScore)
	{
		if (updateTotalScore < 10) {  Level = 1; }		
		else if(updateTotalScore < 21) { Level = 2; }
		else{Level = 3;}
		print ("Level: " + Level);
	}

	void OnDestroy()
	{
		score.Dispose();
		createCharNum.Dispose();
		touchPos.Dispose();
	}

	void OnApplicationPause(bool pauseStatus) 
	{
		//離れる時
		if(pauseStatus) SaveToDisc();
		//戻る時
		else LoadElapseData();
	}

	void OnApplicationQuit()
	{
		//gamesceneで止めるとなぜかtitleで読み込まれていることになる為、boolを作る
		if(isLoadedGame) SaveToDisc();
	}

	void SaveToDisc()
	{
		createCharManager.SaveCharDataToDisc();

		PlayerPrefs.SetInt( TOTALSCOREKEY,totalScore);
		PlayerPrefs.SetInt( LEVELKEY,Level); 
		PlayerPrefs.Save();
		Debug.Log("SaveToDisc");
	}



	//経過時間を保存する
	void LoadElapseData()
	{
		totalScore = PlayerPrefs.GetInt(TOTALSCOREKEY);
		Level = PlayerPrefs.GetInt(LEVELKEY);
	}




}
