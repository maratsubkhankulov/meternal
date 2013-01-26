using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform targetTransform;
    public Vector3 baseOffset = new Vector3(0, 0.2f, -1);
	public float smoothFactor = 0.05f;
	public float xTiltFactor = 1.3f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// Tilt with velocity
        Vector3 targetPosition = targetTransform.position + baseOffset - targetTransform.rigidbody.velocity;
		
		// Emphasis on x
		targetPosition.x -= targetTransform.rigidbody.velocity.x * xTiltFactor;
		
		transform.position = transform.position + (targetPosition - transform.position)* smoothFactor;
		transform.LookAt(targetTransform.position);
	}
}
