using UnityEngine;
using System.Collections;

public class DuckDispenser : MonoBehaviour {

	public Duck originalDuck;
	
	public float baseFrequency = 2f;
	private float timeSince = 0f;
	private float frequencyMultiplier = 1f;

	// Use this for initialization
	void Start () {
		
	}

	void OnEnable() {
		Duck.OnGotAHit += increaseFrequency;
	}

	void OnDisable() {
		Duck.OnGotAHit -= increaseFrequency;
	}
	
	// Update is called once per frame
	void Update () {
		timeSince += Time.deltaTime;
		if (timeSince > baseFrequency * frequencyMultiplier) {
			timeSince = 0f;
			shootttt ();
		}
	}

	public void increaseFrequency() {
		frequencyMultiplier = Mathf.Max (.05f, frequencyMultiplier * 0.96f);
	}

	private void shootttt() {
		Duck d = Instantiate<Duck> (originalDuck);
		d.transform.position = transform.position;
        //d.speed = Mathf.Min(55f, d.speed / frequencyMultiplier);
		d.gameObject.SetActive (true);

	}
}
