using UnityEngine;
using UnityEngine.AI;
public class FirstEnemyAI : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject player;
    public float detectionRange;
    public float speed;
    public LayerMask playerLayer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
          
    }

    // Update is called once per frame
    void Update()
    {
       
        //Debug.Log(player.transform.position.x);
        rb.velocity = new Vector2(player.transform.position.x / speed, transform.position.y);
         Collider2D playerEnter = Physics2D.OverlapCircle(transform.position, detectionRange, playerLayer);
         if(playerEnter == true)
         {
              ChasePlayer();
              //Attack();
         }
         
    }
    void ChasePlayer()
    {
        if(player.transform.position.x - transform.position.x < 0)
        {
            //Debug.Log(player.transform.position.x);
            rb.velocity = new Vector2(player.transform.position.x / speed, transform.position.y);
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            Debug.Log(player.transform.position.x);
            rb.velocity = new Vector2(Mathf.Abs(player.transform.position.x / speed), transform.position.y);
            transform.localScale = new Vector2(1, 1);
        }
    }
    private void OnDrawGizmos()
    {
       Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
