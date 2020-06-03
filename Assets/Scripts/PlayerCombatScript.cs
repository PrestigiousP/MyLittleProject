using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatScript : MonoBehaviour
{
    public Animator animator;
    //public PlayerController playerController;
    public Transform attackPoint;//object qui sert de detecteur pour le hitbox du hit
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    //public Collider2D attack1Collider;//hitbox de l'attack1
    public int attack1Damage;
    private void Start()
    { 

    }
    // Update is called once per frame
    void Update()
    {
   
        if (Input.GetButtonDown("Fire1"))
        {
            Attack1();
        }
        else if (Input.GetButton("Fire2"))
        {
            Attack2();
        }
    

    }
    void Attack1()
    {
        //----------play an attack animation-------------------------
        animator.SetTrigger("Attack1");
        //-----------------Detect enemies in range of attack--------------
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);//
        //-----------------Damage them-------------------------------------
        foreach(Collider2D enemy in hitEnemies)
        {
           // Debug.Log(enemy.name);
            enemy.GetComponent<WarriorEnemy>().TakeDamage(attack1Damage);
        }
    }
    //fonction qui permet de voir le range de l'attack1
    private void OnDrawGizmos()
    {
        if(attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    void Attack2()
    {
        animator.SetTrigger("Attack2");
    }
}
