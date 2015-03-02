using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(SpritePositionManager))]
[CanEditMultipleObjects]
public class SpritePositionManagerEditor : Editor 
{
	static Vector2 coppyBuffer = Vector2.zero;

	void OnEnable()
	{

	}
	
	public override void OnInspectorGUI() {
		//Debug.Log("OnInspectorGUI");

		SpritePositionManager obj = target as SpritePositionManager;
		Vector2 currPosXY = new Vector2(obj.gameObject.transform.localPosition.x, obj.gameObject.transform.localPosition.y);

		//iPhone4s
		Vector2 iPhone4sEditorValue = EditorGUILayout.Vector2Field("iPhone4s Position", obj.iPhone4s);
		EditorGUILayout.BeginHorizontal(); // arrange value in the horizontal

		if(GUILayout.Button("CurrentPosXY", EditorStyles.miniButton))
			iPhone4sEditorValue = currPosXY;

		GUILayout.Space(GUILayoutUtility.GetAspectRect(1).width / 3);

		if(GUILayout.Button("Copy", EditorStyles.miniButtonLeft))
			coppyBuffer = iPhone4sEditorValue;

		if(GUILayout.Button("Paste", EditorStyles.miniButtonRight))
			iPhone4sEditorValue = coppyBuffer;
		EditorGUILayout.EndHorizontal();

		//iPhone5
		Vector2 iPhone5EditorValue = EditorGUILayout.Vector2Field("iPhone5 Position", obj.iPhone5);
		EditorGUILayout.BeginHorizontal(); // arrange value in the horizontal

		if(GUILayout.Button("CurrentPosXY", EditorStyles.miniButton))
			iPhone5EditorValue = currPosXY;
		
		GUILayout.Space(GUILayoutUtility.GetAspectRect(1).width / 3);
		
		if(GUILayout.Button("Copy", EditorStyles.miniButtonLeft))
			coppyBuffer = iPhone5EditorValue;
		
		if(GUILayout.Button("Paste", EditorStyles.miniButtonRight))
			iPhone5EditorValue = coppyBuffer;
		EditorGUILayout.EndHorizontal();


		//iPad
		Vector2 iPadEditorValue = EditorGUILayout.Vector2Field("iPad Position", obj.iPad);
		EditorGUILayout.BeginHorizontal(); // arrange value in the horizontal
		
		if(GUILayout.Button("CurrentPosXY", EditorStyles.miniButton))
			iPadEditorValue = currPosXY;
		
		GUILayout.Space(GUILayoutUtility.GetAspectRect(1).width / 3);
		
		if(GUILayout.Button("Copy", EditorStyles.miniButtonLeft))
			coppyBuffer = iPadEditorValue;
		
		if(GUILayout.Button("Paste", EditorStyles.miniButtonRight))
			iPadEditorValue = coppyBuffer;
		EditorGUILayout.EndHorizontal();



		if(iPhone4sEditorValue != obj.iPhone4s)// if changed value, process data
		{
			Undo.RegisterCompleteObjectUndo(obj, 	"Changed iPhone4s position");
			obj.iPhone4s = iPhone4sEditorValue;
			EditorUtility.SetDirty(obj);
		}

		if(iPhone5EditorValue != obj.iPhone5)// if changed value, process data
		{
			Undo.RegisterCompleteObjectUndo(obj, 	"Changed iPhone5 position");
			obj.iPhone5 = iPhone5EditorValue;
			EditorUtility.SetDirty(obj);
		}

		if(iPadEditorValue != obj.iPad)// if changed value, process data
		{
			Undo.RegisterCompleteObjectUndo(obj, 	"Changed iPad position");
			obj.iPad = iPadEditorValue;
			EditorUtility.SetDirty(obj);
		}

	}

}
