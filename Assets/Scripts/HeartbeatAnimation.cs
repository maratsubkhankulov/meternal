using UnityEngine;
using System.Collections;

public class HeartbeatAnimation : MonoBehaviour {
    public AnimationClip startAnimation;
    public AnimationClip endAnimation;

    private Messenger.Subscription<HeartbeatTriggerEventArgs> _heartbeatSub;
    private Messenger.Subscription<TunnelChoiceEventArgs> _tunnelChoiceSub;
    private TempoPlayer _tempoPlayer;

	// Use this for initialization
	void Start () {
        _heartbeatSub = new Messenger.Subscription<HeartbeatTriggerEventArgs>(OnHeartBeat);
        _tunnelChoiceSub = new Messenger.Subscription<TunnelChoiceEventArgs>(OnTunnelChoice);
        Messenger.Instance.Subscribe(_heartbeatSub);
        Messenger.Instance.Subscribe(_tunnelChoiceSub);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnHeartBeat(HeartbeatTriggerEventArgs eventArgs)
    {
        animation.Play(startAnimation.name);
    }

    private void OnTunnelChoice(TunnelChoiceEventArgs eventArgs)
    {
        animation.Play(endAnimation.name);
    }
}
