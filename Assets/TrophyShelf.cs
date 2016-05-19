using UnityEngine;
using System.Collections;

public class TrophyShelf : MonoBehaviour {


	protected Trophy[] trophies;
	public AchievementPanel achPanel;
	// Use this for initialization
	void Start () {
		trophies = GetComponentsInChildren<Trophy> ();
	}

	private void checkAchievements() {
		foreach (Trophy t in trophies) {
			if (!t.achieved && t.requiredDucks <= ScoreKeeper.Instance.getScore ()) {
				t.achieved = true;
				t.appear ();
				StartCoroutine (achieve (t));
			}
		}
	}

	private IEnumerator achieve(Trophy t) {
		achPanel.show (t);
		Time.timeScale = .1f;
		yield return new WaitForSeconds (.4f);
		achPanel.hide ();
		Time.timeScale = 1f;
	}

	// Update is called once per frame
	void FixedUpdate () {
		checkAchievements ();
	}
}
