using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public class LabelManager : MonoBehaviour {
	Camera ClearCamera;
	Camera camera2D;
	Transform UIRootBottomTransform;
	GameObject scoreEffectObject;
	GameObject particle;

	[SerializeField]
	private BitmapNumberManager scoreNumObject;
	[SerializeField]
	private BitmapNumberManager levelNumObject;

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
		scoreEffectObject = Resources.Load("HUD/CharScoreGrp", typeof(GameObject)) as GameObject;
		UIRootBottomTransform = GameObject.Find("UI Root/Bottom").transform;
		percentageLabel = GameObject.Find("Percentage").GetComponent<UISprite>();
		//particle = Resources.Load("Particle/CFXM2_CartoonFight", typeof(GameObject)) as GameObject; 
		updateLabelAction = UpdatePoint;
		updateLabelAction += UpdateLevel;
		updatePercentageAction = UpdateLevelPercentage;
	}

	void Start () 
	{
		//test
		//gameController.touchPos.AddListener(ShowScoreEffect);

		UpdatePoint();
		UpdateLevel();
		UpdateLevelPercentage();
	}
	
	void UpdatePoint()
	{
		//Debug.Log("UpdatePoint");

		scoreNumObject.UpdateNumber(DATA.Point);
	}

	void UpdateLevel()
	{
		//Debug.Log("UpdateLevel");
		levelNumObject.UpdateNumber(DATA.Level);
	}

	void UpdateLevelPercentage()
	{
		string percentage = DATA.NextLevelPercentage.ToString();
		string afterPath = "0pcnt";
		if(percentage == "0"){afterPath = "pcnt";}

		percentageLabel.spriteName = percentage + afterPath;
	}

	
	public void ShowScoreEffect(int charScore, Vector3 charPos)
	{
		//Instantiate
		GameObject obj = CreateScoreEffect(charPos);
		SetPosition(obj);


		var bitmapNumManager = obj.GetComponent<BitmapNumberManager>();
		bitmapNumManager.UpdateNumber(charScore);

		var effectAnimation = obj.GetComponent<EffectAnimation>();
		effectAnimation.MoveUp();
		/*
		GameObject particleObj =  Instantiate(particle, ConvertPosInto2DCameraPos(charPos), Quaternion.identity) as GameObject;
		particleObj.name = particle.name;
		particleObj.transform.parent = UIRootBottomTransform;
		particleObj.SetActive(true);
		*/
	}

	private GameObject CreateScoreEffect(Vector3 Pos){
		GameObject obj = Instantiate(scoreEffectObject, Convert2DCameraPos(Pos), Quaternion.identity) as GameObject;
		obj.name = scoreEffectObject.name;
		//Debug.Log("Convert2DCameraPos(Pos): " + Convert2DCameraPos(Pos));
		return obj;
	}

	private void SetPosition(GameObject obj)
	{
		//parent position
		obj.transform.parent = UIRootBottomTransform;
		obj.transform.localScale = new Vector3(1f, 1f, 1f);
	}

	Vector3 Convert2DCameraPos(Vector3 charPos)
	{
		////ひよこがかごに落ちた位置Xを計算
		Vector3 screenPoint = ClearCamera.WorldToScreenPoint(charPos); //ClearCamera空間でスクリーン座標に変換
		Vector3 camera2DPos = camera2D.ScreenToWorldPoint(screenPoint);	//スクリーン座標を2D Camera空間に変換
		return new Vector3(camera2DPos.x, camera2DPos.y, 0f);
	}


	float timer = 0f;
	int countCheck = 9;

	void Update () {
		///*
		timer +=Time.deltaTime;

		if(timer > 0.55f){
			//testBitmap.UpdateNumber(countCheck);	
			countCheck +=1;
			//if(countCheck == 11){Debug.Break();}
			timer = 0f;
		}
		//*/
	}

}
