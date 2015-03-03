using UnityEngine;
using System.Collections;

/// <summary>
/// ゲームのステータスや状況を管理
/// </summary>
public class GameController : SingletonMonoBehaviour<GameController> 
{

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

	private NotificationObject<int> _charHitGoalNum = new NotificationObject<int>();
	public static NotificationObject<int> CharHitGoalNum
	{
		get{ return Instance._charHitGoalNum; }
	}

	public enum GameState
	{
		Title,
		Play,
		Collection
	}

	void Start()
	{
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

	void OnDestroy()
	{
		score.Dispose();
		gameState.Dispose();
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
