//----------------------------------------------
// UTAGE: Unity Text Adventure Game Engine
// Copyright © 2012-2013 Ryohei Tokimura
//----------------------------------------------

using UnityEngine;
using System.Collections;

[AddComponentMenu("Utage/NGUI/OffsetZ")]

[ExecuteInEditMode]

/// <summary>
/// NGUIの、Z値定義コンポーネント。描画順を制御のために使う。
/// 名前をキーにして定義したZ値を参照できるようにしてる
/// 拡張に不向きなので、enumは使わなかった。
/// </summary>
public class UtageNguiOffsetZ : MonoBehaviour {
	
	public static string[] GetOffsetNames(){
		return offsetNames;
	}
	public const string NAME_UI_SYSTEM = "UI_SYSYTEM";
	
	static string[] offsetNames = {
		"TEXT_OFFSET",	"BG",	"CHARCTER",	"MESSEAGE_WINDOW",	"SELECTION",	"UI_BACK",	"UI",	"UI_FRONT",	"UI_BANNER_BACK",	"UI_BANNER",	"UI_BANNER_FRONT",	"UI_3D",	"UI_COMMON",	"UI_COMMON_FRONT",  NAME_UI_SYSTEM,	"DEBUG"
	};
	static float[] offsetValues = {
		-0.1f,			100,	90,			80,					70,				60,			50,		40,			35,					30,				25,					-70,			-90,		-100,				-150,			-200
	};

	private float offsetZ;
	public float OffsetZ{ get { return offsetZ; } }
	[SerializeField]
	string offsetName;
	public string OffsetName{
		get { return offsetName; }
		set { 
			offsetName = value;
			offsetZ = offsetValues[ FindCurrentNameIndex() ];
			UpdateOffsetZ();
		}
	}
	public static int FindNameIndex( string name ){
		int index = 0;
		string[] names = GetOffsetNames();
		for( int i = 0; i < names.Length; ++i ){
			if( name == names[i] ){
				index = i;
				break;
			}
		}
		return index;
	}

	public static float GetOffsetZ( string name ){
		return offsetValues[ FindNameIndex(name) ];
	}
	
	void UpdateOffsetZ(){
		if( this.transform.localPosition.z != offsetZ ){
			this.transform.localPosition = new Vector3( this.transform.localPosition.x, this.transform.localPosition.y, offsetZ );
		}
	}
	
	void Awake(){
		offsetZ = offsetValues[ FindCurrentNameIndex() ];
		UpdateOffsetZ();
	}
	
	void Update(){
		UpdateOffsetZ();
	}
	
	public int FindCurrentNameIndex(){
		return FindNameIndex( OffsetName );
	}
}
