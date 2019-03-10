using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSmashTheBall : ActionScript
{
    public Character1Controller charac;

    

    public override IEnumerator DoActionOnEvent(MonoBehaviour sender, GameObject args)
    {
        if (_events != null)
        {
            foreach (var ev in _events)
            {
                Rigidbody2D bumpedRigidbody = args.GetComponent<Rigidbody2D>();
                if (bumpedRigidbody != null && !bumpedRigidbody.isKinematic)
                {
                    yield return new WaitForFixedUpdate();
                    if (Input.GetButtonUp("Smash1"))
                    {
                        //bumpedRigidbody.velocity = new Vector2(10f, charac.smashSpeed);
                        charac.smash(bumpedRigidbody);
                    }
                }
            }
            
        }
        yield return null;

    }
}
