using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CreateCharManager : SingletonMonoBehaviour<CreateCharManager> {
	private GameObject[] AllChicks;
	public static Dictionary<int, GameObject> ResourcesLoadChickDic = new Dictionary<int, GameObject>();

	void Awake()
	{
			//int index = int.Parse(chick.name.Substring(chick.name.Length -3));
			//ResourcesLoadChickDic.Add(index, chick);

		LoadCharFromResources();
	}

	void Start()
	{
		//Create(GameController.touchPos.Value);
		GameController.touchPos.AddListener(Create);
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

	void Create(Vector3 touchPos)
	{
		//とりあえずレベル１だけしか作らない
		GameObject obj = Instantiate(AllChicks[0], touchPos, Quaternion.Euler(-30f, 0f, 0f)) as GameObject;
		obj.name = AllChicks[0].name;
		obj.SetActive(true);
	}

	public void RestoreChick(Vector3  pos, Vector3  rot, int kind) 
	{
		if(ResourcesLoadChickDic.ContainsKey(kind))
		{
			GameObject obj = Instantiate(ResourcesLoadChickDic[kind], pos, Quaternion.Euler(rot)) as GameObject;
			obj.name = AllChicks[kind].name;
		}
		else{ Debug.Log(kind + " key isn't exist");}
	}

	void OnDestroy ()
	{
		if (GameController.Instance != null) {
			GameController.touchPos.RemoveListener (Create);
		}
	}
}
