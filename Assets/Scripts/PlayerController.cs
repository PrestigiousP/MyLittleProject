using System;
using System.Collections;

using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private float movement = 0f;
    public float jumpSpeed = 5f;
    private Rigidbody2D rigidBody;
    public LayerMask enemyLayer;
    public LayerMask groundLayer;//objet qui permet de pouvoir check si le player touche le ground
    private Animator PlayerAnimation;//objet qui permet d'utiliser les conditions d'animation de player
    public Transform GroundCheckPoint;//sous-objet de player, qui sert a verifier si le player touche le ground(pour les jumps)
    public float groundCheckRadius;
    public int doubleJump = 0;
    private bool isTouchingGround;
    private bool onTopOfEnemy;

    //private bool JumpAnimation;
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private bool canDashAgain;
    private int direction;


    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        PlayerAnimation = GetComponent<Animator>();
        dashTime = startDashTime;
    }

    // Update is called once per frame
    void Update()
    {
        //CheckGround();
        //---------------------touching ground checker-------------------------------
        isTouchingGround = Physics2D.OverlapCircle(GroundCheckPoint.position, groundCheckRadius, groundLayer);
        onTopOfEnemy = Physics2D.OverlapCircle(GroundCheckPoint.position, groundCheckRadius, enemyLayer);
        //-------------------------reset les param pour les animations------------------
        if (isTouchingGround)
        {
            PlayerAnimation.ResetTrigger("Jump");
            PlayerAnimation.SetBool("Landing", false);
        }
        PlayerMovement();
        HandleLayerAnimations();


    
      
    }
  

    private void PlayerMovement()
    {
        //------------------script pour le déplacement--------------------------
        movement = Input.GetAxis("Horizontal");
        if (movement > 0f)
        {
            rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
            transform.localScale = new Vector2(1, 1);//x y ?
        }
        else if (movement < 0f)
        {
            rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }
        //------------------script pour le saut--------------------------
        if (Input.GetButtonDown("Jump") && isTouchingGround == true || Input.GetButtonDown("Jump") && onTopOfEnemy == true)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
        }
        //---------------------set les conditions d'animations-----------------
        PlayerAnimation.SetFloat("run", Mathf.Abs(rigidBody.velocity.x));
        //-----------------------Dash attack-------------------------------
        if (!isTouchingGround)
        { 
           
           if (direction == 0)
            {
                if (Input.GetButtonDown("Fire1") && canDashAgain)
                {
                    canDashAgain = false;
                    if (transform.localScale.x > 0)
                    {
                        direction = 1;
                    }
                    else if (transform.localScale.x < 0)
                    {
                        direction = 2;
                    }
                }
            }
            else
            {

                if (dashTime <= 0)
                {
                    direction = 0;//reset
                    //canDashAgain = false;
                    dashTime = startDashTime;//reset
                }
                else
                {
                    dashTime -= Time.deltaTime;
                    if (direction == 1)
                    {
                        rigidBody.velocity = Vector2.right * dashSpeed;
                    }
                    else if (direction == 2)
                    {
                        rigidBody.velocity = Vector2.left * dashSpeed;
                    }
                }
            }
        }
        else if(isTouchingGround)
        {
            canDashAgain = true;
        }
     
    }
  
    void CheckGround()
    {
        //---------------------touching ground checker-------------------------------
        isTouchingGround = Physics2D.OverlapCircle(GroundCheckPoint.position, groundCheckRadius, groundLayer);
        //-------------------------reset les param pour les animations------------------
        if (isTouchingGround)
        {
            PlayerAnimation.ResetTrigger("Jump");
            PlayerAnimation.SetBool("Landing", false);
        }
    }
  private void HandleLayerAnimations()
    {
      
        if (isTouchingGround == false)
        {
            //call la method qui retourne la valeur pour dash
           
            PlayerAnimation.SetLayerWeight(1, 1);//1er param: prend le layer de l'index 1. 2eme param: set un weigth pour le layer

            PlayerAnimation.SetTrigger("Jump");
        }
        else
        {
            PlayerAnimation.SetLayerWeight(1, 0);
        }
        if (rigidBody.velocity.y < 0)
        {
            PlayerAnimation.SetBool("Landing", true);
        }

    }
    

}
