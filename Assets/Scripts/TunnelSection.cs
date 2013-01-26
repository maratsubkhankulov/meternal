using UnityEngine;
using System.Collections.Generic;

public class TunnelSection : MonoBehaviour {

    public ColliderVector[] tunnelTriggers;
	
	private GameObject _tunnelSection;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnChildTriggerEnter(Collider child, Collider other)
    {
        foreach (var trigger in tunnelTriggers)
        {
            if (trigger.collider.GetInstanceID() == child.collider.GetInstanceID())
            {
                if (_tunnelSection == null)
                {
                    // Destroy the previous junction
                    if (transform.parent != null)
                    {
                        Debug.Log("Destroy");
                        var previousSection = transform.parent.gameObject;
                        transform.parent = null;
                        GameObject.Destroy(previousSection);
                    }

                    var randomNo = RandomUtil.random.Next(0, 2);
                    DebugUtils.Log(randomNo);
                    var tunnelName = randomNo == 1 ? ResourceNames.TunnelSection1 : ResourceNames.TunnelSection2;
                    // Create the next one
                    _tunnelSection = GameObject.Instantiate(Resources.Load(tunnelName)) as GameObject;
                    // Position it for the appropriate branch
                    _tunnelSection.transform.position = transform.position + trigger.creationOffset;
                    // Set the the next section as the parent of this section
                    _tunnelSection.transform.parent = this.transform;
                }
            }
        }
    }

	void OnTriggerEnter(Collider other)
	{
		
	}

    public enum Directions
    {
        Left,
        Center,
        Right,
    }

    [System.Serializable]
    public class ColliderVector
    {
        public Collider collider;
        public Vector3 creationOffset;
        public Directions direction;
    }
}