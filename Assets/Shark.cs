using UnityEngine;
using System.Collections;

public class Shark : MonoBehaviour {

	protected Animator animator;
	protected Animation animation;
	protected AudioSource audioSource;
	[SerializeField]
	protected SpriteStrobe bgStrobe;

	void OnEnable() {
		Duck.OnGotAHit += celebrate;
	}

	void OnDisable() {
		Duck.OnGotAHit -= celebrate;
	}

	protected void celebrate() {
		animator.SetBool ("Excited", true);
		audioSource.Play ();
		bgStrobe.strobe ();
	}

	public void beDisappointed() {
		StartCoroutine (disappointed ());
	}

	private IEnumerator disappointed() {
		animator.SetBool ("Ashamed", true);
		yield return new WaitForSeconds (1f);
		animator.SetBool ("Ashamed", false);
	
	}

	public void ashamedEnded() {
	}

	void Awake () {
		animator = GetComponent<Animator> ();
		audioSource = GetComponent<AudioSource> ();
	}
	
	void Update () {
		
	}

	public void animationEnded() {
		animator.SetBool ("Excited", false);
	}
}
