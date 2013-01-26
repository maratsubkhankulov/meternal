using UnityEngine;
using System.Collections;

public class HeartbeatTrigger : MonoBehaviour {

    private Messenger.Subscription<HeartbeatTriggerEventArgs> _heartbeatSub;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        Messenger.Instance.SendMessage(new HeartbeatTriggerEventArgs());
    }
}
