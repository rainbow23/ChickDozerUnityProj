using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CreateCharManager : MonoBehaviour{

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

	private GameObject[] AllChicks;
	public static Dictionary<int, GameObject> ResourcesLoadChickDic = new Dictionary<int, GameObject>();

	GameController gameController;

	void Awake()
	{
			//int index = int.Parse(chick.name.Substring(chick.name.Length -3));
			//ResourcesLoadChickDic.Add(index, chick);
		gameController = GameObject.Find("GameController").GetComponent<GameController>();
		LoadCharFromResources();
		LoadCharDataFromDisc();
	}

	void Start()
	{
		//Create(GameController.touchPos.Value);
		gameController.touchPos.AddListener(Create);
	}

	void Create(Vector3 touchPos)
	{
		//とりあえずレベル１だけしか作らない
		GameObject obj = Instantiate(AllChicks[0], touchPos, Quaternion.Euler(-30f, 0f, 0f)) as GameObject;
		obj.name = AllChicks[0].name;
		obj.SetActive(true);
	}

	void LoadCharFromResources()
	{
		if(AllChicks == null){
				AllChicks = Resources.LoadAll("Chicks",typeof(GameObject))
				.Cast<GameObject>()
				.ToArray();
			
			for (int i = 0; i < AllChicks.Length; i++) 
			{	
				string n = AllChicks[i].name;
				int index = int.Parse(n.Substring(n.Length - 3));
				ResourcesLoadChickDic.Add(index, AllChicks[i]);
			}
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
		if(ResourcesLoadChickDic.ContainsKey(kind))
		{
			GameObject obj = Instantiate(ResourcesLoadChickDic[kind], pos, Quaternion.Euler(rot)) as GameObject;
			obj.name = AllChicks[kind].name;
			if(!isActiveBottomCollider)
			{
				obj.GetComponentInChildren<BoxCollider>().enabled = false;
			}
		}
		else{ Debug.Log(kind + " key isn't exist");}
	}

	void OnDestroy ()
	{
		saveCharacterData.RemoveAllListeners();
	}
}
