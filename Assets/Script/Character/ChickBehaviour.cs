using UnityEngine;
using System.Collections;
//using UnityEditor;

public class ChickBehaviour : MonoBehaviour{
	
	
	public static int indexInt;
	public PhysicMaterial ChickFootMaterial;
	public PhysicMaterial ChickHeadMaterial;
	BoxCollider boxCollider;
	CapsuleCollider capsuleCollider; 
	ParticleSystem scoreParticles;
	bool lastActionFlag = false;
	bool falldownFlag = false;
	bool sideFalldownFlag = false;
	bool chickTouchDozerBaseFlag = false;
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
	
	void Start (){
		anim = GetComponent<Animation>();
		boxCollider = GetComponentInChildren<BoxCollider>();
		capsuleCollider = GetComponentInChildren<CapsuleCollider>();
		ChickFootMaterial = (PhysicMaterial)Resources.Load("PhysicMaterials/ChickFoot");
		ChickHeadMaterial = (PhysicMaterial)Resources.Load("PhysicMaterials/ChickHead");
		
		rigidbody.centerOfMass = new Vector3(0f, -0.5f, -0.3f);
		scoreParticles = gameObject.transform.FindChild("Particle").GetComponent<ParticleSystem>(); 
		
		GameObject childHasCapsuleCollider = GetComponentInChildren<CapsuleCollider>().gameObject;
		childHasCapsuleCollider.GetComponent<CapsuleCollider>().sharedMaterial = ChickHeadMaterial;
		
		Basket = GameObject.Find("Basket");
		int layerNo = LayerMask.NameToLayer("BG");
		EffectScore = Resources.Load("HUD/EffectScore") as GameObject;
	}
	
	
	void OnCollisionEnter(Collision collision){
		if(chickTouchDozerBaseFlag){return;}
		
		if(collision.gameObject.name == "GameObject"){
			chickTouchDozerBaseFlag = true;
			_touchDozerBaseFlag(); //delegate Touch DozerManager.cs
		}
		
		
		
	}
	
	
	public static  int hitBasketCount = 0;
	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.name == "CountChickInBasket"){
			hitBasketCount += 1;
			
			if(boxCollider != null ||capsuleCollider != null  ){
				boxCollider.enabled = false;
				capsuleCollider.enabled = false;
			}
			
			
			goToBasket();
			//Debug.Break();
		}
	}
	
	public void goToBasket(){
		if(scoreParticles != null){scoreParticles.Play();}
		if(transform.parent != null){transform.parent = Basket.transform;}
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
										"islocal", true,
										"y", transform.position.y - 0.3f, 
										"time", 0.5f, 
										"speed", 1.0f, 
										"easetype", "linear"
						)); //Y移動
		lastActionFlag = true;
		//Debug.Break();
	}
	
	void destroy(){
		Destroy(this.gameObject);
	}
	
	void Update (){
		//print ("hitBasketCount: " + hitBasketCount +  "    createChickCount: " + GameControlManagerScript.createChickCount);
		
		//falldown chick at edge of cliff
		if(transform.localPosition.z > 4.9f){
			Debug.DrawLine(this.gameObject.transform.localPosition, new Vector3(transform.localPosition.x, transform.localPosition.y + 4f, transform.localPosition.z), Color.white);
			if(!falldownFlag){
				boxCollider.enabled = false;
			
				falldownFlag = true;
			}
		}
		//chick fall down right
		if(this.transform.localPosition.x < -7.7f){
			Debug.DrawLine(this.gameObject.transform.localPosition, new Vector3(transform.localPosition.x, transform.localPosition.y + 4f, transform.localPosition.z), Color.red);
			//print("this.gameObject.transform.position :" + this.gameObject.transform.position);
			//Debug.Break();
			if(!falldownFlag){
				boxCollider.enabled = false;
				//print ("rigidbody.centerOfMass: " + rigidbody.centerOfMass );//(0.0, -0.5, -0.3)
				//this.rigidbody.centerOfMass = new Vector3(this.rigidbody.centerOfMass.x - 7f, this.rigidbody.centerOfMass.y +2f, this.rigidbody.centerOfMass.z);				
				falldownFlag = true;
			}
		}
		//chick fall down left
		else if(this.transform.position.x > -0.6f){
			Debug.DrawLine(this.gameObject.transform.localPosition, new Vector3(transform.localPosition.x, transform.localPosition.y + 4f, transform.localPosition.z), Color.yellow);
			//print("this.gameObject.transform.position :" + this.gameObject.transform.position);
			if(!falldownFlag){
				boxCollider.enabled = false;
				//this.rigidbody.centerOfMass = new Vector3(this.rigidbody.centerOfMass.x + 7f, this.rigidbody.centerOfMass.y +2f, this.rigidbody.centerOfMass.z);				
				falldownFlag = true;
			}
		}
		
		//goToBasket();
		
		if(rigidbody.IsSleeping()) { rigidbody.WakeUp(); }
		if(transform.localPosition.y < -15f){ Destroy(this.gameObject); }	
	}
}
