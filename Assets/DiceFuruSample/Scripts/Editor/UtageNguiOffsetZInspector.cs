using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UtageNguiOffsetZ))]

/// <summary>
/// NGUIの、Z値定義コンポーネント。描画順を制御のために使う。
/// 名前をキーにして定義したZ値を参照できるようにしてる
/// 拡張に不向きなので、enumは使わなかった。
/// </summary>
class UtageNguiOffsetZInspector : Editor {

	public override void OnInspectorGUI() {
		string[] names = UtageNguiOffsetZ.GetOffsetNames();

		UtageNguiOffsetZ offset = target as UtageNguiOffsetZ;
		int currentIndex = offset.FindCurrentNameIndex();
		int index = EditorGUILayout.Popup( currentIndex, names );
		if( index != currentIndex ){
			offset.OffsetName = names[index];
		}
    }
}
