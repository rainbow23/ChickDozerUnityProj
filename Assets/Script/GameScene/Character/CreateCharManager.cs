using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CreateCharManager : SingletonMonoBehaviour<CreateCharManager>{

	private const string CHARPOSKEY = "CharPosKey";
	private const string CHARROTKEY = "CharRotKey";
	private const string CHARKINDKEY = "CharKindKey";
	private const string CHARBOTTOMCOLLIDERKEY = "CharBottomColliderKey";
	[System.NonSerialized]
	public List <Vector3> charPosList = new List<Vector3>();
	[System.NonSerialized]
	public List <Vector3> charRotList =  new List<Vector3>();
	[System.NonSerialized]
	public List <int> charKindList =  new List<int>();
	[System.NonSerialized]
	public List <bool> isActiveBottomColliderOfCharList = new List<bool>();
	[HideInInspector]
	public  UnityEngine.Events.UnityEvent  saveCharacterData;	
	public Dictionary<int, GameObject> resourcesLoadChickDic = new Dictionary<int, GameObject>();

	public Transform poolCharPlace;


	void Awake()
	{
		//PlayerPrefs.DeleteAll();
			//int index = int.Parse(chick.name.Substring(chick.name.Length -3));
			//resourcesLoadChickDic.Add(index, chick);
		LoadCharFromResources();
		LoadCharDataFromDisc();
	}

	void Start()
	{

	}

	public void Create(Vector3 touchPos)
	{
		int levelRange;
		int odds;
		if(DATA.Level == 1) {
			levelRange = 1;
		}	

		int random = Random.Range(1, 101);
		if(random > 3){
			levelRange = DATA.Level - 1;
			odds = GetRandomByWeight(oddsArray(levelRange));
		}
		else{//Show curr level chick only in 3%
			odds = DATA.Level;
		}

		Debug.Log("odds: " + odds);

		Vector3 bornPos = new Vector3(touchPos.x, 5.7f, -5.2f);// define new pos
		string storeCharName = resourcesLoadChickDic[odds].name;

		var pool = Pool.GetObjectPool(resourcesLoadChickDic[odds]);
		//GameObject randomChar = resourcesLoadChickDic[odds];

		GameObject obj = pool.GetInstance(poolCharPlace);
		obj.transform.position = bornPos;
		obj.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

		//GameObject obj = Instantiate(randomChar, pos, Quaternion.Euler(0f, 0f, 0f)) as GameObject;

		obj.name = storeCharName;
		obj.SetActive(true);
	}

	private int[] oddsArray(int hoge){
		int[] level = new int[hoge];
		Stack<int>  denominator = new Stack<int>();
		/*
		 レベルが１上がるごとに新しいひよこを作れる
		例
		（レベル４）
		ひよこ種類：１,    ２,     ３,    ４, 
		確率：       3/6,  2/6,  1/6,   3%
		（レベル５）
		ひよこ種類：１,      ２,      ３,     ４,   ５
		確率：     4/10, 3/10, 2/10, 1/10,   3%
		*/

		for (int i = 0; i < level.Length; i++) {
			int num = i + 1;
			denominator.Push(num);
			//Debug.Log("i:push " + num);
		}
		
		for (int i = 0; i < level.Length; i++) {
			level[i] = denominator.Pop();
			//Debug.Log("level[" + i +  "]: " + level[i]);
		}
		return level;// level;
	}

	

	void LoadCharFromResources()
	{
		var AllChar = Resources.LoadAll("Chicks",typeof(GameObject))
				.Cast<GameObject>()
				.OrderBy(t =>{
					string numString =  t.gameObject.name.Substring(t.gameObject.name.Length - 2);
					int index = int.Parse(numString);
					return index;
				})
				.ToArray();

		int count  = 0;
		foreach(var character in AllChar){
			resourcesLoadChickDic.Add(count, character);
			count += 1;
		}
	}

	private void LoadCharDataFromDisc()
	{
		Vector3[] charPos = PlayerPrefsX.GetVector3Array(CHARPOSKEY);
		Vector3[] charRot = PlayerPrefsX.GetVector3Array(CHARROTKEY);
		int[] charKind = PlayerPrefsX.GetIntArray(CHARKINDKEY);
		bool[] charBottomCollider =  PlayerPrefsX.GetBoolArray(CHARBOTTOMCOLLIDERKEY);

		//restore chick
		for(int i =0; i < charPos.Length; i++)
		{
			RestoreChick(charPos[i], charRot[i], charKind[i], charBottomCollider[i]);
		}
		Debug.Log("LoadCharDataFromDisc");
	}

	public void SaveCharDataToDisc(){
		//重複保存を避ける
		charPosList.Clear();
		charRotList.Clear();
		charKindList.Clear();
		isActiveBottomColliderOfCharList.Clear();

		saveCharacterData.Invoke(); //delegate to all chicks in scene.

		Vector3[] charPos = charPosList.ToArray();
		Vector3[] charRot = charRotList.ToArray();
		int[] charKind = charKindList.ToArray();
		bool[] charBottomCollider = isActiveBottomColliderOfCharList.ToArray();

		PlayerPrefsX.SetVector3Array(CHARPOSKEY, charPos);
		PlayerPrefsX.SetVector3Array(CHARROTKEY, charRot);
		PlayerPrefsX.SetIntArray(CHARKINDKEY, charKind);
		PlayerPrefsX.SetBoolArray (CHARBOTTOMCOLLIDERKEY, charBottomCollider);
	}
	
	public void RestoreChick(Vector3  pos, Vector3  rot, int kind, bool isActiveBottomCollider) 
	{
		if(resourcesLoadChickDic.ContainsKey(kind))
		{
			GameObject obj = Instantiate(resourcesLoadChickDic[kind], pos, Quaternion.Euler(rot)) as GameObject;
			obj.name = resourcesLoadChickDic[kind].name;

			if(!isActiveBottomCollider)
				obj.GetComponentInChildren<BoxCollider>().enabled = false;
		}
		else{ Debug.Log(kind + " key isn't exist");}
	}

	void OnDestroy ()
	{
		saveCharacterData.RemoveAllListeners();
	}

	private int GetRandomByWeight(int[] hoge){
		int result = 0;
		int sum = 0;
		
		for (int i = 0; i < hoge.Length ; i++){
			sum += hoge[i];
			//print ("i" + i);
		}
		
		int t = Random.Range(1, sum + 1);
		for(int i = 0 ; i < hoge.Length ; i++){
			if(t <= hoge[i]){
				result = i;
				break;
			}
			t -= hoge[i];
		}
		//Debug.Log("t:" + t);
		return result; //
	}
}
