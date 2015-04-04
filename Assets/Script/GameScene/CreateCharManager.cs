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
		//同じレベルのひよこだけだす
		GameObject levelChar = resourcesLoadChickDic[GameController.Level - 1];
		GameObject obj = Instantiate(levelChar, touchPos, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
		obj.name = levelChar.name;
		obj.SetActive(true);
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
}
