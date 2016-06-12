using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DuckDispenser : MonoBehaviour {

    [SerializeField]
    protected DuckOriginals originals;
    protected ChoiceMode choiceMode;

	public float baseFrequency = 2f;
	private float frequencyMultiplier = 1f;
    private float speedMultiplier = 1f;

    private int[] intensityMilestoneIntervals;

    private bool shouldDispense = true;
    [SerializeField]
    private int baseIntensityInterval = 2;

    public void Awake() {
        foreach (Duck d in GetComponentsInChildren<Duck>()) {
            if (d.isClone) { continue; }
            d.gameObject.SetActive(false);
        }
        choiceMode = new ChoiceMode();
        setupMilestoneIntervals();
    }
    private void setupMilestoneIntervals() {
        intensityMilestoneIntervals = new int[choiceMode.Length];
        for(int i = 0; i < choiceMode.Length; ++i) {
            intensityMilestoneIntervals[i] = (i + 1) * baseIntensityInterval + (int)Mathf.Pow((float)(baseIntensityInterval * i), 1.2f);
            print(intensityMilestoneIntervals[i]);
        }
    }

    void Start () {
        StartCoroutine(dispense());
	}

	void OnEnable() {
		Duck.OnGotAHit += increaseFrequency;
        Duck.OnNeverGotHit += decreaseFrequency;
        ScoreKeeper.Instance.OnReset += reset;
	}

    void OnDisable() {
		Duck.OnGotAHit -= increaseFrequency;
        Duck.OnNeverGotHit -= decreaseFrequency;
        if (ScoreKeeper.Instance && ScoreKeeper.Instance.OnReset != null) {
            ScoreKeeper.Instance.OnReset -= reset;
        }
	}

    private void reset() {

    }

    public void OnDestroy() {
        shouldDispense = false;
    }

    private IEnumerator dispense() {
        while(shouldDispense) {
            shootttt();
            yield return new WaitForSeconds(baseFrequency * frequencyMultiplier);
        }
    }

	public void increaseFrequency(Boulder boulder) {
		frequencyMultiplier = Mathf.Max (.05f, frequencyMultiplier * 0.96f);
        if (frequencyMultiplier < .06f) {
            frequencyMultiplier = 1f;
        }
        updateSpeed(true);
        udpateIntensity();
	}

    private void udpateIntensity() {
        int total = 0;
        for(int i = 0; i < intensityMilestoneIntervals.Length; ++i) {
            total += intensityMilestoneIntervals[i];
            if (ScoreKeeper.Instance.getScore() > total) {
                continue;
            }
            choiceMode.setIntensity(i);
            break;
        }
    }

    private void decreaseFrequency() {
        frequencyMultiplier = Mathf.Min(1f, frequencyMultiplier * 1.05f);
        updateSpeed(false);
    }

    private void updateSpeed(bool positive) {
        if (positive) {
            speedMultiplier = Mathf.Min(2.4f, speedMultiplier + .1f);
        } else {
            speedMultiplier = Mathf.Max(1f, speedMultiplier - .05f);
        }
    }

	private void shootttt() {
        Duck d = Instantiate<Duck>(originals[choiceMode.getPick()]); // originals[0]);
        d.transform.parent = null;
		d.transform.position = transform.position;
        d.isClone = true;
        d.speed *= speedMultiplier;
		d.gameObject.SetActive (true);
	}


}

[System.Serializable]
public struct DuckOriginals
{
    public Duck duck;
    public BobbingDuck bobbingDuck;
    public RocketDuck rocketDuck;
    public EvilDuck evilDuck;

    public Duck this[int i] {
        get {
            switch(i) {
                case 0:
                    return duck;
                case 1:
                    return bobbingDuck;
                case 2:
                    return rocketDuck;
                case 3:
                    return evilDuck;
                default:
                    break;
            }
            return null;
        }
    }
    
}
