using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWarriorCombatScript : MonoBehaviour
{
    public float attackRange;
    public float triggerRange;
    public float damageAttack1;
    public Animator enemyAnimator;
    public Transform attackPosition;
    public Transform attackRangeTrigger;
    public LayerMask playerLayer;
    public LayerMask enemyLayer;
    private bool canAttack = true;
    private float waitASec = 2;


    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!canAttack)
        {
            waitASec -= Time.deltaTime;
            if(waitASec <= 0)
            {
                enemyAnimator.SetBool("CanAttack", true);
                canAttack = true;
                waitASec = 2;
            }
        }
        else
        {
            //-------------------Detection du ou des players------------------------------   
            Collider2D playerHit = Physics2D.OverlapCircle(attackPosition.position, attackRange, playerLayer);//physics2d retourne tjrs un array*****devrait changer la méthode pour une qui est moins couteuse en mémoire
            if (playerHit)
            {

                enemyAnimator.SetTrigger("AttackInRange");
                enemyAnimator.SetBool("CanAttack", true);
               // Debug.Log("Take a hit");
                playerHit.GetComponent<PlayerController>().TakeDamage(damageAttack1);
                enemyAnimator.SetBool("CanAttack", false);
                canAttack = false;
            }

        }
    }

    private void OnDrawGizmos()
    {
        if (attackPosition == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
        Gizmos.DrawWireSphere(attackRangeTrigger.position, triggerRange);
    }
}
