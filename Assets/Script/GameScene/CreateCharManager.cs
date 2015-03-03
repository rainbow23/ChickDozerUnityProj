using UnityEngine;
using System.Collections;
using System.Linq;

public class CreateCharManager : SingletonMonoBehaviour<CreateCharManager> {
	public static GameObject[] AllChicks = new GameObject[36];

	void Awake()
	{
		AllChicks = Resources.LoadAll("ChickNewUv",typeof(GameObject))
			.Cast<GameObject>()
			.OrderBy(t => {
			string chickIndexString = t.name.Substring(t.name.Length -3);
			int chickIndexInt = int.Parse(chickIndexString);
			return chickIndexInt;
			})
			.ToArray();
	}

	void Start()
	{
		//Create(GameController.touchPos.Value);
		GameController.touchPos.AddListener(Create);
	}

	void Create(Vector3 touchPos)
	{
		//とりあえずレベル１だけしか作らない
		GameObject obj = Instantiate(
				AllChicks[0], 
				touchPos, 
				Quaternion.Euler(-30f, 0f, 0f)) as GameObject;
			obj.name = AllChicks[0].name;
			obj.rigidbody.angularDrag = 0f;
			obj.SetActive(true);
	}

	void OnDestroy ()
	{
		if (GameController.Instance != null) {
			GameController.touchPos.RemoveListener (Create);
		}
	}
}
