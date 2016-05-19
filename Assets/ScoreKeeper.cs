using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : Singleton<ScoreKeeper> {

	public Text text;
	protected ScoreKeeper() {
	}


	public void Awake() {
		Duck.OnGotAHit += registerAPoint;
	}

	public void Destroy() {
		Duck.OnGotAHit -= registerAPoint;
	}

	private int score;
	public int getScore() {
		return score;
	}

	public void registerAPoint() {
		score += 1;
		text.text = "" + score;
	}
}
