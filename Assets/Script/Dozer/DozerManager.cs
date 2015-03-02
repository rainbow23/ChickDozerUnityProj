using UnityEngine;
using System.Collections;

public class DozerManager : MonoBehaviour {
	
	public GameObject DozerMove;
	bool chickTouchDozerBase = false;
	bool addChickInFeverTimeFlag = false;
	public delegate void addChickInFeverTime(bool addChick);
    public static event addChickInFeverTime  _addChickInFeverTime;
	int once = 0;
	
	void Start () 
	{
		DozerMove = GameObject.Find("DozerMove");
	}
	
	void MoveDozer(){
		
		if(chickTouchDozerBase == false){
			iTween.MoveTo( DozerMove, 
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
			//if pull back DozerMove
			if(dozerMoveBeforePosZ > DozerMove.transform.localPosition.z){
				if(DozerMove.transform.localPosition.z <= -7.0f){
					if(onceAdd == false){
						_addChickInFeverTime(true);
						onceAdd =true;
					}
				}
			}
			else{
				if(DozerMove.transform.localPosition.z > -7.0f){
					onceAdd = false;
				}
			}
		}
		
		dozerMoveBeforePosZ = DozerMove.transform.localPosition.z;
		//print ("beforePosZ:" +dozerMoveBeforePosZ);	
	}
	
	void OnEnable(){
		ChickBehaviour._touchDozerBaseFlag += MoveDozer;
		EggManager._feverTimeTiming += feverTiming;
		UI_FeverTime._feverTimeTimingEnd += feverTimingEnd;
	}
	
	void OnDisable(){
		ChickBehaviour._touchDozerBaseFlag -= MoveDozer;
		EggManager._feverTimeTiming -= feverTiming;
		UI_FeverTime._feverTimeTimingEnd -= feverTimingEnd;
	}
	
	void OnDestroy(){
		ChickBehaviour._touchDozerBaseFlag -= MoveDozer;
		EggManager._feverTimeTiming -= feverTiming;
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
		if(transform.parent.name == "DozerMove"){}
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
