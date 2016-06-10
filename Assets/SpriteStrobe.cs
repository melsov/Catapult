using UnityEngine;
using System.Collections;

public class SpriteStrobe : MonoBehaviour {

	SpriteRenderer sr;
	public Color strobeColor = Color.yellow;
	private Color defaultColor;
	// Use this for initialization
	public float strobeDuration = .4f;
	private float lastStrobeTime;
    public float strobeInterval = .02f;
	private bool shouldStrobe;
    private int strobeOn;

    void Start () {
		sr = GetComponent<SpriteRenderer> ();
		defaultColor = sr.color;
	}

	public void strobe() {
		lastStrobeTime = Time.fixedTime;
		shouldStrobe = true;
        StartCoroutine(strobeTime());
	}

    private IEnumerator strobeTime() {
        for(int i = 0; i < 12; ++i) {
            sr.color = i % 2 == 0 ? strobeColor : defaultColor;
            yield return new WaitForFixedUpdate();
            //yield return new WaitForSeconds(strobeInterval);
        }
        sr.color = defaultColor;
    }
	
	// Update is called once per frame
	//void FixedUpdate () {
	//	if (shouldStrobe) {
	//		sr.color = strobeOn? strobeColor : defaultColor;
	//		strobeOn = !strobeOn;
	//		if (Time.fixedTime - lastStrobeTime > strobeDuration) {
	//			shouldStrobe = false;
	//		}
	//	} else {
	//		sr.color = defaultColor;
	//	}
	//}
}
