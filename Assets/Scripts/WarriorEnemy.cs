using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorEnemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    public Transform GroundCheckPoint;//sous-objet de player, qui sert a verifier si le player touche le ground(pour les jumps)
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private Rigidbody2D rb;
    private bool isTouchingGround;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage)
    {
        animator.SetTrigger("Hurt");
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy die");
        animator.SetBool("isDead", true);
        //-----------disable enemy----------------
        GetComponent<Collider2D>().enabled = false;//get
        if(isTouchingGround)
        gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        this.enabled = false;//prend le script et disable
    }
    // Update is called once per frame
    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(GroundCheckPoint.position, groundCheckRadius, groundLayer);
    }
}
