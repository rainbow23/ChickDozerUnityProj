using UnityEngine;
using System.Collections;

public static class TransformExtension{
	private static Vector3 vector3;

	//under three method is used shoot Apple project
	#region  SetLocalPosition
	public static void positionX(this Transform transform, float x){
		Vector3 newPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
		transform.localPosition = newPosition;
	}
	public static void positionY(this Transform transform, float y){
		Vector3 newPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
		transform.localPosition = newPosition;
	}
	public static void positionZ(this Transform transform, float z){
		Vector3 newPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
		transform.localPosition = newPosition;
	}
	#endregion



	#region SetPosition
	public static void setPosition(this Transform transform, float x, float y, float z) {
		vector3.Set(x, y, z);
		transform.position = vector3;
	}
	public static void setPositionX(this Transform transform, float x) {
		vector3.Set(x, transform.position.y, transform.position.z);
		transform.position = vector3;
	}
	public static void setPositionY(this Transform transform, float y) {
		vector3.Set(transform.position.x, y, transform.position.z);
		transform.position = vector3;
	}
	public static void setPositionZ(this Transform transform, float z) {
		vector3.Set(transform.position.x, transform.position.y, z);
		transform.position = vector3;
	}
	#endregion

	#region AddPosition
	public static void addPosition(this Transform transform, float x, float y, float z) {
		vector3.Set(transform.position.x + x, transform.position.y + y, transform.position.z + z);
		transform.position = vector3;
	}
	public static void addPositionX(this Transform transform, float x) {
		vector3.Set(transform.position.x + x, transform.position.y, transform.position.z);
		transform.position = vector3;
	}
	public static void addPositionY(this Transform transform, float y) {
		vector3.Set(transform.position.x, transform.position.y + y, transform.position.z);
		transform.position = vector3;
	}
	public static void addPositionZ(this Transform transform, float z) {
		vector3.Set(transform.position.x, transform.position.y, transform.position.z + z);
		transform.position = vector3;
	}
	#endregion

	#region SetLocalPosition
	public static void setLocalPosition(this Transform transform, float x, float y, float z){
		vector3.Set(x, y, z);
		transform.localPosition = vector3;
	}
	public static void setLocalPositionX(this Transform transform, float x){
		vector3.Set(x, transform.localPosition.y, transform.localPosition.z);
		transform.localPosition = vector3;
	}
	public static void setLocalPositionY(this Transform transform, float y){
		vector3.Set(transform.localPosition.x, y, transform.localPosition.z);
		transform.localPosition = vector3;
	}
	public static void setLocalPositionZ(this Transform transform, float z){
		vector3.Set(transform.localPosition.x, transform.localPosition.y, z);
		transform.localPosition = vector3;
	}
	#endregion

	#region AddLocalPosition
	public static void addLocalPosition(this Transform transform, float x, float y, float z){
		vector3.Set(transform.localPosition.x + x, transform.localPosition.y + y, transform.localPosition.z + z);
		transform.localPosition = vector3;
	}
	public static void addLocalPositionX(this Transform transform, float x){
		vector3.Set(transform.localPosition.x + x, transform.localPosition.y, transform.localPosition.z);
		transform.localPosition = vector3;
	}
	public static void addLocalPositionY(this Transform transform, float y){
		vector3.Set(transform.localPosition.x, transform.localPosition.y + y, transform.localPosition.z);
		transform.localPosition = vector3;
	}
	public static void addLocalPositionZ(this Transform transform, float z){
		vector3.Set(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + z);
		transform.localPosition = vector3;
	}
	#endregion

	#region SetLocalScale
	public static void setLocalScale(this Transform transform, float x, float y, float z) {
		vector3.Set(x, y, z);
		transform.localScale = vector3;
	}
	public static void setLocalScaleX(this Transform transform, float x) {
		vector3.Set(x, transform.localScale.y, transform.localScale.z);
		transform.localScale = vector3;
	}
	public static void setLocalScaleY(this Transform transform, float y) {
		vector3.Set(transform.localScale.x, y, transform.localScale.z);
		transform.localScale = vector3;
	}
	public static void setLocalScaleZ(this Transform transform, float z) {
		vector3.Set(transform.localScale.x, transform.localScale.y, z);
		transform.localScale = vector3;
	}
	#endregion

