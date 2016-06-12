using UnityEngine;
using System.Collections;

public class RocketDuck : Duck {

    public AnimationCurve curve;

	public void awake() {
        bobMovement = false;
    }
   
    protected override void move() {
        if (rb) {

            Vector2 f =  new Vector2(speed * curve.Evaluate(Mathf.Max(0f, normalizedPositionX)), 0f);// force * Mathf.Max(.2f, normalizedPositionX);
            //f = Vector2.ClampMagnitude(f, 45f);
            rb.AddForce(f);
        }
    }
}
