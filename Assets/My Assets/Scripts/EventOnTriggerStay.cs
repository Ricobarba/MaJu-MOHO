using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventOnTriggerStay : EventScript
{
    // Use this for initialization
    void OnTriggerStay2D(Collider2D col)
    {

        OnTriggered(this, col.gameObject);
    }
}
