using UnityEngine;
using System.Collections;

public class CommonManager2 : SingletonMonoBehaviour<CommonManager2> {

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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
