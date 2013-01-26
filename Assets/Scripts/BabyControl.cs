using UnityEngine;
using System.Collections;

public class BabyControl : MonoBehaviour {

    public float moveSpeed = 1.0f;
	public float moveAcceleration = 1.0f;
    public float driftSpeed = 0.5f;
    public float driftAcceleration = 1.0f;

    Messenger.Subscription<KeyPressedEventArgs> _keyPressSub;
    Messenger.Subscription<KeyReleasedEventArgs> _keyReleaseSub;

	// Use this for initialization
	void Start () {
        _keyPressSub = new Messenger.Subscription<KeyPressedEventArgs>(onKeyPress);
        _keyReleaseSub = new Messenger.Subscription<KeyReleasedEventArgs>(onKeyRelease);
        Messenger.Instance.Subscribe(_keyPressSub);
        Messenger.Instance.Subscribe(_keyReleaseSub);
	}
	
	// Update is called once per frame
	void Update () {
	}

    void FixedUpdate()
    {
        if (rigidbody.velocity.z < driftSpeed)
		{
            rigidbody.AddForce(Vector3.forward * driftAcceleration, ForceMode.Acceleration);
		}
		
		if (Input.GetKey(KeyCode.W))
			if (rigidbody.velocity.z < moveSpeed)
        		rigidbody.AddForce(Vector3.forward * moveAcceleration, ForceMode.Acceleration);
		
		if (Input.GetKey(KeyCode.A))
			if (rigidbody.velocity.x > -moveSpeed)
        		rigidbody.AddForce(Vector3.left * moveAcceleration, ForceMode.Acceleration);
		
		if (Input.GetKey(KeyCode.D))
            if (rigidbody.velocity.x < moveSpeed)
        		rigidbody.AddForce(Vector3.right * moveAcceleration, ForceMode.Acceleration);
			
    }

    private void onKeyPress(KeyPressedEventArgs eventArgs)
    {
    }

    private void onKeyRelease(KeyReleasedEventArgs eventArgs)
    {
    }
}
