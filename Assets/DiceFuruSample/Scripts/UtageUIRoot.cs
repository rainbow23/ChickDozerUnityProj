using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class UtageUIRoot : UIRoot {
	public UtageCameraManager cameraManager;
	
	void Update () {
		UpdateScale ();
	}
	
	//カメラの更新
	void UpdateScale () {
		if( null != cameraManager ){

			float scale = cameraManager.GetRoot2DScale();
			if( !Mathf.Approximately(this.transform.localScale.x, scale ) ){
				this.transform.localScale = new Vector3( scale, scale, scale );
			}
			
			if(	manualHeight != cameraManager.CurrentHeight ){
				manualHeight = cameraManager.CurrentHeight;
			}
			
			//if( scalingStyle != UIRoot.Scaling.FixedSize ){
			//	scalingStyle = UIRoot.Scaling.FixedSize;
			//}
		}
	}
}
