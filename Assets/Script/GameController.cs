using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Linq;
/// <summary>
/// ゲームのステータスや状況を管理
/// </summary>
public class GameController : MonoBehaviour//SingletonMonoBehaviour<GameController> 
{
	private const string SCOREKEY =  "Score";
	private const string LEVELKEY =  "Level";
	private const string POINTKEY =  "Point";
	private const string SAVEDFIRSTRUNKEY =  "savedFirstRun";
	private const string NEXTLEVELPERCENTAGEKEY =  "nextLevelPercentage";
	
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
	public static int  NextLevelPercentage{private set; get;}

	private bool isLoadedGame = false;

	CreateCharManager createCharManager;
	AudioManager audioManager;
	LabelManager labelManager;

	[HideInInspector]
	public  UnityEvent  UpdatePercentage;
	[HideInInspector]
	public  UnityEvent   UpdateScoreAndLevel;

	private NotificationObject<int> _score = new NotificationObject<int>(0);
	public  NotificationObject<int> score { get{ return _score; }}

	private NotificationObject<Vector3> _touchPos = new NotificationObject<Vector3>();
	public NotificationObject<Vector3> touchPos{ get{ return _touchPos; }}

	private NotificationObject<int> _createCharNum = new NotificationObject<int>(0);
	public NotificationObject<int> createCharNum{ get{ return _createCharNum; }}

	void Awake()
	{
		//PlayerPrefs.DeleteAll();
		LoadElapseData();

		if(checkFirstRun == 0)
		{
			Debug.Log("FirstLaunch");
			Point = 2000;
			Level = 1;
			Score = 0;
			checkFirstRun = 1;
		}
		else{ 

		}

		Application.targetFrameRate = 60;
		isLoadedGame = true;
		audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
		createCharManager = GameObject.Find("CreateCharManager").GetComponent<CreateCharManager>();
		labelManager = GameObject.Find("LabelManager").GetComponent<LabelManager>();
	}

	void Start()
	{

		score.AddListener(AddScore);
		createCharNum.AddListener(audioManager.PlayTouchSE);
		UpdateScoreAndLevel.AddListener(labelManager.updateLabelAction);
		UpdatePercentage.AddListener(labelManager.updatePercentageAction);
	}

	/// <summary>
	/// Modify score and level
	/// </summary>
	void AddScore(int chickScore)
	{ 
		Score += chickScore; 
		Point += chickScore * 10;
		Debug.Log("Score: " + Score);

		UpdateLevelPercentage();
		UpdateLevel();

		//After update score and level  you can call  label action
		UpdateScoreAndLevel.Invoke();
	}

	void UpdateLevelPercentage()
	{
		int nextLevel = Level + 1;
		float nextLevelScore = (float)(DATA.NextLevelScore[nextLevel]);
		float score = (float)Score;
		
		float untilNextScorePercentage = score / nextLevelScore;
		//Debug.Log("untilNextScorePercentage: " + untilNextScorePercentage);
		
		int storeNextLevelPercentage = NextLevelPercentage;
		
		//整数一桁を返す
		if(untilNextScorePercentage > 0.9 && untilNextScorePercentage < 1.0)
			NextLevelPercentage = 8;

		else
			NextLevelPercentage = Mathf.RoundToInt((untilNextScorePercentage) * 10);

		//整数を返す 0,2,4,6,8,10
		if(NextLevelPercentage % 2 != 0)
			NextLevelPercentage -= 1;
		
		//割り切れた時を考慮 , It"s level up timing
		if(NextLevelPercentage >= 10)
		{
			NextLevelPercentage = 10;
		}

		Debug.Log("NextLevelPercentage: " + NextLevelPercentage);

		//更新した時だけラベル更新 またはレベルアップした次にかごに入る時
		if(storeNextLevelPercentage < NextLevelPercentage || NextLevelPercentage == 10 || storeNextLevelPercentage == 10)
			UpdatePercentage.Invoke();
	}

	 void UpdateLevel()
	{
		int nextLevel = Level + 1;
		if(Score >= DATA.NextLevelScore[nextLevel])
		{
			Level += 1;
			Score -= DATA.NextLevelScore[Level]; //スコアを引き今のレベルの得点だけにする
			Debug.Log("Level Up: " + Level);
			Debug.Log("Level Upped Score: " + Score);
		}
	}



	void OnDestroy()
	{
		Debug.Log("GameController OnDestroy");
		score.Dispose();
		createCharNum.Dispose();
		touchPos.Dispose();
		UpdateScoreAndLevel.RemoveAllListeners();
		UpdatePercentage.RemoveAllListeners();
	}

	void OnApplicationPause(bool pauseStatus) 
	{
		//離れる時
		if(pauseStatus){
			#if UNITY_EDITOR
			//ゲームシーンから再生時OnApplicationPauseが呼ばれてしまう
			Debug.Log("OnApplicationPause go away in UnityEditor");
			#endif
			SaveToDisc();
		} 
		//戻る時
		else{ 
			#if UNITY_EDITOR
			//ゲームシーンから再生時OnApplicationPauseが呼ばれてしまうのでLoadElapseData();を呼ばない
			Debug.Log("OnApplicationPause come back in UnityEditor");
			#else
			Debug.Log("OnApplicationPause in  except UnityEditor");
			LoadElapseData();
			#endif
		}
	}

	void OnApplicationQuit()
	{
		Debug.Log("OnApplicationQuit");
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
		PlayerPrefs.SetInt( NEXTLEVELPERCENTAGEKEY, NextLevelPercentage);
		PlayerPrefs.Save();
		//Debug.Log("Level: " + Level );
		//Debug.Log("checkFirstRun: " + checkFirstRun );
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
		NextLevelPercentage = PlayerPrefs.GetInt(NEXTLEVELPERCENTAGEKEY);
		//Debug.Log("Level: " + Level );
		//Debug.Log("checkFirstRun: " + checkFirstRun );
		//Debug.Log("Point: " + Point);
	}




}
