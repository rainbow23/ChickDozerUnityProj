using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public class LabelManager : MonoBehaviour {

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

	



	float timer = 0f;
	int countCheck = 1;

	void Update () {
		///*
		timer +=Time.deltaTime;

		if(timer > 0.55f){
			testBitmap.UpdateNumber(countCheck);	
			countCheck +=1;
			//if(countCheck == 11){Debug.Break();}
			timer = 0f;
		}
		//*/
	}

}
