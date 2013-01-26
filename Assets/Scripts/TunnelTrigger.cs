using UnityEngine;
using System.Collections;

public class TunnelTrigger : MonoBehaviour {
	public Vector3 creationOffset;
	
	private GameObject _tunnelSection;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (_tunnelSection == null)
		{
			_tunnelSection = GameObject.Instantiate(Resources.Load(ResourceNames.TunnelSection)) as GameObject;
			_tunnelSection.transform.position = transform.parent.position + creationOffset;
		}
	}
}