	#region AddLocalScale
	public static void addLocalScale(this Transform transform, float x, float y, float z) {
		vector3.Set(transform.localScale.x + x, transform.localScale.y + y, transform.localScale.z + z);
		transform.localScale = vector3;
	}
	public static void addLocalScaleX(this Transform transform, float x) {
		vector3.Set(transform.localScale.x + x, transform.localScale.y, transform.localScale.z);
		transform.localScale = vector3;
	}
	public static void addLocalScaleY(this Transform transform, float y) {
		vector3.Set(transform.localScale.x, transform.localScale.y + y, transform.localScale.z);
		transform.localScale = vector3;
	}
	public static void addLocalScaleZ(this Transform transform, float z) {
		vector3.Set(transform.localScale.x, transform.localScale.y, transform.localScale.z + z);
		transform.localScale = vector3;
	}
	#endregion

	#region SetEulerAngles
	public static void setEulerAngles(this Transform transform, float x, float y, float z){
		vector3.Set(x, y, z);
		transform.eulerAngles = vector3;
	}
	public static void setEulerAnglesX(this Transform transform, float x) {
		vector3.Set(x, transform.eulerAngles.y, transform.eulerAngles.z);
		transform.eulerAngles = vector3;
	}
	public static void setEulerAnglesY(this Transform transform, float y) {
		vector3.Set(transform.eulerAngles.x, y, transform.eulerAngles.z);
		transform.eulerAngles = vector3;
	}
	public static void setEulerAnglesZ(this Transform transform, float z) {
		vector3.Set(transform.eulerAngles.x, transform.eulerAngles.y, z);
		transform.eulerAngles = vector3;
	}
	#endregion

	#region AddEulerAngles
	public static void addEulerAngles(this Transform transform, float x, float y, float z){
		vector3.Set(transform.eulerAngles.x + x, transform.eulerAngles.y + y, transform.eulerAngles.z + z);
		transform.eulerAngles = vector3;
	}
	public static void addEulerAnglesX(this Transform transform, float x) {
		vector3.Set(transform.eulerAngles.x + x, transform.eulerAngles.y, transform.eulerAngles.z);
		transform.eulerAngles = vector3;
	}
	public static void addEulerAnglesY(this Transform transform, float y) {
		vector3.Set(transform.eulerAngles.x, transform.eulerAngles.y + y, transform.eulerAngles.z);
		transform.eulerAngles = vector3;
	}
	public static void addEulerAnglesZ(this Transform transform, float z) {
		vector3.Set(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + z);
		transform.eulerAngles = vector3;
	}
	#endregion

	#region SetLocalEulerAngles
	public static void setLocalEulerAngles(this Transform transform, float x, float y, float z) {
		vector3.Set(x, y, z);
		transform.localEulerAngles = vector3;
	}
	public static void setLocalEulerAnglesX(this Transform transform, float x) {
		vector3.Set(x, transform.localEulerAngles.y, transform.localEulerAngles.z);
		transform.localEulerAngles = vector3;
	}
	public static void setLocalEulerAnglesY(this Transform transform, float y) {
		vector3.Set(transform.localEulerAngles.x, y, transform.localEulerAngles.z);
		transform.localEulerAngles = vector3;
	}
	public static void setLocalEulerAnglesZ(this Transform transform, float z) {
		vector3.Set(transform.localEulerAngles.x, transform.localEulerAngles.y, z);
		transform.localEulerAngles = vector3;
	}
	#endregion

	#region AddLocalEulerAngles
	public static void addLocalEulerAngles(this Transform transform, float x, float y, float z) {
		vector3.Set(transform.localEulerAngles.x + x, transform.localEulerAngles.y + y, transform.localEulerAngles.z + z);
		transform.localEulerAngles = vector3;
	}
	public static void addLocalEulerAnglesX(this Transform transform, float x) {
		vector3.Set(transform.localEulerAngles.x + x, transform.localEulerAngles.y, transform.localEulerAngles.z);
		transform.localEulerAngles = vector3;
	}
	public static void addLocalEulerAnglesY(this Transform transform, float y) {
		vector3.Set(transform.localEulerAngles.x, transform.localEulerAngles.y + y, transform.localEulerAngles.z);
		transform.localEulerAngles = vector3;
	}
	public static void addLocalEulerAnglesZ(this Transform transform, float z) {
		vector3.Set(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + z);
		transform.localEulerAngles = vector3;
	}
	#endregion
}