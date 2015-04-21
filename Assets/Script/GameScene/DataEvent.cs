using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DataEvent :MonoBehaviour
{
	private const string CharPosKey = "CharPosKey";
	private const string CharRotKey = "CharRotKey";
	private const string CharKindKey = "CharKindKey";
	private const string CharBottomColliderKey = "CharBottomColliderKey";
	private CreateCharManager createCharManager;

	void Awake()
	{
		createCharManager = GameObject.Find("CreateCharManager").GetComponent<CreateCharManager>();
	}

	private void SaveChar()
	{
		Transform poolPlace = createCharManager.poolCharPlace.transform;
		List<GameObject> objList = new List<GameObject>();
		for (int i = 0; i < poolPlace.childCount; i++) {
			objList.Add(poolPlace.GetChild(i).gameObject);
		}

		var activeObj = from item in objList
						where item.activeSelf == true
						select item;

		CharInfo(activeObj.ToArray());
	}

	private void CharInfo(GameObject[] objList)
	{
		List<Vector3> posList = new List<Vector3>();
		List<Vector3> rotList = new List<Vector3>();
		List<int> charKindList = new List<int>();
		List<bool> charBottomColliderList = new List<bool>();

		foreach (var obj in objList ) {
			ChickManager chickManager = obj.GetComponent<ChickManager>();

			posList.Add (obj.transform.position);
			rotList.Add(obj.transform.rotation.eulerAngles);
			charKindList.Add(chickManager.thisCharNum);
			charBottomColliderList.Add(chickManager.boxCollider.enabled);
		}
		//position
		PlayerPrefsX.SetVector3Array(CharPosKey, posList.ToArray());
		//rotation
		PlayerPrefsX.SetVector3Array(CharRotKey, rotList.ToArray());
		//kind
		PlayerPrefsX.SetIntArray(CharKindKey, charKindList.ToArray());
		//bottomCollider
		PlayerPrefsX.SetBoolArray (CharBottomColliderKey, charBottomColliderList.ToArray());
		PlayerPrefs.Save();
	}



	public void SaveGameData()
	{
		//Debug.Log("SaveGameData");
		PlayerPrefs.SetInt( DATA.SCOREKEY, DATA.Score);
		PlayerPrefs.SetInt( DATA.LEVELKEY,DATA.Level); 
		PlayerPrefs.SetInt( DATA.POINTKEY, DATA.Point); 
		PlayerPrefs.SetInt( DATA.SAVEDFIRSTRUNKEY, System.Convert.ToInt32(DATA.CheckFirstRun)); 
		PlayerPrefs.SetInt( DATA.NEXTLEVELPERCENTAGEKEY, DATA.NextLevelPercentage);
		SaveChar();
		PlayerPrefs.Save();
		//Debug.Log("Level: " + Level );
		//Debug.Log("DATA.CheckFirstRun: " + DATA.CheckFirstRun );
		//Debug.Log("Point: " + Point);
	}


	private void LoadChar()
	{
		Vector3[] charPos = PlayerPrefsX.GetVector3Array(CharPosKey);
		Vector3[] charRot = PlayerPrefsX.GetVector3Array(CharRotKey);
		int[] charKind = PlayerPrefsX.GetIntArray(CharKindKey);
		bool[] charBottomCollider =  PlayerPrefsX.GetBoolArray(CharBottomColliderKey);

		//restore chick
		for(int i =0; i < charPos.Length; i++)
		{
			RestoreChar(charPos[i], charRot[i], charKind[i], charBottomCollider[i]);
		}
		Debug.Log("LoadChar");

	}

	private void RestoreChar(Vector3  pos, Vector3  rot, int kind, bool bottomCollider) 
	{
		GameObject referenceChar = createCharManager.resourcesLoadChickDic[kind];
		if(createCharManager.resourcesLoadChickDic.ContainsKey(kind))
		{
			GameObject obj = Instantiate(referenceChar, pos, Quaternion.Euler(rot)) as GameObject;
			obj.name = referenceChar.name;
			obj.transform.parent = createCharManager.poolCharPlace;
			
			if(!bottomCollider)
				obj.GetComponentInChildren<BoxCollider>().enabled = false;
		}
		else{ Debug.Log(kind + " key isn't exist");}
	}

	public void LoadGameData()
	{
		//Debug.Log("LoadGameData");

		DATA.Score = PlayerPrefs.GetInt(DATA.SCOREKEY);
		DATA.Level = PlayerPrefs.GetInt(DATA.LEVELKEY);
		DATA.Point = PlayerPrefs.GetInt(DATA.POINTKEY);
		DATA.CheckFirstRun = System.Convert.ToBoolean(PlayerPrefs.GetInt(DATA.SAVEDFIRSTRUNKEY));
		DATA.NextLevelPercentage = PlayerPrefs.GetInt(DATA.NEXTLEVELPERCENTAGEKEY);
		LoadChar();

		//Debug.Log("DATA.Level: " + DATA.Level );
		//Debug.Log("DATA.CheckFirstRun: " + DATA.CheckFirstRun );
		//Debug.Log("DATA.Point: " + DATA.Point);
	}
	


}
