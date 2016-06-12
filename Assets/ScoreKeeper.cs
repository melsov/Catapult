using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScoreKeeper :  Singleton<ScoreKeeper> {

    [SerializeField]
    private TrophyShelf trophyShelf;
    [SerializeField]
    private LosePanel losePanel;
    private bool canLose = false;

    public delegate void Reset();
    public Reset OnReset;

    public Text text;
	protected ScoreKeeper() {}

	public void Awake() {
        losePanel.hide();
		Duck.OnGotAHit += registerAPoint;
        Duck.OnNeverGotHit += applyPenalty;
    }

    public void Destroy() {
		Duck.OnGotAHit -= registerAPoint;
        Duck.OnNeverGotHit -= applyPenalty;
	}

	private int _score;
    private int score {
        get { return _score; }
        set {
            _score = value;
            if (text != null)
                text.text = "" + _score;
            trophyShelf.checkAchievements();
            if (!canLose) {
                canLose = _score > 0;
            } else if (_score < 0) {
                lose("Score became negative");
            }
        }
    }
	public int getScore() {
		return score;
	}

	public void registerAPoint(Boulder boulder) {
		score += boulder.preciousness;
	}

    private void applyPenalty() {
        score -= 1;
    }

	public void lose(string because) {
        StartCoroutine(showAndLose(because));
	}

    private IEnumerator showAndLose(string because) {
        losePanel.show(because);
        yield return new WaitForSeconds(3f);
        losePanel.hide();
        reset();
        OnReset();
    }

    private void reset() {
        canLose = false;
        _score = 0;
        text.text = "";
    }
}
