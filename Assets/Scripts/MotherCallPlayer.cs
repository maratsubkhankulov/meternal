using UnityEngine;
using System.Collections;

public class MotherCallPlayer : MonoBehaviour {
	
	private Messenger.Subscription<ChoiceTriggerEventArgs> _choiceSub;
	
	// Use this for initialization
	void Start () {
		_choiceSub = new Messenger.Subscription<ChoiceTriggerEventArgs>(OnEnterChoiceTrigger);
        Messenger.Instance.Subscribe(_choiceSub);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void OnEnterChoiceTrigger(ChoiceTriggerEventArgs eventArgs)
	{
		audio.Play();
	}
}
