using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatScript : MonoBehaviour
{
    public Animator animator;
    //private Rigidbody2D player;
    public float dashAttack = 20;
    private void Start()
    {
        //player = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        //Attack animation
        //Detect enemies in attack range
        //Damage them
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();

        }
        else if (Input.GetButton("Fire2"))
        {
            Attack2();
        }
    }
    void Attack()
    {
        animator.SetTrigger("Attack1");
    }
    void Attack2()
    {
        animator.SetTrigger("Attack2");
    }
}
