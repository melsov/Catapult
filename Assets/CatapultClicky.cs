using UnityEngine;
using System.Collections;

public class CatapultClicky : MonoBehaviour 
{
	public Transform boulderOriginal;

	public float strength = 5000f;

	void Update() {
		
		if(Input.GetKeyDown(KeyCode.Space) ) {
			Transform theNewBoulder = Instantiate<Transform> (boulderOriginal);
			theNewBoulder.gameObject.SetActive (true);
			theNewBoulder.position = transform.position; // now boulder's position is my position

			Rigidbody2D theNewBouldersRB = theNewBoulder.GetComponent<Rigidbody2D> ();
			theNewBouldersRB.AddForce (new Vector2(strength, strength));


		}
	}

}
