using UnityEngine;
using System.Collections;

public class BombChick : ChickManager {
	
	void Awake () 
	{
		SetPerformFly(new FlyWithWings());
		base.PeformFly();
	//	base.PeformFly();
	}

	void Start()
	{
		SetPerformFly(new FlyNoWay());
		base.PeformFly();
	}





	void Update () 
	{
	
	}
}
