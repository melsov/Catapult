using UnityEngine;
using System.Collections;

using UnityEngine.UI;
public class SpriteStrobe : MonoBehaviour {

	SpriteRenderer sr;
    Image image;
	public Color strobeColor = Color.yellow;
	private Color defaultColor;
	// Use this for initialization
	public float strobeDuration = .4f;
	private float lastStrobeTime;
    public float strobeInterval = .02f;
	private bool shouldStrobe;
    private int strobeOn;

    void Start () {
		//sr = GetComponent<SpriteRenderer> ();
		//defaultColor = sr.color;
        image = GetComponent<Image>();
        defaultColor = image.color;
	}

	public void strobe() {
		lastStrobeTime = Time.fixedTime;
		shouldStrobe = true;
        StartCoroutine(strobeTime());
	}

    private IEnumerator strobeTime() {
        for(int i = 0; i < 12; ++i) {
            image.color =  i % 2 == 0 ? strobeColor : defaultColor;
            //sr.color = i % 2 == 0 ? strobeColor : defaultColor;
            yield return new WaitForFixedUpdate();
        }
        image.color = defaultColor;
        //sr.color = defaultColor;
    }

}
