using UnityEngine;
using System.Collections;

public class Boulder : MonoBehaviour , IDestructable {

	private bool gotOne;
	[SerializeField]
	protected Shark shark;

	void OnCollisionEnter2D(Collision2D other) {
		Duck ducky = other.transform.GetComponent<Duck> ();
		if (ducky != null) {
			ducky.getHit ();
			gotOne = true;
		}
	}

//	void OnTriggerExit2D(Collision2D other) {
//		WorldBounds wb = other.transform.GetComponent<WorldBounds> ();
//		if (wb != null) {
//			print ("boulder exit " + gotOne);
//		}
//	}

	public void getDestroyed() {
		if (!gotOne) {
			shark.beDisappointed ();
		}
		Destroy (this.gameObject);
	}
}
