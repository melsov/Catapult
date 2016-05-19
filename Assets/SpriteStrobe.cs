using UnityEngine;
using System.Collections;

public class SpriteStrobe : MonoBehaviour {

	SpriteRenderer sr;
	public Color strobeColor = Color.yellow;
	private Color defaultColor;
	// Use this for initialization
	public float strobeDuration = .4f;
	private float lastStrobeTime;
	private bool shouldStrobe;
	private bool strobeOn;

	void Start () {
		sr = GetComponent<SpriteRenderer> ();
		defaultColor = sr.color;
	}

	public void strobe() {
		lastStrobeTime = Time.fixedTime;
		shouldStrobe = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (shouldStrobe) {
			sr.color = strobeOn? strobeColor : defaultColor;
			strobeOn = !strobeOn;
			if (Time.fixedTime - lastStrobeTime > strobeDuration) {
				shouldStrobe = false;
			}
		} else {
			sr.color = defaultColor;
		}
	}
}
