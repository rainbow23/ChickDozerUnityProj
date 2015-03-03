using UnityEngine;
using System.Collections;
//using UnityEditor;

public class ChickManager : CharacterManager{
	#region variable
	public static int indexInt;
	public PhysicMaterial ChickFootMaterial;
	//public PhysicMaterial ChickHeadMaterial;
	BoxCollider boxCollider;
	CapsuleCollider capsuleCollider; 
	ParticleSystem scoreParticles;
	bool lastActionFlag = false;
	bool falldownFlag = false;
	bool sideFalldownFlag = false;
	bool touchDozer = false;
	public static bool sceneLoadFromCollection = false;
	private float timer = 0f;
	Animation anim;
	
	public delegate void touchDozerBaseFlag();
    public static event touchDozerBaseFlag  _touchDozerBaseFlag;
	
	/*
	public delegate void onHitChickByBasket(string chickName, Transform posChick);
    public static event onHitChickByBasket  _onHitChickByBasket;
	
	public delegate void chickHitExceptBasket(string chickName, Transform posChick);
    public static event chickHitExceptBasket  _chickHitExceptBasket;
	*/
	GameObject Basket;
	GameObject EffectScore;
	#endregion
	
	void Awake(){		
		anim = GetComponent<Animation>();
		boxCollider = GetComponentInChildren<BoxCollider>();
		capsuleCollider = GetComponentInChildren<CapsuleCollider>();
		ChickFootMaterial = (PhysicMaterial)Resources.Load("PhysicMaterials/ChickFoot");
		//ChickHeadMaterial = (PhysicMaterial)Resources.Load("PhysicMaterials/ChickHead");
		//GetComponent<Animation>().animatePhysics = true;
	}
	
	void Start (){
		ChickFootMaterial.dynamicFriction = 0.10f;
		ChickFootMaterial.staticFriction =0.1f;
		
		rigidbody.angularDrag = 60f;
		rigidbody.centerOfMass = new Vector3(0f, -0.5f, -0.7f);
		scoreParticles = gameObject.transform.FindChild("Particle").GetComponent<ParticleSystem>(); 
		
		//GameObject childHasCapsuleCollider = GetComponentInChildren<CapsuleCollider>().gameObject;
		//childHasCapsuleCollider.GetComponent<CapsuleCollider>().sharedMaterial = ChickHeadMaterial;
		
		Basket = GameObject.Find("Basket");
		int layerNo = LayerMask.NameToLayer("BG");
		EffectScore = Resources.Load("HUD/EffectScore") as GameObject;
	}
	
	Vector3 velocityValue = new Vector3(0f, 0f, 0f);
	void initialize(){
		timer = 0f;
		touchDozer = false;
		gameObject.transform.setEulerAngles(-30f, 0f, 0f);
		boxCollider.enabled = true;
		capsuleCollider.enabled = true;
		gameObject.transform.localScale = new Vector3(1f,1f,1f);
		rigidbody.isKinematic = false;
		rigidbody.velocity = new Vector3(0f, 0f, 0f);
		
		iTween.Stop(this.gameObject);
		gameObject.animation.enabled = true;
		scoreParticles.Stop();
	}	
	
	void OnCollisionEnter(Collision collision){
		//print ("collision.gameObject.name: " + collision.gameObject.name);
		if(	collision.gameObject.tag == "Dozer" && !touchDozer)
		{
			touchDozer = true;
			_touchDozerBaseFlag(); //delegate Touch DozerManager.cs
		}
	}
	
