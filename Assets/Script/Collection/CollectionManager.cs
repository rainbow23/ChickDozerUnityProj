using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class CollectionManager : MonoBehaviour {

	private const string CHARPANEL = "CharPanel";
	private Dictionary<int, CharPanel> charPanelDic = new Dictionary<int, CharPanel>();

	void Awake()
	{
		var charPanels = GameObject.FindGameObjectsWithTag(TAG.CHARPANEL)
				.OrderBy(t => {
					string numString = t.name.Substring(t.name.Length -2);
					int num = int.Parse(numString);
					return num;
				})
				//.Cast<CharPanel>()
				.ToArray();
			
		int count = 0;
		foreach (GameObject charPanel in charPanels) 
		{ 
			charPanelDic.Add(count, charPanel.GetComponent<CharPanel>());
			count += 1;
			//Debug.Log("charPanel: " + charPanel.gameObject.name);
		}

	}

	void Start () 
	{
		ShowObtainedCharPanel();
	}

	void ShowObtainedCharPanel()
	{
		//int[] obtainedCharArray = new int[DATA.ResourcesChickNum];
		int[] obtainedCharArray = PlayerPrefsX.GetIntArray(DATA.OBTAINEDCHARKEY);

		for (int i = 0; i < obtainedCharArray.Length; i++) 
		{
			//Debug.Log("Load obtainedCharArray[" + i + "]: " + obtainedCharArray[i]);
			Debug.Log("i:" + i);



			//if already get chick panel shows chick
			if(obtainedCharArray[i] == 1)
				charPanelDic[i].ShowChar(true);
			else if(obtainedCharArray[i] == 0)
				charPanelDic[i].ShowChar(false);

		}
	}

	void Update () 
	{
	
	}
}
