using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ActionGetStun : ActionScript
{

    public Character1Controller charac;

    public float stunTime = 1f;
    public float ballSpeedSquareMin = 900f;

    public GameObject ball;
    public Rigidbody2D ballrb;

    public float ballSpeedSquare;
    public float ballSpeedX;
    public float ballSpeedY;    

    public override IEnumerator DoActionOnEvent(MonoBehaviour sender, GameObject args)
    {
        if (_events != null)
        {
            

            //ballSpeedX = ballrb.velocity.x;
            //ballSpeedY = ballrb.velocity.y;

            //ballSpeedSquare = ballSpeedX * ballSpeedX + ballSpeedY * ballSpeedY;

            foreach (var ev in _events)
            {
                Rigidbody2D ballrb = args.GetComponent<Rigidbody2D>();

                ballSpeedX = ballrb.velocity.x;
                ballSpeedY = ballrb.velocity.y;

                ballSpeedSquare = ballSpeedX * ballSpeedX + ballSpeedY * ballSpeedY;

                if (ev != null && !charac.isDashing && ballSpeedSquare>=ballSpeedSquareMin)
                {
                    charac.isStun = true;
                    yield return new WaitForSeconds(stunTime);
                    charac.isStun = false;
                }
                    
            }
        }
        yield return null;

    }


}