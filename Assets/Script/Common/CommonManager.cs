using UnityEngine;
using System.Collections.Generic;

public class CommonManager : SingletonMonoBehaviour<CommonManager>
{
	//=================================================================================
	//初期化
	//=================================================================================

	private void Awake (){


	
		//シーン移動後、前シーンからのCommonnManagerがいる時はデストロイ
		if(GameObject.FindGameObjectsWithTag (gameObject.tag).Length > 1){
			Destroy (gameObject);
			return;
		}
			
		if (this != Instance) {
			Destroy (this);
			return;
		}

		DontDestroyOnLoad (this.gameObject);

		//FPS設定
		Application.targetFrameRate = 60;
	}


	void OnLevelWasLoaded(int level)
	{

	}
}