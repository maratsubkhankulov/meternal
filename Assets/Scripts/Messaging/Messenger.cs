using System;
using System.Collections.Generic;
using UnityEngine;

public enum MessagePriority
{
    Low,
    Medium,
    High,
    Default
}

public class Messenger : MonoBehaviour {

    private static readonly Dictionary<Type, List<ISubscription>> Subscriptions = new Dictionary<Type, List<ISubscription>>();
    private static readonly List<InternalMessage> DelayedMessages = new List<InternalMessage>();
	
    public interface ISubscription
    {
        Type EventArgsType { get; }

        void Invoke(MessageEventArgs e);
    }

    public static Messenger Instance { get; private set; }

    public void Subscribe(ISubscription subscription)
    {
        var typeKey = subscription.EventArgsType;

        List<ISubscription> list;
        if (Subscriptions.ContainsKey(typeKey))
        {
            list = Subscriptions[typeKey];
        }
        else
        {
            list = new List<ISubscription>();
            Subscriptions.Add(typeKey, list);
        }

        list.Add(subscription);
    }

    public void Unsubscribe(ISubscription subscription)
    {
        var typeKey = subscription.EventArgsType;
        if (Subscriptions.ContainsKey(typeKey))
        {
            var list = Subscriptions[typeKey];
            list.Remove(subscription);
        }
    }

    public void SendMessage(MessageEventArgs eventArgs)
    {
        var internalMessage = new InternalMessage(eventArgs);
        if (internalMessage.Instant)
            Invoke(internalMessage);
        else
            AddToDelayedMessages(internalMessage);
    }

    private void AddToDelayedMessages(InternalMessage internalMessage)
    {
        DelayedMessages.Add(internalMessage);
		
		// Don't need to sort single message
        if (DelayedMessages.Count >= 2)
            DelayedMessages.Sort((message1, message2) => message1.AbsDeliveryTime.CompareTo(message2.AbsDeliveryTime));
    }

    private void Invoke(InternalMessage internalMessage)
    {
        DebugUtils.Log(string.Format("Dispatching message: {0}, Instant: {1}{2}", internalMessage.EventArgs,
                                     internalMessage.Instant,
                                     internalMessage.Instant ? "" : ", Priority: " + internalMessage.Priority));

        var eventArgs = internalMessage.EventArgs;
        var typeKey = eventArgs.GetType();
        if (Subscriptions.ContainsKey(typeKey))
        {
            var list = Subscriptions[typeKey];
            foreach (var messageSubscription in list)
            {
                messageSubscription.Invoke(eventArgs);
            }
        }
    }
    
    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var readyToGo = new List<InternalMessage>();

        while (DelayedMessages.Count > 0 && DelayedMessages[0].AbsDeliveryTime <= Time.time)
        {
            readyToGo.Add(DelayedMessages[0]);
            DelayedMessages.RemoveAt(0);
        }

        // Sort from largest priority to smallest
        readyToGo.Sort((message1, message2) => -message1.Priority.CompareTo(message2.Priority));

	    foreach (var delayedMessage in readyToGo)
	    {
	        Invoke(delayedMessage);
        }

        // DebugUtils.Log("Frame end.");
	}

    public class Subscription
    {
        protected static int NextId { get; set; }
    }

    public class Subscription<T> : Subscription, ISubscription where T : MessageEventArgs
	{

	    public Subscription(Callback handler)
		{
			Handler = handler;
		    EventArgsType = typeof (T);
	        Id = NextId++;
		}

        public delegate void Callback(T eventArgs);

        public Callback Handler { get; set; }
        public Type EventArgsType { get; private set; }
        public int Id { get; private set; }

        public void Invoke(MessageEventArgs eventArgs)
        {
            var e = eventArgs as T;
            if (e != null)
                Handler(e);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Subscription<T>);
        }

	    public bool Equals(Subscription<T> subscription)
	    {
            if (subscription != null)
                return (subscription.EventArgsType == EventArgsType &&
                        subscription.Handler == Handler);
	        return false;
	    }

        public override int GetHashCode()
        {
            return Handler.GetHashCode();
        }
	}

    private class InternalMessage
    {
        public InternalMessage(MessageEventArgs eventArgs)
        {
            EventArgs = eventArgs;
            Priority = (int) eventArgs.Priority;
            AbsDeliveryTime = Time.time + eventArgs.DelayTime;
            if (eventArgs.Priority == MessagePriority.Default && Math.Abs(eventArgs.DelayTime - 0) < float.Epsilon)
            {
                Instant = true;
            }
            else
                Instant = false;
        }

        public MessageEventArgs EventArgs { get; private set; }

        public int Priority { get; private set; }
        public float AbsDeliveryTime { get; private set; }
        public bool Instant { get; private set; }
    }
}

public class MessageEventArgs : EventArgs
{
    private MessagePriority _priority = MessagePriority.Default;

    /// <summary>
    /// Default priority messages are sent immediately.
    /// Prioritised messages are sent at the end of a frame, as per their DelayTime and Priority.
    /// </summary>
    public MessagePriority Priority
    {
        get { return _priority; }
        set { _priority = value; }
    }

    /// <summary>
    /// Delay time in seconds relative to message creation.
    /// </summary>
    public float DelayTime { get; set; }
}




