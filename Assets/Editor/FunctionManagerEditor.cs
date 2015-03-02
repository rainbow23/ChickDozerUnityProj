using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(FunctionManager))]
[CanEditMultipleObjects]
public class FunctionManagerEditor : Editor {

	public override void OnInspectorGUI() {
		//Debug.Log("OnInspectorGUI");
		//DrawDefaultInspector();
		
		FunctionManager obj = target as FunctionManager;
		obj.funcType = (FunctionManager.FuncType)EditorGUILayout.EnumPopup( "FuncType", obj.funcType );

		if(obj.funcType == FunctionManager.FuncType.HelpFunc)
		{
			//EditorGUILayout.BeginHorizontal(); // arrange value in the horizontal
			obj.helpGameObj = (GameObject)EditorGUILayout.ObjectField( "Help Obj", obj.helpGameObj, typeof( GameObject ), true );
			obj.showHelp = EditorGUILayout.Toggle("Show/Hide", obj.showHelp);
			//EditorGUILayout.EndHorizontal();
		}

		EditorUtility.SetDirty(obj);
	}
}
