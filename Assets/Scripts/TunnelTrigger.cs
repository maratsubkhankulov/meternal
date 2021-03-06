using UnityEngine;
using System.Collections;

public class TunnelTrigger : MonoBehaviour {

    private TunnelSection _tunnelTrigger;

	// Use this for initialization
	void Start () {
        _tunnelTrigger = transform.parent.GetComponent<TunnelSection>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        _tunnelTrigger.OnChildTriggerEnter(this.collider, other);
    }
}
