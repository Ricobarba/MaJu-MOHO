using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character1Controller : MonoBehaviour
{
    public float fx;
    public float maxSpeed = 60f;
    public float runSpeed = 12f;
    public float jumpSpeed = 13f;
    public float dashSpeed = 50f;
    public float dashTime = 0.1f;
    public float dashCoolDown = 1f;
    public float acceleration_x = 200f; 
    public bool facingRight = true;
    public bool isJumping = false;
    public bool isWallJumping = false;

    public float jumpTime = 0.35f;

    public float jumpTimeCounter;

    public bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    public bool walled = false;
    public Transform wallCheck;
    float wallRadius = 0.2f;
    public LayerMask whatIsWall;

    public bool backWalled = false;
    public Transform backWallCheck;

    public bool dashAvailable = true;
    public bool isDashing = false;
    float dashX=0;
    float dashY=0;

    public string horizontalStr = "Horizontal1";
    public string verticalStr = "Vertical1";
    public string jumpStr = "Jump1";
    public string dashStr = "Dash1";
    public string smashStr = "Smash1";
    public string guardStr = "Guard1";
    public string playerStr = "Player1";


    
    

    Animator anim;
    Rigidbody2D rigid;

    int playerLayer, platformLayer;
    bool jumpingOff = false;


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        playerLayer = LayerMask.NameToLayer(playerStr);
        platformLayer = LayerMask.NameToLayer("Plateform");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        anim.SetBool("FacingRight", facingRight);

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);

        walled = Physics2D.OverlapCircle(wallCheck.position, wallRadius, whatIsWall);
        anim.SetBool("Wall", walled);

        //backWalled = Physics2D.OverlapCircle(backWallCheck.position, wallRadius, whatIsWall);
        //anim.SetBool("BackWall", backWalled);

        anim.SetFloat("vSpeed", rigid.velocity.y);


        float moveX = Input.GetAxisRaw(horizontalStr);
        float moveY = Input.GetAxisRaw(verticalStr);

        
        if (isDashing)
        {
            dash();
        }

        //else if (isHitting)
        //  {public GameObject ball = GameObject.findWithTag("Ball")
        //  ball.setSpeed()

        else if (isWallJumping)
        {
            if (facingRight)
                wallJump(1);
            else
                wallJump(-1);
        }
        else
        {
            //Mouvement gauche droite
            anim.SetFloat("Speed", Mathf.Abs(moveX));
            moveLeftRight(moveX);


            //frein
            if (moveX == 0)
                brake();
        }
    }

    void Update()
    {
        // jump
        if (grounded && Input.GetButtonDown(jumpStr))
            jump();

        if ( Input.GetButton(jumpStr) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, (0.5f+(jumpTimeCounter/(0.5f*jumpTime))*jumpSpeed));
                jumpTimeCounter -= Time.deltaTime;
            }
            else
                isJumping = false; 
        }

        if (Input.GetButtonUp(jumpStr))
            isJumping = false;

        
        //wall jump
        if (walled && Input.GetButtonDown(jumpStr) && !grounded)
            StartCoroutine("wallJumpRoutine");

        //backWall jump
        if (backWalled && Input.GetButtonDown(jumpStr) && !grounded)
            StartCoroutine("wallJumpRoutine");


        //get down platform
        if (Input.GetAxisRaw(verticalStr)<0)
            StartCoroutine("JumpOff");

        //dash
        bool dashButton = Input.GetButtonDown(dashStr);
        if (dashButton && dashAvailable)
        {
            dashX = Input.GetAxisRaw(horizontalStr);
            dashY = Input.GetAxisRaw(verticalStr);
            StartCoroutine("dashRoutine");
        }
        

    }

    //***************************************************************************************
    //************************************ METHODS ******************************************
    //***************************************************************************************

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }

    void moveLeftRight(float move)
    {
        if (Mathf.Abs(rigid.velocity.x) < runSpeed || move * rigid.velocity.x < 0)
            rigid.AddForce(new Vector2(move * acceleration_x, 0));

        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();
    }

    void brake()
    {
         rigid.AddForce(new Vector2(-rigid.velocity.x * 20, 0));
    }

    void jump()
    {
        anim.SetBool("Ground", false);
        jumpTimeCounter = jumpTime;
        isJumping = true;
        rigid.velocity = new Vector2(rigid.velocity.x, jumpSpeed);
    }

    void wallJump(int direction)
    {
        anim.SetBool("Wall", false);
        fx = runSpeed*direction;
        rigid.velocity = new Vector2(fx, jumpSpeed);
    }

    void dash()
    {
        if (dashX != 0 || dashY != 0)
            rigid.velocity = new Vector2(dashSpeed * dashX, dashSpeed * dashY);
        else if (facingRight)
            rigid.velocity = new Vector2(dashSpeed, 0);
        else
            rigid.velocity = new Vector2(-dashSpeed, 0);
    }


    //***************************************************************************************
    //************************************ COROUTINES ***************************************
    //***************************************************************************************

    public IEnumerator JumpOff()
    {
        //jumpingOff = true;
        Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, true);
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, false);
        //jumpingOff = false;
    }

    public IEnumerator dashRoutine()
    {
        isWallJumping = false;
        isJumping = false;
        
        isDashing = true;
        dashAvailable = false;
        yield return new WaitForSeconds(dashTime);
        rigid.velocity = new Vector2(0, 0);
        isDashing = false;
        yield return new WaitForSeconds(dashCoolDown-dashTime);
        dashAvailable = true;
        

    }

    public IEnumerator wallJumpRoutine()
    {
        jumpTimeCounter = jumpTime;
        if (!backWalled)
            Flip();
        isJumping = true;
        isWallJumping = true;
        yield return new WaitForSeconds(0.2f);
        isWallJumping = false;
    }

}
