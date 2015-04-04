using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class CollectionManager : MonoBehaviour {
	private bool isLoadedGame = false;
	private const string CHARPANEL = "CharPanel";
	private Dictionary<int, CharPanel> charPanelDic = new Dictionary<int, CharPanel>();

	private int[] obtainedCharArray = new int[DATA.ResourcesChickNum];

	void Awake()
	{
		isLoadedGame = true;
		LoadElapseData();
		LoadCharPanels();
	}

	void Start () 
	{
		ShowObtainedCharPanel();
		TakeOffFirstTimeLoadPanel();
	}

	void LoadCharPanels()
	{
		var charPanels = GameObject.FindGameObjectsWithTag(TAG.CHARPANEL)
			.OrderBy(t => {
				string numString = t.name.Substring(t.name.Length -2);
				int num = int.Parse(numString);
				return num;
			})
				.ToArray();
		
		int count = 0;
		foreach (GameObject charPanel in charPanels) 
		{ 
			charPanelDic.Add(count, charPanel.GetComponent<CharPanel>());
			count += 1;
			//Debug.Log("charPanel: " + charPanel.gameObject.name);
		}
	}
	
	void ShowObtainedCharPanel()
	{
		//int[] obtainedCharArray = new int[DATA.ResourcesChickNum];
		int[] obtainedCharArray = PlayerPrefsX.GetIntArray(DATA.OBTAINEDCHARKEY);

		for (int i = 0; i < obtainedCharArray.Length; i++) 
		{
			//Debug.Log("Load obtainedCharArray[" + i + "]: " + obtainedCharArray[i]);
			//Debug.Log("i:" + i);

			if(obtainedCharArray[i] == 0){// dont get chick
				charPanelDic[i].ShowChar(false);
			}
			else if(obtainedCharArray[i] == 1){//if first time got  some kinds of chick it's shows chick panel
				charPanelDic[i].ShowChar(true);
				charPanelDic[i].ShowFirstTimeChar();
			}
			else {// if already got some kinds of chick
				charPanelDic[i].ShowChar(true);
			}
		}
	}

	void LoadElapseData()
	{
		Debug.Log("LoadElapseData Collection");
		obtainedCharArray = PlayerPrefsX.GetIntArray(DATA.OBTAINEDCHARKEY);
	}

	void TakeOffFirstTimeLoadPanel()
	{
		for (int i = 0; i < obtainedCharArray.Length; i++) 
		{
			if(obtainedCharArray[i] == 1)
			{
				obtainedCharArray[i] = 2;
			}
		}
	}

	void OnApplicationPause(bool pauseStatus) 
	{
		//離れる時
		if(pauseStatus){
			#if UNITY_EDITOR
			//ゲームシーンから再生時OnApplicationPauseが呼ばれてしまう
			Debug.Log("OnApplicationPause go away in UnityEditor");
			#endif
			SaveToDisc();
		} 
		//戻る時
		else{ 
			#if UNITY_EDITOR
			//ゲームシーンから再生時OnApplicationPauseが呼ばれてしまうのでLoadElapseData();を呼ばない
			Debug.Log("OnApplicationPause come back in UnityEditor");
			#else
			Debug.Log("OnApplicationPause in  except UnityEditor");
			#endif
		}
	}

	void OnApplicationQuit()
	{
		Debug.Log("OnApplicationQuit");
		//gamesceneで止めるとなぜかtitleで読み込まれていることになる為、boolを作る
		if(isLoadedGame) SaveToDisc();
	}

	void OnDestroy()
	{
		//Debug.Log("GameController OnDestroy");
		SaveToDisc();
	}

	void SaveToDisc()
	{
		Debug.Log("SaveToDisc: Collection");
		PlayerPrefsX.SetIntArray(DATA.OBTAINEDCHARKEY, obtainedCharArray);
		PlayerPrefs.Save();

		for (int i = 0; i < obtainedCharArray.Length; i++) {
			Debug.Log("Store obtainedCharArray[" + i + "]: " + obtainedCharArray[i]);
		}
		Debug.Log("Collection");
	}
}
