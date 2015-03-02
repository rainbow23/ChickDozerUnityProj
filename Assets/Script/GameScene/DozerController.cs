using UnityEngine;
using System.Collections;

public class DozerController : MonoBehaviour {
		bool chickTouchDozerBase = false;
		bool addChickInFeverTimeFlag = false;
		public delegate void addChickInFeverTime(bool addChick);
		public static event addChickInFeverTime  _addChickInFeverTime;
		int once = 0;
		
		void Start () 
		{

		}
		
		public void MoveDozer(){
			
			if(chickTouchDozerBase == false){
				iTween.MoveTo( this.gameObject, 
				              iTween.Hash("z", 4f, 
				            "time", 3.5f, 
				            "easeType", "linear", 
				            "loopType", "pingPong", 
				            "delay", 0));
			}
			chickTouchDozerBase =true;
			
		}
		
		void feverTiming(){
			addChickInFeverTimeFlag = true;
		}
		
		void feverTimingEnd(){
			addChickInFeverTimeFlag = false;
		}
		
		bool onceAdd = false;
		float dozerMoveBeforePosZ ;
		
		void Update (){
			if(addChickInFeverTimeFlag){
				//print ("addChickInFeverTimeFlag:" + addChickInFeverTimeFlag);
				//if pull back this.gameObject
				if(dozerMoveBeforePosZ > this.gameObject.transform.localPosition.z){
					if(this.gameObject.transform.localPosition.z <= -7.0f){
						if(onceAdd == false){
							_addChickInFeverTime(true);
							onceAdd =true;
						}
					}
				}
				else{
					if(this.gameObject.transform.localPosition.z > -7.0f){
						onceAdd = false;
					}
				}
			}
			
			dozerMoveBeforePosZ = this.gameObject.transform.localPosition.z;
			//print ("beforePosZ:" +dozerMoveBeforePosZ);	
		}
		
		void OnEnable(){
			UI_FeverTime._feverTimeTimingEnd += feverTimingEnd;
		}
		
		void OnDisable(){
			UnSubscribeEvent();
		}
		
		void OnDestroy(){
			UnSubscribeEvent();
		}
		
		void UnSubscribeEvent(){
			UI_FeverTime._feverTimeTimingEnd -= feverTimingEnd;
		}
		
		
		/*
	void Awake(){}
	void Start(){}
	
	bool chickTouchDozer = false;
	
	void OnTriggerEnter(Collider hitChick){
		Debug.Break();
		print ("hitChick: " + hitChick.gameObject.name);
		if(chickTouchDozer == true){return;}
		
		if(hitChick.gameObject.name.Contains("BottomCollider")){
			moveDozer();
			chickTouchDozer = true;
		}
	}
	
	void moveDozer(){
		if(transform.parent.name == "this.gameObject"){}
		iTween.MoveTo( transform.parent.gameObject, 
				iTween.Hash("z", 4f, 
									"time", 3.5f, 
									"easeType", "linear", 
									"loopType", "pingPong", 
									"delay", 0));
	}
	
	
	void Update(){}
	
	*/
}
