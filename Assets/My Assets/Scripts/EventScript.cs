using UnityEngine;
using System.Collections;

public abstract class EventScript : MonoBehaviour
{

    public delegate void EventDelegate(MonoBehaviour sender, GameObject args);

    [SerializeField]
    public EventDelegate _triggered;

    public void OnTriggered(MonoBehaviour sender, GameObject args)
    {
        if (_triggered != null)
            _triggered(sender, args);
    }
}
