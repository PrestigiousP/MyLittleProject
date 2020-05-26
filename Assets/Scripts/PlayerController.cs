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
    public LayerMask groundLayer;//objet qui permet de pouvoir check si le player touche le ground
    private Animator PlayerAnimation;//objet qui permet d'utiliser les conditions d'animation de player
    public Transform GroundCheckPoint;//sous-objet de player, qui sert a verifier si le player touche le ground(pour les jumps)
    public float groundCheckRadius;
    public int doubleJump = 0;
    private bool isTouchingGround;
    private PlayerCombatScript PlayerAttackMoves;
    //private bool JumpAnimation;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        PlayerAnimation = GetComponent<Animator>();
        PlayerAttackMoves = new PlayerCombatScript();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        PlayerMovement();
        HandleLayerAnimations();
        DashAttack();
    }

    private void PlayerMovement()
    {
        //------------------script pour le déplacement--------------------------
        movement = Input.GetAxis("Horizontal");
        if (movement > 0f)
        {
            rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
            transform.localScale = new Vector2(1, 1);
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
        if (Input.GetButtonDown("Jump") && isTouchingGround == true)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
        }
        //---------------------set les conditions d'animations-----------------
        PlayerAnimation.SetFloat("run", Mathf.Abs(rigidBody.velocity.x));
        //-----------------------Dash attack-------------------------------
        
    }
    private void CheckGround()
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
    void DashAttack()
    {
        if (!isTouchingGround)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (transform.localScale.x > 0)
                {
                    rigidBody.velocity = new Vector2(300, rigidBody.velocity.y);
                }
                else
                {
                    rigidBody.velocity = new Vector2(-300, rigidBody.velocity.y);
                }
            }
        }

    }

}
