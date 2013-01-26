using UnityEngine;
using System.Collections;

public class ChoiceTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
		if (other.tag.Equals(Tags.Baby))
		{
	        Messenger.Instance.SendMessage(new ChoiceTriggerEventArgs());
			Debug.Log("ChoiceTrigger: entered");
		}
    }
}
