using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CatapultClicky : MonoBehaviour 
{
	//public Boulder boulderOriginal;
    public Boulder[] boulderPrefabs;
    public TrophyShelf trophyShelf;

	public float strength = 5000f;
    protected Boulder boulderInWaiting;
    private List<float> requests = new List<float>();
    private BoulderChoiceMode choiceMode = new BoulderChoiceMode();

    public void Awake() {
        foreach(Boulder b in boulderPrefabs) {
            b.gameObject.SetActive(false);
        }
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            requests.Add(Time.realtimeSinceStartup);
        }
    }

    private Boulder getNextBoulder() {
        choiceMode.setIntensity(trophyShelf.achievementLevel);
        return boulderPrefabs[choiceMode.getPick()];
    }

	public void FixedUpdate() {
		if (boulderInWaiting == null) {
            boulderInWaiting = Instantiate<Boulder>(getNextBoulder());
            boulderInWaiting.GetComponent<Rigidbody2D>().gravityScale = 0;
            boulderInWaiting.transform.position = transform.position;
            boulderInWaiting.gameObject.SetActive(true);
        }
		if (shouldShoot()) { // if(Input.GetKeyDown(KeyCode.Space) ) {
            throwBoulder(boulderInWaiting, transform.position, new Vector2(strength, strength));
   //         Boulder theNewBoulder = boulderInWaiting; // Instantiate<Transform> (boulderOriginal);
			//Rigidbody2D theNewBouldersRB = theNewBoulder.GetComponent<Rigidbody2D> ();
   //         theNewBouldersRB.gravityScale = 5;
			//theNewBouldersRB.AddForce (new Vector2(strength, strength));
            boulderInWaiting = null;
		}
	}

    public static void throwBoulder(Boulder theNewBoulder, Vector2 startPos, Vector2 direction) {
        //Boulder theNewBoulder = boulderInWaiting; // Instantiate<Transform> (boulderOriginal);
        Rigidbody2D theNewBouldersRB = theNewBoulder.GetComponent<Rigidbody2D> ();
        theNewBouldersRB.velocity = Vector2.zero;
        theNewBouldersRB.gravityScale = 5;
        theNewBoulder.transform.position = startPos;
        theNewBouldersRB.AddForce (direction);
        theNewBoulder.doLaunchRoutine();
    }

    private bool shouldShoot() {
        if (requests.Count > 0) {
            float requestTime = requests[requests.Count - 1];
            if (Time.realtimeSinceStartup - requestTime > Time.fixedDeltaTime) {
                requests.RemoveAt(requests.Count - 1);
                return true;
            }
        }
        return false;
    }

}
