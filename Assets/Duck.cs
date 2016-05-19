using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Duck : MonoBehaviour {
	
	public float speed = 44f;
	private bool gotHit = false;

	public delegate void HandleGotAHit ();
	public static event HandleGotAHit OnGotAHit;



	void Update () {
		if (!gotHit) {
			transform.position += new Vector3 (speed, 0f, 0f);	
		}
	}

	public void getHit() {
		gotHit = true;
		OnGotAHit ();
		GetComponent<Rigidbody2D> ().gravityScale = 1f;


	}
}
