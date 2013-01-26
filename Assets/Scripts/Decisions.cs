using UnityEngine;
using System.Collections.Generic;

public class Decisions : MonoBehaviour {
	private Messenger.Subscription<TunnelChoiceEventArgs> _choiceSub;
	private List<TunnelChoiceEventArgs> _tunnelChoices;
	
	// Use this for initialization
	void Start () {
		_tunnelChoices = new List<TunnelChoiceEventArgs>();
		_choiceSub = new Messenger.Subscription<TunnelChoiceEventArgs>(OnEnterChoiceTrigger);
		Messenger.Instance.Subscribe(_choiceSub);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void OnEnterChoiceTrigger(TunnelChoiceEventArgs eventArgs){
		_tunnelChoices.Add(eventArgs);
		Debug.Log ("Entered tunnel "+eventArgs.direction.ToString()+", correct:"+eventArgs.correctChoice.ToString());
	}
}
