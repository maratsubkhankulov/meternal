using System;
using UnityEngine;

public class InputManager : MonoBehaviour {

    private static readonly Array KeyCodeValues = Enum.GetValues(typeof(KeyCode));
	private static int _numberKeysPressed;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        DebugUtils.Assert(_numberKeysPressed >= 0);

        if (!Input.anyKey && _numberKeysPressed == 0) return;
		
        foreach (var keyCodeValue in KeyCodeValues)
	    {
	        if (Input.GetKeyDown((KeyCode)keyCodeValue))
	        {
                Messenger.Instance.SendMessage(new KeyPressedEventArgs((KeyCode)keyCodeValue));
	            // DebugUtils.Log("Key pressed" + keyCodeValue);
	            _numberKeysPressed += 1;
            }
            
            if (Input.GetKeyUp((KeyCode)keyCodeValue))
	        {
                Messenger.Instance.SendMessage(new KeyReleasedEventArgs((KeyCode)keyCodeValue));
                // DebugUtils.Log("Key released" + keyCodeValue);
				_numberKeysPressed -= 1;
	        }
	    }
	}
}

public class KeyPressedEventArgs : MessageEventArgs
{
    public KeyPressedEventArgs(KeyCode keyCodeValue)
    {
        KeyCode = keyCodeValue;
    }

    public KeyCode KeyCode { get; set; }
}

public class KeyReleasedEventArgs : MessageEventArgs
{
    public KeyReleasedEventArgs(KeyCode keyCodeValue)
    {
        KeyCode = keyCodeValue;
    }

    public KeyCode KeyCode { get; set; }
}