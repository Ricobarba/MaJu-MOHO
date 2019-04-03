using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character1Controller : MonoBehaviour
// smash : pas d'inertie après un dash
// Si choc pendant is charging pas de frein
// Commence à charger smash pendant stun --> ???
{
    public float fx;
    public float maxSpeed = 40f;
    public float runSpeed = 12f;
    public float jumpSpeed = 13f;
    public float dashSpeed = 50f;
    public float dashTime = 0.1f;
    public float dashCoolDown = 1f;
    public float acceleration_x = 200f; 
    public bool facingRight = true;

    public bool isStun=false;

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

    public bool isAiming = false;
    public float aimX;
    public float aimY;

    public float smashMinSpeed = 10f;
    public float smashSpeed = 25f;
    public float smashCharge;
    public float maxCharge = 0.5f;
    public float smashCooldown = 0.5f;
    
    public bool isSmashing = false;
    public bool canBall = false;
    public Transform ballCheck;
    float ballRadius = 1.2f;
    public LayerMask whatIsBall;

    public string horizontalStr = "Horizontal1";
    public string verticalStr = "Vertical1";
    public string jumpStr = "Jump1";
    public string dashStr = "Dash1";
    public string smashStr = "Smash1";
    public string guardStr = "Guard1";
    public string playerStr = "Player1";

    public string getStunnedStr = "GetStunned1";


    public GameObject ball;
    public Rigidbody2D ballrb;
    

    Animator anim;
    Rigidbody2D rigid;

    int playerLayer, platformLayer;
    bool jumpingOff = false;


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        ball = GameObject.FindGameObjectWithTag("Ball");
        ballrb = ball.GetComponent<Rigidbody2D>();

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

        canBall = Physics2D.OverlapCircle(ballCheck.position, ballRadius, whatIsBall);

        backWalled = Physics2D.OverlapCircle(backWallCheck.position, wallRadius, whatIsWall);
        


        float moveX = Input.GetAxisRaw(horizontalStr);
        float moveY = Input.GetAxisRaw(verticalStr);


        /// Stunned
        if (isStun)
            brake();

        else if (isDashing)
            dash();

        // Hitting the ball
        else if (isAiming)
        {
            rigid.AddForce(-6.5f * Physics2D.gravity * rigid.mass);
            smashCharge += Time.deltaTime;
            if (moveX > 0 && !facingRight)
                Flip();
            else if (moveX < 0 && facingRight)
                Flip();
        }

        // Moving
        else
        {
            if (isWallJumping)
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

        //chute
        if (rigid.velocity.y < 0)
        {
            rigid.velocity = new Vector2 (rigid.velocity.x, Mathf.Max (-maxSpeed, rigid.velocity.y)) ;
        }
    }

    void Update()
    {
        // jump
        if (grounded && Input.GetButtonDown(jumpStr) && !isStun  && !isAiming)
            jump();

        if ( Input.GetButton(jumpStr) && isJumping && !isStun)
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
        if (walled && Input.GetButtonDown(jumpStr) && !grounded && !isAiming && !isStun)
            StartCoroutine("wallJumpRoutine");

        //backWall jump
        if (backWalled && Input.GetButtonDown(jumpStr) && !grounded && !isAiming && !isStun)
            StartCoroutine("wallJumpRoutine");


        //get down platform
        if (Input.GetAxisRaw(verticalStr)<0 && !isAiming && !isStun)
            StartCoroutine("JumpOff");

        //dash
        bool dashButton = Input.GetButtonDown(dashStr);
        if (dashButton && dashAvailable && !isStun)
        {
            dashX = Input.GetAxisRaw(horizontalStr);
            dashY = Input.GetAxisRaw(verticalStr);
            StartCoroutine("dashRoutine");
        }

        //smash
        //preparing to smash
        if (Input.GetButtonDown(smashStr) && !isStun)
        {
            freeze();
            isAiming = true;
            smashCharge = 0;
            isJumping = false;
        }

        //smashing
        if (Input.GetButtonUp(smashStr) && isAiming)
        {
            /*if (canBall && !isStun)
            {
                aimX = Input.GetAxis(horizontalStr);
                aimY = Input.GetAxis(verticalStr);
                //smash();
            }*/
            isAiming = false;
            StartCoroutine("getStunned", smashCooldown);
        }

        if (Input.GetButtonDown(getStunnedStr))
            StartCoroutine("getStunned", 2f);


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
        isAiming = false;
    }

    void freeze()
    {
        rigid.velocity = new Vector2(rigid.velocity.x * 0.1f, rigid.velocity.y * 0.1f);
    }

    /*void smash()
    {
        if (smashCharge > maxCharge)
            smashCharge = 1;
        else
            smashCharge = smashCharge / maxCharge;
        if (aimX != 0 || aimY != 0)
            ballrb.velocity = new Vector2(smashCharge * smashSpeed * aimX / Mathf.Sqrt(aimX * aimX + aimY * aimY), smashCharge * smashSpeed * aimY / Mathf.Sqrt(aimX * aimX + aimY * aimY));
        else if (facingRight)
            ballrb.velocity = new Vector2(smashCharge * smashSpeed, 0);
        else
            ballrb.velocity = new Vector2(-smashCharge * smashSpeed, 0);
    }
    */

    public void smash(Rigidbody2D ballrb)
    {
        if (isAiming)
        {
            aimX = Input.GetAxis(horizontalStr);
            aimY = Input.GetAxis(verticalStr);
            if (smashCharge > maxCharge)
                smashCharge = 1;
            else
                smashCharge = smashCharge / maxCharge;
            if (aimX != 0 || aimY != 0)
                ballrb.velocity = new Vector2((smashMinSpeed + smashCharge * (smashSpeed - smashMinSpeed)) * aimX / Mathf.Sqrt(aimX * aimX + aimY * aimY),
                                              (smashMinSpeed + smashCharge * (smashSpeed - smashMinSpeed)) * aimY / Mathf.Sqrt(aimX * aimX + aimY * aimY));
            else if (facingRight)
                ballrb.velocity = new Vector2(smashMinSpeed + smashCharge * smashSpeed, 0);
            else
                ballrb.velocity = new Vector2(-smashMinSpeed + -smashCharge * smashSpeed, 0);
        }
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
        isAiming = false;
        
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

    public IEnumerator getStunned(float time)
    {
        isStun = true;
        isAiming = false;
        yield return new WaitForSeconds(time);
        isStun = false;
    }

}
