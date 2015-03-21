using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

/// <summary>
/// ゲームのステータスや状況を管理
/// </summary>
public class GameController : MonoBehaviour//SingletonMonoBehaviour<GameController> 
{
	private const string SCOREKEY =  "Score";
	private const string LEVELKEY =  "Level";
	private const string POINTKEY =  "Point";
	private const string SAVEDFIRSTRUNKEY =  "savedFirstRun";
	
	private int checkFirstRun;
	public static int  Score{private set; get;}
	private static int _point = 2000;
	public static int  Point{
		get{ return _point;}
		private set{ _point = value;}
	}

	private static int _level = 1;
	public static int Level{
		get{ return _level;}
		private set{ _level = value;}
	} 

	private bool isLoadedGame = false;

	CreateCharManager createCharManager;
	AudioManager audioManager;
	LabelManager labelManager;

	private  UnityEvent  UpdateScoreAndLevel;

	private NotificationObject<int> _score = new NotificationObject<int>(0);
	public  NotificationObject<int> score { get{ return _score; }}

	private NotificationObject<Vector3> _touchPos = new NotificationObject<Vector3>();
	public NotificationObject<Vector3> touchPos{ get{ return _touchPos; }}

	private NotificationObject<int> _createCharNum = new NotificationObject<int>(0);
	public NotificationObject<int> createCharNum{ get{ return _createCharNum; }}

	void Awake()
	{
		LoadElapseData();
		//PlayerPrefs.DeleteAll();

		if(checkFirstRun == 0)
		{
			Debug.Log("FirstLaunch");
			Point = 2000;
			Level = 1;
			Score = 0;
			checkFirstRun = 1;
		}
		else{ }

		Application.targetFrameRate = 60;
		isLoadedGame = true;
		audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
		createCharManager = GameObject.Find("CreateCharManager").GetComponent<CreateCharManager>();
		labelManager = GameObject.Find("LabelManager").GetComponent<LabelManager>();
	}

	void Start()
	{
		score.AddListener(addScore);
		createCharNum.AddListener(audioManager.PlayTouchSE);

		UpdateScoreAndLevel.AddListener(labelManager.updateLabel);
	}

	/// <summary>
	/// Modify score and level
	/// </summary>
	void addScore(int chickScore)
	{ 
		//Debug.Log("chickScore: " +chickScore);
		Score += chickScore; 
		_point += chickScore * 10;
		UpdateLevel();

		//After update score and level  you can call  label action
		UpdateScoreAndLevel.Invoke();
	}

	 void UpdateLevel()
	{
		int nextLevel = Level + 1;
		if(Score > DATA.NextLevelScore[nextLevel])
		{
			Level += 1;
			Score -= DATA.NextLevelScore[Level]; //スコアを引き今のレベルの得点だけにする
		}
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
		else{ 
			#if UNITY_EDITOR
			//ゲームシーンから再生時OnApplicationPauseが呼ばれてしまうのでLoadElapseData();を呼ばない
			Debug.Log("OnApplicationPause in UnityEditor");
			#else
			Debug.Log("OnApplicationPause in  except UnityEditor");
			LoadElapseData();
			#endif
		}
	}

	void OnApplicationQuit()
	{
		//gamesceneで止めるとなぜかtitleで読み込まれていることになる為、boolを作る
		if(isLoadedGame) SaveToDisc();
	}

	void SaveToDisc()
	{
		Debug.Log("SaveToDisc");
		createCharManager.SaveCharDataToDisc();
		PlayerPrefs.SetInt( SCOREKEY, Score);
		PlayerPrefs.SetInt( LEVELKEY,Level); 
		PlayerPrefs.SetInt( POINTKEY, Point); 
		PlayerPrefs.SetInt( SAVEDFIRSTRUNKEY, checkFirstRun); 
		PlayerPrefs.Save();
		//Debug.Log("Point: " + Point);
	}



	//経過時間を保存する
	void LoadElapseData()
	{
		Debug.Log("LoadElapseData");
		Score = PlayerPrefs.GetInt(SCOREKEY);
		Level = PlayerPrefs.GetInt(LEVELKEY);
		Point = PlayerPrefs.GetInt(POINTKEY);
		checkFirstRun = PlayerPrefs.GetInt(SAVEDFIRSTRUNKEY);
		//Debug.Log("Point: " + Point);
	}




}
