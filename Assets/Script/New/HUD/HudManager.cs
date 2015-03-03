using UnityEngine;
using System.Collections;

public class HudManager : MonoBehaviour {
	#region variable
	GameObject chickScoreHud;
	GameObject scoreHud;
	Transform hudParentTransform;
	
	//Hud
	GameObject Bottom;
	GameObject HudGrpResource;
	UISprite[] hudPointUISprite = new UISprite[5];
	Transform[] eggs = new Transform[5];
	
	//Hud chickPoint
	Camera ClearCamera;
	Camera camera2D;
	Transform UIRootBottomTransform;
	
	//Particle
	GameObject particle;
	#endregion
	
	void Awake () {
		//chickScoreHud = Resources.Load("HUD/LevelUpSprite", typeof(GameObject)) as GameObject;
		
		HudGrpResource = Resources.Load("HUD/HudGrp", typeof(GameObject)) as GameObject;
		Bottom = GameObject.Find("UI Root/Bottom");
		
		//Hud chickPoint
		ClearCamera = GameObject.Find("ClearCamera").GetComponent<Camera>();
		camera2D = GameObject.Find("2D Camera").GetComponent<Camera>();
		UIRootBottomTransform = GameObject.Find("UI Root/Bottom").transform;
		
		//Particle
		particle = Resources.Load("Particle/CFXM2_CartoonFight", typeof(GameObject)) as GameObject; 
	}
	
	
	void showChickScoreHud(int receiveChickScore, Transform receiveChickTransformFromBasketManager){	
		//receive
		int score =  receiveChickScore;
		Vector3 chickPos = receiveChickTransformFromBasketManager.transform.position;
		
		////ひよこがかごに落ちた位置Xを計算
		//ClearCamera空間でスクリーン座標に変換
		Vector3 chickPosInScreenPoint = ClearCamera.WorldToScreenPoint(receiveChickTransformFromBasketManager.position);
		
		//スクリーン座標を2D Camera空間に変換
		Vector3 chickPosInCamera2DWorld = camera2D.ScreenToWorldPoint(chickPosInScreenPoint);	
		//print ("chickPosInScreenPoint: " + chickPosInScreenPoint);
		//print ("chickPosInCamera2DWorld: " + chickPosInCamera2DWorld);
		
		//Instantiate
		GameObject createHudGrp = Instantiate(HudGrpResource, chickPosInCamera2DWorld, Quaternion.identity) as GameObject;
		createHudGrp.name = HudGrpResource.name;
		
		//parent position
		createHudGrp.transform.parent = UIRootBottomTransform;
		createHudGrp.transform.localScale = new Vector3(1f, 1f, 1f);
		createHudGrp.transform.setLocalPositionY(160f);
		createHudGrp.transform.setLocalPositionZ(0f);
		
		//particle position
		if(CharacterPool.objectPoolMode){
			//trouble
			var poolParticle = Pool.GetObjectPool(particle);
			GameObject pParticle = poolParticle.GetInstance();
			pParticle.SetActive(true);
			pParticle.transform.position = chickPosInCamera2DWorld;
			
			pParticle.transform.parent = UIRootBottomTransform;
			pParticle.transform.setLocalPositionY(160f);
			pParticle.transform.setLocalPositionZ(0f);
		}
		else{
			GameObject pParticle =  Instantiate(particle, chickPosInCamera2DWorld, Quaternion.identity) as GameObject;
			pParticle.name = particle.name;
			pParticle.transform.parent = UIRootBottomTransform;
			pParticle.transform.setLocalPositionY(160f);
			pParticle.transform.setLocalPositionZ(0f);
			pParticle.SetActive(true);
		}
		
		bool hoge = false;
		for(int i = 0; i < 5; i++){	
			hudPointUISprite[i] = createHudGrp.transform.FindChild("AnchorLeft" + i.ToString())
				.transform.FindChild("HudPoint" + i.ToString()).GetComponent<UISprite>();
			
			//get eggs transform
			if(i == 0){
				eggs[i] = hudPointUISprite[i].transform.GetChild(0).GetComponent<Transform>();
			}
			else{
				eggs[i] = createHudGrp.transform.FindChild("AnchorLeft" + i.ToString()).transform.GetChild(0).GetComponent<Transform>();
			}
			//positionX offset
			if(i == 0){
				if(score < 10){
					hudPointUISprite[i].transform.setLocalPositionX(20f);
				}
				else if(score < 100){
					hudPointUISprite[i].transform.setLocalPositionX(40f);
				}
				else if(score < 1000){
					hudPointUISprite[i].transform.setLocalPositionX(60f);
				}
				else if(score < 10000){
					hudPointUISprite[i].transform.setLocalPositionX(80f);
				}
				else if(score < 100000){
					hudPointUISprite[i].transform.setLocalPositionX(100f);
				}
			}
			
			if(hoge){
				hudPointUISprite[i].gameObject.SetActive(false);
				eggs[i].gameObject.SetActive(false);
			}
			
			if(score / 10 == 0){
				hudPointUISprite[i].spriteName = /*"numberFont/" + */score.ToString();
				hudPointUISprite[i].MakePixelPerfect();
//				print("spriteGameObjName: " + hudPointUISprite[i].gameObject.name);
				hoge = true;
				
				if(score == 1){
					
				}
			}
			else{
				string num =  (score % 10).ToString();
				hudPointUISprite[i].spriteName = /*"numberFont/" + */num;
				//hudPointUISprite[i].MakePixelPerfect();
				score = score / 10;
			}
			//hudPointUISprite[i].topAnchor
			hudPointUISprite[i].MakePixelPerfect();
		}
			
		//animation
		
		iTween.MoveTo(createHudGrp, 
			iTween.Hash("y", createHudGrp.transform.localPosition.y +  80f, 
								"islocal", true, 
								"time", 1.0f,
								"easetype", "easeOutCirc",
								"oncompletetarget", createHudGrp, 
								"oncomplete", "destroythisGameObject"
								));	
	}
	
	
	void Update () {
	
	}
	
	#region Delegate
	void OnEnable(){
		ScoreManager._hudChickPointIsStart += showChickScoreHud;
	}
	void OnDisable(){
		UnSubscribeEvent();
	}
	void OnDestroy(){
		UnSubscribeEvent();
	}
	void UnSubscribeEvent(){
		ScoreManager._hudChickPointIsStart -= showChickScoreHud;
	}
	#endregion
	
}
