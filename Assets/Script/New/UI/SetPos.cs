using UnityEngine;
using System.Collections;

public class SetPos : MonoBehaviour {
	
	public bool newPanelActiveSelf = false;
	
	//[SerializeField]
	public Vector3 newPanelSetPosition = new Vector3(6f, 19.7f, 0f);
	
	void Start () {
		setNewPanelPos();
	}
	
	void setNewPanelPos(){
		int childCount = this.gameObject.transform.childCount;
		
		for(int i =0; i < childCount; i++){
			Transform eachItem = transform.GetChild(i).GetComponent<Transform>();
			Transform newTransform = eachItem.GetChild(0).FindChild("New").transform;
			newTransform.gameObject.SetActive(newPanelActiveSelf);
			newTransform.localPosition = newPanelSetPosition;
		}
	}
	

	void Update () {
	
	}
}
