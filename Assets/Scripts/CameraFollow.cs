using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform targetTransform;
    public Vector3 offset = new Vector3(0, 0.2f, -2);
    public float moveAcceleration = 0.001f;
    public float moveSpeed = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 targetPosition = targetTransform.position + offset;

        if (targetPosition.x < transform.position.x)
            rigidbody.velocity = rigidbody.velocity - new Vector3(moveAcceleration, 0, 0);
        if (targetPosition.x > transform.position.x)
            rigidbody.velocity = rigidbody.velocity + new Vector3(moveAcceleration, 0, 0);

        if (targetPosition.y < transform.position.y)
            rigidbody.velocity = rigidbody.velocity - new Vector3(0, moveAcceleration, 0);
        if (targetPosition.y > transform.position.y)
            rigidbody.velocity = rigidbody.velocity + new Vector3(0, moveAcceleration, 0);

        if (targetPosition.z < transform.position.z)
            rigidbody.velocity = rigidbody.velocity - new Vector3(0, 0, moveAcceleration);
        if (targetPosition.z > transform.position.z)
            rigidbody.velocity = rigidbody.velocity + new Vector3(0, 0, moveAcceleration);

	}
}
