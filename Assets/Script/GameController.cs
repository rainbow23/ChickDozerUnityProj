using UnityEngine;
using System.Collections;

/// <summary>
/// ゲームのステータスや状況を管理
/// </summary>
public class GameController : SingletonMonoBehaviour<GameController> 
{
	private const string TOTALSCOREKEY =  "totalScore";
	private const string LEVELKEY =  "level";
	public static int  totalScore{private set; get;}
	public static int level = 1;

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

	void Awake()
	{
		Application.targetFrameRate = 60;
	}

	void Start()
	{
		LoadData();
		score.AddListener(addScore);
		switch (Application.loadedLevelName) {
			case SCENE.TITLE:
				AudioManager.Instance.PlayBGM(AUDIO.BGM_TITLE);
				break;
			case SCENE.GAME:
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
		if (updateTotalScore < 10) {  level = 1; }		
		else if(updateTotalScore < 21) { level = 2; }
		else{level = 3;}
		print ("level: " + level);
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
			SaveData();
		} 
		else//戻る時
		{
			LoadData();
		}
	}

	void OnApplicationQuit()
	{
		//print ("OnApplicationQuit");
		SaveData();
	}

	void SaveData()
	{
		PlayerPrefs.SetInt( TOTALSCOREKEY,totalScore);
		PlayerPrefs.SetInt( LEVELKEY,level); 
	}

	void LoadData()
	{
		totalScore = PlayerPrefs.GetInt(TOTALSCOREKEY);
		level = PlayerPrefs.GetInt(LEVELKEY);
		//print ("level: " + level);
	}
	
	void OnLevelWasLoaded(int level)
	{
		switch (level) 
		{
			case 0:
				gameState.Value = GameState.Title;
				AudioManager.Instance.PlayBGM(AUDIO.BGM_TITLE);
				break;
			case 1:
				gameState.Value = GameState.Play;	
				AudioManager.Instance.PlayBGM(AUDIO.BGM_GAME);
				break;
			case 2:
				gameState.Value = GameState.Collection;
				break;
		}
	}
}
