using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasketManager : MonoBehaviour {
	
	public delegate void basketHandler(int chickNum);
	public static event basketHandler _hitChickBasket;
	
	public delegate void basketHandler2(int chickNum, Transform hitPosition);
	public static event basketHandler2 _hitChickBasket2;
	
	void Start (){
		
	}	
	/*
	void OnCollisionEnter(Collision collision) {
		print ("collision.gameObject.name: " +  collision.gameObject.name);
	//	if(collision.gameObject.name == "BottomCollider" || collision.gameObject.name == "CapsuleCollider"){
			if(this.gameObject.name == "CountChickInBasket"){
				Collider hitCollider =  collision.gameObject.GetComponent<Collider>();
				hitCollider.enabled = false;
				
				Transform topParent = searchParent(collision.transform);
				
				string index = topParent.gameObject.name.Substring(topParent.gameObject.name.Length - 3);
				int num =int.Parse(index);
				//print ("num:" + num);
				_hitChickBasket(num);//Delegate
				_hitChickBasket2(num, topParent); //Delegate : HUD表示に使う
				print ("hit");
			}
	//	}
	}
	*/
	
	
	bool once = false;
	void OnTriggerEnter(Collider collider){
		//かごに触れたひよこ種類をDelegate
		
		if(collider.gameObject.name == "BottomCollider" || collider.gameObject.name == "CapsuleCollider"){
			//二度当たるのを防ぐためオフにする
			collider.transform.parent.GetChild(0).collider.enabled = false;
			collider.transform.parent.GetChild(1).collider.enabled = false;
				if(this.gameObject.name == "CountChickInBasket"){
					Transform topParent = searchParent(collider.transform);
					
					string index = topParent.gameObject.name.Substring(topParent.gameObject.name.Length - 3);
					int num =int.Parse(index);
					
					//復元場所のひよこがカゴに触れた状態ならば
					if(timer < 1.0f){
						return;
					}
					else{
					_hitChickBasket(num);//Delegate
					_hitChickBasket2(num, topParent); //Delegate : HUD表示に使う
					}
					//print ("num:" + num);
				}
		}
	}
	
	//かごに触れている間シーン遷移した場合、Triggerの中で復元されるため
	void OnTriggerStay (Collider collider){
		//かごに触れたひよこ種類をDelegate
		if(collider.gameObject.name == "BottomCollider" || collider.gameObject.name == "CapsuleCollider"){
			if(this.gameObject.name == "CountChickInBasket"){
				print ("hit");
				Collider hitCollider =  collider.gameObject.GetComponent<Collider>();
				hitCollider.enabled = false;
				Transform topParent = searchParent(collider.transform);
				
				
				
				
				string index = topParent.gameObject.name.Substring(topParent.gameObject.name.Length - 3);
				int num =int.Parse(index);
				//print ("num:" + num);
				//復元場所のひよこがカゴに触れた状態ならば
				if(timer < 1.0f){
					return;
				}
				else{
				_hitChickBasket(num);//Delegate
				_hitChickBasket2(num, topParent); //Delegate : HUD表示に使う
				}
				
				print ("num:" + num);
			}
		}
	}
	
	
	Transform searchParent(Transform obj){
		if(obj.parent ==null ){
			return obj;	
		}
		else{
			return searchParent(obj.parent);
		}
	}
	
	
	/*
	void OnCollisionEnter(Collision collision){
		if( collision.gameObject.name.Contains("Egg") == false){
			string index = collision.gameObject.name.Substring(collision.gameObject.name.Length - 3);
			indexInt = int.Parse(index);
			//print ("indexInt: " + indexInt);
			//かごに触れたひよこの種類を判定 0-1 GameControlManager.cs
			gameData.setChickCollectionFlag(indexInt);
		}
	}
	*/
	float timer = 0;
	void Update (){
		if(timer > 2f){return;}
		timer += Time.deltaTime;

	}
	
}
