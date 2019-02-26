using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ActionGetStun : ActionScript
{

    public Character1Controller charac;

    public float stunTime = 1.5f;
    public float ballSpeedMin = 10000f;

    public GameObject ball;
    public Rigidbody2D ballrb;

    public float ballSpeedSquare;
    public float ballSpeedX;
    public float ballSpeedY;

   // private  void Start()
    //{
        
    //}
    

    public override IEnumerator DoActionOnEvent(MonoBehaviour sender, GameObject args)
    {
        if (_events != null)
        {
            ball = GameObject.FindGameObjectWithTag("Ball");
            ballrb = ball.GetComponent<Rigidbody2D>();

            ballSpeedX = ballrb.velocity.x;
            ballSpeedY = ballrb.velocity.y;

            ballSpeedSquare = ballSpeedX * ballSpeedX + ballSpeedY * ballSpeedY;

            foreach (var ev in _events)
            {
                if (ev != null && !charac.isDashing && ballSpeedSquare>=ballSpeedMin)
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