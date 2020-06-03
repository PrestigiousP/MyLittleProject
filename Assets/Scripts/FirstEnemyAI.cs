using System.Threading;
using UnityEngine;
using UnityEngine.AI;
public class FirstEnemyAI : MonoBehaviour
{
    public Animator enemyAnimator;//private ou public ? 
    public Rigidbody2D rb;
    public WarriorEnemy enemyScript;
    public GameObject player;
    public float detectionRange;
    public float speed;
    public LayerMask playerLayer;
    private float randomJump;
    public int jumpSpeed;
    private float timerJump;
    private float jumpTime = 1f;

    void Start()
    {

        timerJump = 2;
    }

    // Update is called once per frame
    void FixedUpdate()//on utilise la fonction fixedupdate pour apppliquer des forces au lieu de update
    {
       // Debug.Log(rb.velocity);
        Collider2D playerEnter = Physics2D.OverlapCircle(transform.position, detectionRange, playerLayer);
         if(playerEnter == true)
         {
            enemyAnimator.SetBool("PlayerInRange", true);
            PlayerDetected();
        }
        else
        {
            enemyAnimator.SetBool("PlayerInRange", false);
        }
         
    }
    void PlayerDetected()
    {
        //--------------------------when timer is up, enemy jump--------------------------------
        timerJump -= Time.deltaTime;
        // Debug.Log(timerJump);
        if (timerJump <= 0 && enemyScript.GroundCheck() == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            // Jump();
            if (enemyScript.GroundCheck())
                timerJump = 2;


        }
        else
        {
            if (transform.position.x <= player.transform.position.x + 0.05 && transform.position.x >= player.transform.position.x - 0.05)
                rb.velocity = new Vector2(0, 0);
            else
            {
                //---------------------Enemy walk toward---------------------
                if (player.transform.position.x - transform.position.x < 0)//algo qui decide quel bord il marche
                {
                    //Debug.Log(player.transform.position.x);
                    rb.velocity = new Vector2(player.transform.position.x / speed, rb.velocity.y);//0 ou transform.position.y?
                                                                                      // enemyAnimator.SetFloat("Speed", rb.velocity.x);
                    transform.localScale = new Vector2(-1, 1);
                }
                else
                {
                    // Debug.Log(player.transform.position.x);
                    rb.velocity = new Vector2(Mathf.Abs(player.transform.position.x / speed), rb.velocity.y);
                    transform.localScale = new Vector2(1, 1);
                }
            }
        }
        /* if (transform.position.x <= player.transform.position.x + 0.05 && transform.position.x >= player.transform.position.x - 0.05)
             rb.velocity = new Vector2(0, 0);
         else
         {
             //---------------------Enemy walk toward---------------------
             if (player.transform.position.x - transform.position.x < 0)//algo qui decide quel bord il marche
             {
                 //Debug.Log(player.transform.position.x);
                 rb.velocity = new Vector2(player.transform.position.x / speed, 0);//0 ou transform.position.y?
                                                                                   // enemyAnimator.SetFloat("Speed", rb.velocity.x);
                 transform.localScale = new Vector2(-1, 1);
             }
             else
             {
                 // Debug.Log(player.transform.position.x);
                 rb.velocity = new Vector2(Mathf.Abs(player.transform.position.x / speed), 0);
                 transform.localScale = new Vector2(1, 1);
             }
         }*/


    }
    void Jump()
    {
        //Debug.Log(rb.velocity);
       // jumpTime -= Time.deltaTime;
        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
    }
    private void OnDrawGizmos()
    {
       Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
