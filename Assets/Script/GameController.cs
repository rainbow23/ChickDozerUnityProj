using UnityEngine;
using System.Collections;

/// <summary>
/// ゲームのステータスや状況を管理
/// </summary>
public class GameController : SingletonMonoBehaviour<GameController> 
{
	private const string TOTALSCOREKEY =  "totalScore";
	public static int  totalScore{private set; get;}

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

	void Start()
	{
		Application.targetFrameRate = 60;
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

	void addScore(int score){ totalScore += score; }

	void OnDestroy()
	{
		score.Dispose();
		gameState.Dispose();
	}

	void OnApplicationPause(bool pauseStatus) 
	{
		if(pauseStatus)//離れる時
		{
			PlayerPrefs.SetInt( TOTALSCOREKEY,totalScore);
			print ("GameController.totalScore: " + totalScore);
		} 
		else//戻る時
		{
			totalScore = PlayerPrefs.GetInt(TOTALSCOREKEY);
			print ("GameController.totalScore: " + totalScore);
		}


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
