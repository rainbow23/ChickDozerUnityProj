using UnityEngine;
using System.Collections;

public class EffectManager : MonoBehaviour {
	Camera ClearCamera;
	Camera camera2D;

	GameObject scoreEffectObject;
	GameObject effectObj;

	Transform UIRootBottomTransform;
	public Transform effectPlace;
	Pool scorePool;
	Pool effectPool;
	void Awake()
	{
		scoreEffectObject = Resources.Load("HUD/CharScoreGrp", typeof(GameObject)) as GameObject;
		UIRootBottomTransform = GameObject.Find("UI Root/Bottom").transform;
		ClearCamera = GameObject.Find("ClearCamera").GetComponent<Camera>();
		camera2D = GameObject.Find("2D Camera").GetComponent<Camera>();
		effectObj = Resources.Load("HUD/CFXM2_PickupHeart3", typeof(GameObject)) as GameObject;
	}

	void Start () 
	{
		scorePool = Pool.GetObjectPool(scoreEffectObject);
		effectPool = Pool.GetObjectPool(effectObj);
		scorePool.maxCount = 20;
		effectPool.maxCount = 20;
	}

	public void ShowScoreEffect(int charScore, Vector3 charPos)
	{
		//Instantiate
		GameObject obj = CreateScore(charPos);

		//show bitmap score
		var bitmapNumManager = obj.GetComponent<BitmapNumberManager>();
		bitmapNumManager.UpdateNumber(charScore);
		//create Effect effect animation
		var effectAnimation = obj.GetComponent<EffectAnimation>();
		effectAnimation.MoveUp();

		CreateParticle(charPos);
	}
	
	private GameObject CreateScore(Vector3 Pos)
	{
		//pool.GetInstance
		GameObject scoreObj = scorePool.GetInstance(effectPlace);
		scoreObj.transform.position = Convert2DCameraPos(Pos);
		scoreObj.transform.localScale = new Vector3(1f, 1f, 1f);
		scoreObj.name = scoreEffectObject.name;
		//Debug.Log("Convert2DCameraPos(Pos): " + Convert2DCameraPos(Pos));
		return scoreObj;
	}

	private void CreateParticle(Vector3 position)
	{
		GameObject particle = effectPool.GetInstance(effectPlace);
		particle.transform.position = Convert2DCameraPos(position);

	}

	Vector3 Convert2DCameraPos(Vector3 charPos)
	{
		////ひよこがかごに落ちた位置Xを計算
		Vector3 screenPoint = ClearCamera.WorldToScreenPoint(charPos); //ClearCamera空間でスクリーン座標に変換
		Vector3 camera2DPos = camera2D.ScreenToWorldPoint(screenPoint);	//スクリーン座標を2D Camera空間に変換
		return new Vector3(camera2DPos.x, camera2DPos.y, 0f);
	}


	void Update () 
	{
	
	}
}
