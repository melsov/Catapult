using UnityEngine;
using System.Collections;

public class Boulder : MonoBehaviour , IDestructable {

	private bool gotOne;
	[SerializeField]
	protected Shark shark;
    public int preciousness = 1;
    protected ParticleSystem _particleSystem;

    public void Awake() {
        awake();
    }
    protected virtual void awake() {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        if (_particleSystem) {
            //_particleSystem.gameObject.SetActive(false);
        }
    }

	void OnCollisionEnter2D(Collision2D other) {
        handleCollisionEnter(other);
	}

    protected virtual void handleCollisionEnter(Collision2D other) {
		Duck ducky = other.transform.GetComponent<Duck> ();
		if (ducky != null) {
            if (canHandleEvilness(ducky)) {
                ducky.getHit(this);
                reactToHit(ducky);
            } else {
                getStymiedByEvil(ducky);
            }
		}
    }

    protected virtual void getStymiedByEvil(Duck duck) {
        duck.rejectHit(this);
        getDestroyed();
    }

    protected virtual bool canHandleEvilness(Duck duck) {
        return duck.evilness < 1;
    }

    protected virtual void reactToHit(Duck duck) {
        gotOne = true;
        showParticles();
    }


	public void getDestroyed() {
		if (!gotOne) {
			shark.beDisappointed ();
		}
		Destroy (this.gameObject);
	}

    protected void showParticles() {
        if(_particleSystem) {
            _particleSystem.gameObject.SetActive(true);
        }
    }

    public virtual void doLaunchRoutine() {

    }

}
