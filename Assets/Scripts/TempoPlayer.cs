using UnityEngine;
using System.Collections;

public class TempoPlayer : MonoBehaviour {
	
	public bool play = true;
	public float period = 1; //play sound after this period

	private float _elapsed = 0;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		_elapsed += Time.deltaTime;
		if (_elapsed > period && play) {
			_elapsed -= period;
			// Play sound
			audio.Play();
		}
	}
}
