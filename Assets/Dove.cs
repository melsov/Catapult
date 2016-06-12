using UnityEngine;
using System.Collections;

public class Dove : Duck {



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void getHit(Boulder boulder) {
		base.getHit (boulder);

	}
}
