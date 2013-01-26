using UnityEngine;
using System.Collections;

public class BabyControl : MonoBehaviour {

    public float moveSpeed = 20.0f;
    public float driftSpeed = 1.0f;
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
            rigidbody.AddForce(Vector3.forward * driftAcceleration, ForceMode.Acceleration);
    }

    private void onKeyPress(KeyPressedEventArgs eventArgs)
    {
        switch (eventArgs.KeyCode)
        {
            case KeyCode.W:
                rigidbody.AddForce(Vector3.forward * moveSpeed);
                break;
        }
    }

    private void onKeyRelease(KeyReleasedEventArgs eventArgs)
    {
    }
}