	//public static  int hitBasketCount = 0;
	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.tag == "Basket")
		{
			//二度当たるのを防ぐためオフにする
			DisableCollider();
			GameController.CharHitGoalNum.Value += 1;
			goToBasket();
		}
	}
	
	private void goToBasket(){
		//if(scoreParticles != null){scoreParticles.Play();}
		if(rigidbody != null){rigidbody.isKinematic = true;}
		if(capsuleCollider != null){capsuleCollider.enabled = false;}
		//newUVひよこは iTween Scaleをするには Animation Componentをoffにする。
		if(anim != null){ anim.enabled = false;}
		
		iTween.ScaleTo(gameObject, 
						iTween.Hash("x", 0.5f, 
											"y", 0.5f, 
											"z", 0.5f, 
											"speed", 1.4f, 
											"time", 0.5f, 
											"oncomplete", "destroy", 
											"oncompletetarget", gameObject,
											"easetype", "linear"
						)); 
		iTween.MoveTo(gameObject, 
					iTween.Hash(//"z", transform.position.z + 0.8f, 
										//"islocal", true,
										"y", - 2.8f, 
										"time", 0.49f, 
										"speed", 1.0f, 
										"easetype", "linear"
					)); //Y移動
		lastActionFlag = true;
		//Debug.Break();
	}
	
	/* not use
	void moveChick(){
		rigidbody.isKinematic = false;
	}
	*/
	
	void destroy(){
		if(CharacterPool.objectPoolMode){
			initialize();
			lastActionFlag = false;
			gameObject.SetActive(false);
		}
		else{
			Destroy(this.gameObject);
		}
	}
	

	
	void Update (){
		//test
		//rigidbody.centerOfMass
		Debug.DrawLine(rigidbody.centerOfMass, new Vector3(rigidbody.centerOfMass.x, rigidbody.centerOfMass.y + 1f, rigidbody.centerOfMass.z), Color.yellow);
		//print ("rigidbody.centerOfMass: " + rigidbody.centerOfMass);
		
		// bottom collider is disable if chick has not landed after 3 second time passes from game start   
		timer += Time.deltaTime;
		if(timer > 3.0f){
			if(touchDozer == false && boxCollider.enabled == true){
				boxCollider.enabled = false;	
			}
		}
		
		//falldown chick at edge of cliff
		if(transform.position.z > 2.3f){//4.9f){
			Debug.DrawLine(this.gameObject.transform.position, new Vector3(transform.position.x, transform.position.y + 4f, transform.position.z), Color.white);
			boxCollider.enabled = false;
		}
		//chick fall down right
		if(this.transform.position.x < -7.65f){
			Debug.DrawLine(this.gameObject.transform.position, new Vector3(transform.position.x, transform.position.y + 4f, transform.position.z), Color.red);
			//print("this.gameObject.transform.position :" + this.gameObject.transform.position);
		
			boxCollider.enabled = false;
			rigidbody.centerOfMass = new Vector3(-0.3f, 0.2f, -0.7f);
			Debug.DrawLine(rigidbody.centerOfMass, new Vector3(rigidbody.centerOfMass.x, rigidbody.centerOfMass.y + 1f, rigidbody.centerOfMass.z), Color.green);
			//print ("rigidbody.centerOfMass: " + rigidbody.centerOfMass );//(0.0, -0.5, -0.3)
			//this.rigidbody.centerOfMass = new Vector3(this.rigidbody.centerOfMass.x - 7f, this.rigidbody.centerOfMass.y +2f, this.rigidbody.centerOfMass.z);				
		}
		//chick fall down left
		if(this.transform.position.x > -0.8f){
			Debug.DrawLine(this.gameObject.transform.position, new Vector3(transform.position.x, transform.position.y + 4f, transform.position.z), Color.yellow);
			//print("this.gameObject.transform.position :" + this.gameObject.transform.position);
			boxCollider.enabled = false;
			rigidbody.centerOfMass = new Vector3(0.3f, 0.2f, -0.7f);
			Debug.DrawLine(rigidbody.centerOfMass, new Vector3(rigidbody.centerOfMass.x, rigidbody.centerOfMass.y + 1f, rigidbody.centerOfMass.z), Color.green);
			//this.rigidbody.centerOfMass = new Vector3(this.rigidbody.centerOfMass.x + 7f, this.rigidbody.centerOfMass.y +2f, this.rigidbody.centerOfMass.z);				
		}
		
		//goToBasket();
		
		if(rigidbody.IsSleeping()) { rigidbody.WakeUp(); }
		if(transform.position.y < -15f){ 
			if(CharacterPool.objectPoolMode){
				initialize();
				gameObject.SetActive(false);
			}
			else{
				Destroy(this.gameObject); 
			}
		}
	}
	
	void OnEnable(){
		//UI_GameScene._isMoveChick += moveChick;
	}
	void OnDisable(){
		UnSubscribeEvent();
	}
	void OnDestroy(){
		UnSubscribeEvent();
	}
	void UnSubscribeEvent(){
		//UI_GameScene._isMoveChick -= moveChick;
	}
}
