using UnityEngine;
using System.Collections.Generic;
using System;

public class TunnelGenerator : MonoBehaviour {

    public enum Direction
    {
        Left,
        Centre,
        Right
    }

    public List<Collider> colliders;

	// Use this for initialization
    void Start()
    {
        colliders = new List<Collider>();
        foreach (var direction in Enum.GetValues(typeof(Direction)))
        {
            colliders.Insert((int)direction, transform.Find(direction.ToString()+"Trigger").collider);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
