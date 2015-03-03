using UnityEngine;
using System.Collections;

public class LabelManager : MonoBehaviour {
	Camera ClearCamera;
	Camera camera2D;
	Transform UIRootBottomTransform;
	GameObject hudObj;
	GameObject particle;

	// Use this for initialization
	void Awake () {
		ClearCamera = GameObject.Find("ClearCamera").GetComponent<Camera>();
		camera2D = GameObject.Find("2D Camera").GetComponent<Camera>();
		hudObj = Resources.Load("HUD/HudGrp", typeof(GameObject)) as GameObject;
		UIRootBottomTransform = GameObject.Find("UI Root/Bottom").transform;
		GameController.touchPos.AddListener(ShowScoreEffect);
		//particle = Resources.Load("Particle/CFXM2_CartoonFight", typeof(GameObject)) as GameObject; 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void ShowScoreEffect(Vector3 worldPos)
	{
		//Instantiate
		/*
		GameObject obj = Instantiate(hudObj, ConvertPosInto2DCameraPos(worldPos), Quaternion.identity) as GameObject;
		obj.name = hudObj.name;
		
		//parent position
		obj.transform.parent = UIRootBottomTransform;
		obj.transform.localScale = new Vector3(1f, 1f, 1f);


		GameObject particleObj =  Instantiate(particle, ConvertPosInto2DCameraPos(worldPos), Quaternion.identity) as GameObject;
		particleObj.name = particle.name;
		particleObj.transform.parent = UIRootBottomTransform;
		particleObj.SetActive(true);
		*/
	}

	Vector3 ConvertPosInto2DCameraPos(Vector3 worldPos)
	{
		////ひよこがかごに落ちた位置Xを計算
		Vector3 screenPoint = ClearCamera.WorldToScreenPoint(worldPos); //ClearCamera空間でスクリーン座標に変換
		Vector3 camera2DPos = camera2D.ScreenToWorldPoint(screenPoint);	//スクリーン座標を2D Camera空間に変換
		return new Vector3(camera2DPos.x, 160f, 0f);
	}


}
