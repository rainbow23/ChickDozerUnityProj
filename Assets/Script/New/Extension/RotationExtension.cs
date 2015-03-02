using UnityEngine;
using System.Collections;

public static class RotationExtension{
	public static void rotationX(this Transform transform, float x){
		Vector3 newRotation = new Vector3(x, transform.rotation.y, transform.rotation.z);
		transform.rotation = Quaternion.Euler(newRotation);
	}
	public static void rotationY(this Transform transform, float y){
		Vector3 newRotation = new Vector3(transform.rotation.x, y, transform.rotation.z);
		transform.rotation = Quaternion.Euler(newRotation);
	}
	public static void rotationZ(this Transform transform, float z){
		Vector3 newRotation = new Vector3(transform.rotation.x, transform.rotation.y, z);
		transform.rotation = Quaternion.Euler(newRotation);
	}
}
