using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class UtageCameraManager : MonoBehaviour {
	
	//2D用カメラ
	[SerializeField]
	Camera[] cameras2D;

	//2DカメラのOrthographicSize
	[SerializeField]
	float camera2DOrthographicSize = 1;
	
	//3D用カメラ
	[SerializeField]
	Camera[] cameras3D;

	public float GetRoot2DScale(){
		return 2.0f*this.camera2DOrthographicSize / currentHeight;
	}

	//ゲーム画面を画面中央ではなく、下にくっつける形にする。広告表示などのレイアウトに合わせるために
	[SerializeField]
	bool isAnchorBottom = false;
	
	public enum TYPE{
		FIXED,
		FLOATING,
	};
	TYPE type;

	//アスペクト比
	public enum ASPECT{
		_1x2,		// 縦持ち 1:2
		_9x16,	// 縦持ち iPhone4inch (iPhone5)
		_2x3,		// 縦持ち iPhone3.5inch (iPhone4s以前)
		_3x4,		// 縦持ち iPad
		_1x1,		//  正方形
		_4x3,		// 横持ち iPad
		_3x2,		// 横持ち iPhone3.5inch (iPhone4s以前)
		_16x9,	// 横持ち iPhone4inch (iPhone5)
		_2x1,		// 横持ち 2:1
	};

	[SerializeField]
	ASPECT defaultAspect = ASPECT._2x3;	//基本のアスペクト比
	
	[SerializeField]
	ASPECT wideAspect = ASPECT._3x4;	//最も横長になった場合のアスペクト比

	[SerializeField]
	ASPECT nallowAspect = ASPECT._9x16;	//最も縦長になった場合のアスペクト比
	
	[SerializeField]
	int defaultHeight = 960;
	public int DefaultHeight{ get { return defaultHeight; } }

	public int DefaultWidth{ get { return (int)(defaultHeight*CalcAspectRatio(defaultAspect)); } }
	
	bool isChanged;
	float currentAspectRatio;
	int currentHeight;
	public int CurrentHeight{ get { return currentHeight; } }

	int currentWidth;
	public int CurrentWidth{ get { return currentWidth; } }
	float screenAspectRatio;
	
	Rect currentRect;
	
	void Awake(){
		Reflesh();
	}
	void Update(){
		if( !Mathf.Approximately( screenAspectRatio, 1.0f*Screen.width/Screen.height) ){
			Reflesh();
		}
	}

	//ゲームの終了処理
	//Androidで、iPhoneっぽくアプリを終了させる(描画範囲を中央に向けて閉じる)
	public IEnumerator CoGameExit( float fadetime = 0.2f ){
		float time = 0;
		Rect start = currentRect;
		Rect rect = currentRect;
		while(true){
			float rate = 1.0f - time/fadetime;
			if( rate <= 0 ){
				break;
			}
			rect.width = start.width*rate;
			rect.height =  start.height*rate;
			rect.center = start.center;
			SetRects(rect);
			yield return 0;
			time += Time.deltaTime;
		}
		foreach( Camera camera2d in cameras2D ){
			camera2d.gameObject.SetActive(false);
		}
		foreach( Camera camera3d in cameras3D ){
			camera3d.gameObject.SetActive(false);
		}
		yield return 0;
		yield return 0;
	}
	
	void Reflesh(){
		screenAspectRatio = 1.0f*Screen.width/Screen.height;
		
		float defaultAspectRatio = CalcAspectRatio(defaultAspect);
		float wideAspectRatio = CalcAspectRatio(wideAspect);
		float nallowAspectRatio = CalcAspectRatio(nallowAspect);
		
		bool isCameraClip = false;
		//スクリーンのアスペクト比から、ゲームのアスペクト比を決める
		if( screenAspectRatio > wideAspectRatio ){
			//アスペクト比が設定よりも横長
			currentAspectRatio = wideAspectRatio;
		}else if( screenAspectRatio < nallowAspectRatio ){
			//アスペクト比が設定よりも縦長
			currentAspectRatio = nallowAspectRatio;
		}else{
			//アスペクト比が設定の範囲内ならそのままスクリーンのアスペクト比を使う
			currentAspectRatio = screenAspectRatio;
		}
		
		//ゲームの画面サイズを決める
		if( currentAspectRatio < defaultAspectRatio ){
			//ゲームのアスペクト比が、デフォルトのアスペクト比よりも縦長
			currentHeight = (int)(defaultHeight*defaultAspectRatio/currentAspectRatio);
		}else{
			currentHeight = defaultHeight;
		}
		currentWidth = (int)(currentHeight*currentAspectRatio);
		
		float marginX = 0;
		float marginY = 0;
		//描画領域をクリップする
		if( currentAspectRatio != screenAspectRatio ){
			if( screenAspectRatio > currentAspectRatio ){
				//スクリーンのほうが横長なので、左右をクリップ.
				marginX = ( 1.0f - currentAspectRatio/screenAspectRatio )/2;
				marginY = 0;
			}
			else if( screenAspectRatio < currentAspectRatio ){
				//スクリーンのほうが縦長なので、上下をクリップ.
				marginX = 0;
				marginY = ( 1.0f - screenAspectRatio/currentAspectRatio )/2;
			}
		}
		
		if( isAnchorBottom ){
			currentRect = new Rect ( marginX, 0, 1- marginX*2, 1 - marginY*2 );
		}else{
			currentRect = new Rect ( marginX, marginY, 1- marginX*2, 1 - marginY*2 );
		}
		
		SetRects(currentRect);
	}
	
	void SetRects( Rect rect ){
		foreach( Camera camera2d in cameras2D ){
			camera2d.rect = rect;
			camera2d.orthographicSize = this.camera2DOrthographicSize;
		}
		foreach( Camera camera3d in cameras3D ){
			camera3d.rect = rect;
		}
	}
	
	float CalcAspectRatio( ASPECT aspect ){
		switch(aspect){
		case ASPECT._1x2:
			return 1.0f/2;
		case ASPECT._9x16:
			return 9.0f/16;
		case ASPECT._2x3:
			return 2.0f/3;
		case ASPECT._3x4:
			return 3.0f/4;
		case ASPECT._1x1:
			return 1;
		case ASPECT._4x3:
			return 4.0f/3;
		case ASPECT._3x2:
			return 3.0f/2;
		case ASPECT._16x9:
			return 16.0f/9;
		case ASPECT._2x1:
			return 2.0f;
		default:
			return 1;
		}
	}
}
