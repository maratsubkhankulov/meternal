using UnityEngine;
using System.Collections.Generic;

public class TunnelSection : MonoBehaviour {

    public ColliderVector[] tunnelTriggers;
	public TunnelEntranceVector[] tunnelEntrances;
	
	private GameObject _tunnelSection;
	private int correctTunnelID;
	private Directions correctDirection;
	
	// Chose the correct tunnel, create audioSource, create visual cue
	void Start () {
		int tunnelIndex = RandomUtil.random.Next(0, tunnelTriggers.Length);
		correctDirection = tunnelTriggers[tunnelIndex].direction;
		Debug.Log ("Correct tunnel is: "+correctDirection);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnChildTriggerEnter(Collider child, Collider other)
    {
        foreach (var trigger in tunnelTriggers)
        {
            if (trigger.collider.GetInstanceID() == child.collider.GetInstanceID() && other.tag.Equals(Tags.Baby))
            {
				if (trigger.direction.Equals(correctDirection))
				{
					//Send choice message
					Messenger.Instance.SendMessage(new TunnelChoiceEventArgs(trigger.direction, true));
				} else {
					//Send choice message
					Messenger.Instance.SendMessage(new TunnelChoiceEventArgs(trigger.direction, false));
				}
                if (_tunnelSection == null)
                {
                    // Destroy the previous junction
                    if (transform.parent != null)
                    {
                        ///Debug.Log("Destroy");
                        var previousSection = transform.parent.gameObject;
                        transform.parent = null;
                        GameObject.Destroy(previousSection);
                    }

                    var randomNo = RandomUtil.random.Next(0, 2);
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
	
	[System.Serializable]
    public class TunnelEntranceVector
    {
        public Vector3 offset;
        public Directions direction;
    }
}