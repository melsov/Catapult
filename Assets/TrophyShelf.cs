﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class TrophyShelf : MonoBehaviour {

	protected Trophy[] trophies;
	public AchievementPanel achPanel;
    void OnEnable() {
        ScoreKeeper.Instance.OnReset += reset;
	}

    void OnDisable() {
        if(ScoreKeeper.Instance != null && ScoreKeeper.Instance.OnReset != null)
            ScoreKeeper.Instance.OnReset -= reset;
    }

    private void reset() {
        hideTrophies();
    }

    private void hideTrophies() {
        foreach(Trophy t in trophies) {
            t.hide();
        }
    }
	// Use this for initialization
	void Start () {
		trophies = GetComponentsInChildren<Trophy> ();
        hideTrophies();
        sortTrophies();
	}

    private void sortTrophies() {
        List<Trophy> temp = new List<Trophy>(trophies);
        List<Trophy> sorted = temp.OrderBy(t => t.requiredDucks).ToList<Trophy>();
        trophies = sorted.ToArray();
    }

	public void checkAchievements() {
        check();
	}

    private void check() {
		foreach (Trophy t in trophies) {
			if (!t.achieved && t.requiredDucks <= ScoreKeeper.Instance.getScore()) {
				t.achieved = true;
				t.appear ();
				StartCoroutine (achieve (t));
                return;
			}
		}
    }

	private IEnumerator achieve(Trophy t) {
		achPanel.show (t);
		Time.timeScale = .01f;
		yield return new WaitForSeconds (1f * Time.timeScale);
		achPanel.hide ();
		Time.timeScale = 1f;
	}

    public int achievementLevel {
        get {
            int result = 0;
            foreach(Trophy t in trophies) {
                if (t.achieved) result++;
            }
            return result;
        }
    }

}
