using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BitmapNumberManager : MonoBehaviour {

	private List<NumSettings> numSprites = new List<NumSettings>();
	private float firstNumPosX;
	private const float initializeOffsetX = 38f;

	void Awake () {
		//Debug.Log("transform.childCount: " + transform.childCount);

		for (int i = 0; i < transform.childCount; i++) {
			//Debug.Log("transform.GetChild(i).name: " + transform.GetChild(i).name);
			numSprites.Add(transform.GetChild(i).GetComponent<NumSettings>());
		}

		firstNumPosX = numSprites[0].transform.localPosition.x;
	}
	
	public void UpdateNumber(int refernce){
		float posX = firstNumPosX;

		//initialize position X
		foreach(var each in numSprites){
			each.gameObject.transform.setLocalPositionX(posX);
			posX -= initializeOffsetX;
			each.Show(false);
			//Debug.Log("each.name: " + each.name);
		}
		
		float offsetX = 20f;
		int offsetCount = 0;
		foreach(var each in numSprites)
		{
			//Debug.Log("refernce: " + refernce);
			if(refernce < 1) {return;}

			//Debug.Log("point: " + point);
			
			if(!each.activeSelf) {each.Show(true);}	
			
			int currNum = refernce % 10;
			
			each.gameObject.transform.addLocalPositionX(offsetX * offsetCount);
			
			//Debug.Log("offsetCount: " + offsetCount ); 
			
			each.SetSpriteName(currNum.ToString());
			
			if(currNum == 1){ offsetCount +=1;}
			refernce  /= 10;
		}
	}
}
