using UnityEngine;
using System.Collections;

public class EventOnTriggerEnter : EventScript {

    public ScoreScript scoreScript;

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D col) {
	    
		OnTriggered(this, col.gameObject);
	}
}
