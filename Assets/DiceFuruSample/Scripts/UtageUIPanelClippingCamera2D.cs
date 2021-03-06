using UnityEngine;
using System.Collections;

//パネルのクリッピングサイズを、現在のゲームスクリーンサイズを2Dカメラから取得
public class UtageUIPanelClippingCamera2D : MonoBehaviour {
	public UtageCameraManager cameraManager;
	public UIPanel panel;
	public enum TYPE{
		HOLIZON,
		VERTICAL,
	};
	public TYPE type;
	
	public int offsetCenter;
	public int offsetSize;
	
	void Awake(){
		if( null == panel ) panel = GetComponent<UIPanel>();
	}
	
	void OnEnable(){
		float adHeight = 0;
//		UtageADBannerView view = UtageADBannerView.GetInstance();
//		if( view != null ) adHeight = view.GetGame2DHeight();

		if( null != panel && null != cameraManager){
			Vector4 clipRange = panel.clipRange;
			switch(type){
			case TYPE.HOLIZON:
				clipRange.z =  cameraManager.CurrentWidth + offsetSize;
				clipRange.x =  0 + offsetCenter;
				break;
			case TYPE.VERTICAL:
				clipRange.w =  cameraManager.CurrentHeight + offsetSize - adHeight;
				clipRange.y =  -(clipRange.w)/2  + offsetCenter;
				break;
			}
			panel.clipRange = clipRange;
			panel.transform.localPosition = Vector3.zero;
		}
	}
}
