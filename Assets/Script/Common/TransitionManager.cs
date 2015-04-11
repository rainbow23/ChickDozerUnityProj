using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TransitionManager:SingletonMonoBehaviour<TransitionManager> 
{
	private NotificationObject<GameState> _gameState = new NotificationObject<GameState>( GameState.Title );
	public static NotificationObject<GameState> gameState
	{
		get{ return Instance._gameState; }
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


	void OnLevelWasLoaded(int level)
	{
		switch (Application.loadedLevelName) 
		{
		case SCENE.TITLE:
			gameState.Value = GameState.Title;
			AudioManager.Instance.PlayBGM(AUDIO.BGM_TITLE);
			break;
		case SCENE.GAME:
			gameState.Value = GameState.Play;	
			AudioManager.Instance.PlayBGM(AUDIO.BGM_GAME);
			break;
		case SCENE.COLLECTION:
			gameState.Value = GameState.Collection;
			break;
		}
	}

	void OnDestroy()
	{
		gameState.Dispose();
	}
}
