using UnityEngine;
using System.Collections;

public class HeartbeatAnimation : MonoBehaviour {
    public AnimationClip startAnimation;
    public AnimationClip endAnimation;

    private Messenger.Subscription<HeartbeatTriggerEventArgs> _heartbeatSub;
    private TempoPlayer _tempoPlayer;

	// Use this for initialization
	void Start () {
        _heartbeatSub = new Messenger.Subscription<HeartbeatTriggerEventArgs>(OnHeartBeat);
        Messenger.Instance.Subscribe(_heartbeatSub);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnHeartBeat(HeartbeatTriggerEventArgs eventArgs)
    {
        animation.Play(startAnimation.name);
    }
}
