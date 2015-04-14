using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public class LabelManager : MonoBehaviour {
	Camera ClearCamera;
	Camera camera2D;
	Transform UIRootBottomTransform;
	GameObject hudObj;
	GameObject particle;
	public BitmapNumberManager[] bitmapNumObject;
	public BitmapNumberManager testBitmap;

	UISprite percentageLabel;
	GameController gameController;
	public UnityAction  updateLabelAction;
	public UnityAction updatePercentageAction;

	void Awake () 
	{
		gameController = GameObject.Find("GameController").GetComponent<GameController>();
		ClearCamera = GameObject.Find("ClearCamera").GetComponent<Camera>();
		camera2D = GameObject.Find("2D Camera").GetComponent<Camera>();
		hudObj = Resources.Load("HUD/HudGrp", typeof(GameObject)) as GameObject;
		UIRootBottomTransform = GameObject.Find("UI Root/Bottom").transform;
		percentageLabel = GameObject.Find("Percentage").GetComponent<UISprite>();
		//particle = Resources.Load("Particle/CFXM2_CartoonFight", typeof(GameObject)) as GameObject; 
		updateLabelAction = UpdatePoint;
		updateLabelAction += UpdateLevel;
		updatePercentageAction = UpdateLevelPercentage;
	}

	void Start () 
	{
		//gameController.touchPos.AddListener(ShowScoreEffect);
		UpdatePoint();
		UpdateLevel();
		UpdateLevelPercentage();
	}


	void UpdatePoint()
	{
		//Debug.Log("UpdatePoint");
		bitmapNumObject[1].UpdateNumber(DATA.Point);
	}

	void UpdateLevel()
	{
		//Debug.Log("UpdateLevel");
		bitmapNumObject[0].UpdateNumber(DATA.Level);
	}

	void UpdateLevelPercentage()
	{
		string percentage = DATA.NextLevelPercentage.ToString();
		string afterPath = "0pcnt";
		if(percentage == "0"){afterPath = "pcnt";}

		percentageLabel.spriteName = percentage + afterPath;
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


	float timer = 0f;
	int countCheck = 99;

	void Update () {
		///*
		timer +=Time.deltaTime;

		if(timer > 0.55f){
			testBitmap.UpdateNumber(countCheck);	
			//bitmapNumObject[0].UpdateNumber(countCheck);
			countCheck +=1;
			//if(countCheck == 11){Debug.Break();}
			timer = 0f;
		}
		//*/
	}

}
