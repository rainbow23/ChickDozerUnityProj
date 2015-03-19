using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

/// <summary>
/// ゲームのステータスや状況を管理
/// </summary>
public class GameController : SingletonMonoBehaviour<GameController> 
{
	private const string TOTALSCOREKEY =  "totalScore";
	private const string LEVELKEY =  "Level";
	private const string CHARPOSKEY = "CharPosKey";
	private const string CHARROTKEY = "CharRotKey";
	private const string CHARKINDKEY = "CharKindKey";
	public static int  totalScore{private set; get;}
	public static int Level = 1;	
	private bool isLoadedGame = false;

	public List <Vector3> charPosList =  new List<Vector3>();
	public List <Vector3> charRotList =  new List<Vector3>();
	public List <int> charKindList =  new List<int>();


	public delegate void SaveCharData();
	public event SaveCharData saveCharData;

	private NotificationObject<int> _score = new NotificationObject<int>(0);
	public static NotificationObject<int> score
	{
		get{ return Instance._score; }
	}
	
	private NotificationObject<GameState> _gameState = new NotificationObject<GameState>( GameState.Title );
	public static NotificationObject<GameState> gameState
	{
		get{ return Instance._gameState; }
	}

	private NotificationObject<Vector3> _touchPos = new NotificationObject<Vector3>();
	public static NotificationObject<Vector3> touchPos
	{
		get{ return Instance._touchPos; }
		//set{ Instance._touchPos = value; }
	}

	private NotificationObject<int> _createCharNum = new NotificationObject<int>(0);
	public static NotificationObject<int> createCharNum
	{
		get{ return Instance._createCharNum; }
	}


	public enum GameState
	{
		Title,
		Play,
		Collection
	}

	void OnLevelWasLoaded(int level)
	{
		switch (Application.loadedLevelName) 
		{
		case SCENE.TITLE:
			gameState.Value = GameState.Title;
			AudioManager.Instance.PlayBGM(AUDIO.BGM_TITLE);
			break;
		case SCENE.GAME:
			isLoadedGame = true;
			LoadData();
			gameState.Value = GameState.Play;	
			AudioManager.Instance.PlayBGM(AUDIO.BGM_GAME);
			break;
		case SCENE.COLLECTION:
			gameState.Value = GameState.Collection;
			break;
		}
	}

	void Awake()
	{
		//PlayerPrefs.DeleteAll();
		Application.targetFrameRate = 60;
	}

	void Start()
	{
		switch (Application.loadedLevelName) {
			case SCENE.TITLE:
				AudioManager.Instance.PlayBGM(AUDIO.BGM_TITLE);
				break;
			case SCENE.GAME:
				isLoadedGame = true;
				LoadData();
				LoadCharDataFromDisc();
				score.AddListener(addScore);
				AudioManager.Instance.PlayBGM(AUDIO.BGM_GAME);
				break;
			case SCENE.COLLECTION:
				AudioManager.Instance.PlayBGM(AUDIO.BGM_GAME);
				break;
			}
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
		gameState.Dispose();
	}

	void OnApplicationPause(bool pauseStatus) 
	{
		if(pauseStatus)//離れる時
		{
			SaveToDisc();
		} 
		else//戻る時
		{
			//Debug.Log("OnApplicationResumeAfterPause ");
			LoadData();
		}
	}

	void OnApplicationQuit()
	{
		if(isLoadedGame) SaveToDisc();
	}

	void SaveToDisc()
	{
		//重複保存を避ける
		charPosList.Clear();
		charRotList.Clear();
		charKindList.Clear();

		saveCharData();

		Vector3[] charPos = charPosList.ToArray();
		Vector3[] charRot = charRotList.ToArray();
		int[] charKind = charKindList.ToArray();

		PlayerPrefsX.SetVector3Array(CHARPOSKEY, charPos);
		PlayerPrefsX.SetVector3Array(CHARROTKEY, charRot);
		PlayerPrefsX.SetIntArray(CHARKINDKEY, charKind);

		PlayerPrefs.SetInt( TOTALSCOREKEY,totalScore);
		PlayerPrefs.SetInt( LEVELKEY,Level); 
		PlayerPrefs.Save();
		Debug.Log("SaveToDisc");
	}

	void LoadCharDataFromDisc()
	{
		Vector3[] charPos = PlayerPrefsX.GetVector3Array(CHARPOSKEY);
		Vector3[] charRot = PlayerPrefsX.GetVector3Array(CHARROTKEY);
		int[] charKind = PlayerPrefsX.GetIntArray(CHARKINDKEY);

		Debug.Log("Load");

		CreateCharManager createCharManager = GameObject.Find("CreateCharManager").GetComponent<CreateCharManager>(); 
		//restore chick
		for(int i =0; i < charPos.Length; i++)
		{
			createCharManager.RestoreChick(charPos[i], charRot[i], charKind[i]);
		}

	}
	//経過時間を保存する
	void LoadData()
	{
		totalScore = PlayerPrefs.GetInt(TOTALSCOREKEY);
		Level = PlayerPrefs.GetInt(LEVELKEY);
	}




}
