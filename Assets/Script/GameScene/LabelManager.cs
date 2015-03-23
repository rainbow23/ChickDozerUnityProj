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
	UISprite[] pointsLabels;
	UISprite[] levelLabels = new UISprite[2];
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
		pointsLabels = GameObject.Find("PointGrp").GetComponentsInChildren<UISprite>()
				.OrderBy( t=>{
					string index = t.name.Substring(t.name.Length - 1);
					t.gameObject.SetActive(false);
					return int.Parse(index);
				})
				.ToArray();

		for (int i = 0; i < levelLabels.Length; i++) 
		{
			levelLabels[i] = GameObject.Find("LvScore" + (i + 1).ToString()).GetComponent<UISprite>();
			levelLabels[i].gameObject.SetActive(false);
		}

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
		int point = GameController.Point;
		foreach(var each in pointsLabels)
		{
			if(point < 1) return;
			//Debug.Log("point: " + point);
			if(!each.gameObject.activeSelf) each.gameObject.SetActive(true);
			each.spriteName = "numberFont/" + (point % 10).ToString();
			point  /= 10;
		}
	}

	void UpdateLevel()
	{
		//Debug.Log("UpdateLevel");
		int level = GameController.Level;
		foreach (var each in levelLabels) 
		{
			//print ("level: "  + level);
			if(level < 1) return;
			if(!each.gameObject.activeSelf) each.gameObject.SetActive(true);
			each.spriteName = "numberFont/" + (level % 10).ToString();
			level  /= 10;
		}
	}

	void UpdateLevelPercentage()
	{
		string percentage = GameController.NextLevelPercentage.ToString();
		string path = "levelpercent_sprite/";
		string afterPath = "0pcnt";
		if(percentage == "0"){afterPath = "pcnt";}

		percentageLabel.spriteName = path + percentage + afterPath;
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

	// Update is called once per frame
	void Update () {
		
	}

}
