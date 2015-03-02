using UnityEngine;
using System.Collections;

public class EggManager : MonoBehaviour{
	public delegate void feverTimeTiming();
    public static event feverTimeTiming  _feverTimeTiming;
	bool touchDozerBaseFlag = false;
	bool lastAction = false;
		
	void Start (){
		rigidbody.centerOfMass = new Vector3(0, -1, 0);
	}
	
	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.tag =="DozerBase"){
			touchDozerBaseFlag = true;
		}
	}
	
	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.name == "CountChickInBasket"){
			lastAction = true;
			print ("touchCountChickInBasket: ");
			rigidbody.isKinematic = true;
		}
	}
	
	float lifeTimer = 0f;
	public void goToBasket(){
		if(lastAction == true){
			lifeTimer = 0f;
			lifeTimer += Time.deltaTime;
			float scaleX = Mathf.Lerp(transform.localScale.x, 0f, Time.deltaTime);
			float scaleY = Mathf.Lerp(transform.localScale.y, 0f, Time.deltaTime);
			float scaleZ = Mathf.Lerp(transform.localScale.z, 0f, Time.deltaTime);
			transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
			
			if(scaleX < 0.5f){
				//delegate: FeaveTime.cs
				//method: feverTime
				_feverTimeTiming();
				Destroy(this.gameObject);
				//pointUpFlag
				//_chickDestroyFlag();
				lastAction = false;
			}
		}
	}
	public void hoge(){}
	
	void Update (){
		goToBasket();	
		if(rigidbody.IsSleeping()) { rigidbody.WakeUp(); }
		if(transform.localPosition.y < -5f) { Destroy(this.gameObject); }
	}
}
