using UnityEngine;
using System.Collections;

public class Catapult : MonoBehaviour {

	public Transform boulderPrefab;
	public float minPower;
	public float maxPower = 444f;
	public float maxPrepTime = 3f;

	private float shootTimeStamp;
	private float shootPrepTime;

	void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			shootTimeStamp = Time.deltaTime;
		} else if (Input.GetKeyUp (KeyCode.A)) {
			shootPrepTime = Time.deltaTime - shootTimeStamp;
			shoot ();
		}
	}

	private Vector2 pos2() {
		return new Vector2 (transform.position.x, transform.position.y);
	}

	private void shoot() {
		Vector3 mouse = Input.mousePosition;
		mouse = Camera.main.ViewportToWorldPoint (new Vector3 (mouse.x, mouse.y, transform.position.z));
		Transform boulder = Instantiate<Transform> (boulderPrefab);
		boulder.gameObject.SetActive (true);
		float ratio = minPower + maxPower * Mathf.Min (shootPrepTime / maxPrepTime, 1f);
		print (mouse.ToString ());

		Vector2 dir = new Vector2 (mouse.x, mouse.y) - pos2();
		boulder.GetComponent<Rigidbody2D> ().AddForce (dir.normalized * (ratio));
	}
}
