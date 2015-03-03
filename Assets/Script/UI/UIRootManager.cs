using UnityEngine;
using System.Collections;
enum ScreenAspect{inch4 = 67, inch4_5 = 56, iPad = 75};
public class UIRootManager : MonoBehaviour {

	public delegate void setBgAspectHandler();
	public static event setBgAspectHandler _setPosition;

	public static float Aspect;

	private UIRoot thisUIRoot;

	public static int panelWidth {get; private set;}
	public static int panelHeight {get; private set;}
	private static int ManualHeight;

	void Awake(){
		Aspect = (float)Screen.width / (float)Screen.height;
		Aspect = Mathf.Round(Aspect * 100);
		thisUIRoot = GetComponent<UIRoot>();
		thisUIRoot.scalingStyle = UIRoot.Scaling.ConstrainedOnMobiles;

		//iPad
		if(Aspect >= (int)ScreenAspect.iPad )
		{
			ManualHeight = 1024;
		}
		//3.5inch
		else if(Aspect >= (int)ScreenAspect.inch4)
		{
			ManualHeight = 960;
		}
		//4inch
		else if(Aspect >= (int)ScreenAspect.inch4_5)
		{
			ManualHeight = 1136;
		}

		thisUIRoot.manualHeight = ManualHeight;
	}

	//AwakeではUIPanel座標が変わったとき正しく取得できないのでStartで実行する
	//When change coordinate value of UIPanel In Awake, script can't get accurate value of panel's clipping rectangle. so excuse it in Start.
	void Start()
	{
		setPanelSize();
//		_setPosition(); //delegate 
	}



	void setPanelSize()
	{
		float aspectXFromScreenSize = (float)Screen.width / (float)Screen.height;
		panelHeight = ManualHeight;
		panelWidth = Mathf.RoundToInt(panelHeight * aspectXFromScreenSize);
		//print ("panelWidth: " + panelWidth);
		//print ("panelHeight: " + panelHeight);
	}

	
	void Update () {
		
	}
}
