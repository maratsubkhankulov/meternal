using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform targetTransform;
    public Vector3 baseOffset = new Vector3(0, 0.2f, -0.5f);
	public float smoothFactor = 0.05f;
	public float xTiltFactor = 1.3f;
    public bool linkByCollider = true;

    private GameObject _linkObject;
    private BoxCollider _linkCollider;

	// Use this for initialization
	void Start () {
        if (linkByCollider)
        {
            _linkObject = new GameObject();

            _linkObject.name = "LinkObject";
            _linkObject.layer = LayerMask.NameToLayer(Layers.CameraLink);
            _linkObject.transform.parent = this.transform;
            _linkObject.transform.localPosition = Vector3.zero;

            _linkCollider = _linkObject.AddComponent<BoxCollider>();
            _linkCollider.size = new Vector3(0.1f, 0.1f, 1);
            _linkCollider.center = new Vector3(0, 0, -baseOffset.z);
        }
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
