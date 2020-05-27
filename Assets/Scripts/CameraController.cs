using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    private Vector3 playerPosition;
    public float xOffset;
    public float yOffset;
    public float timerOffset;
    public float fallOffset;
    // Start is called before the first frame update
    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rb.velocity.y);
        //-------------------------permet a la camera de suivre le player-----------------------------
        playerPosition = new Vector3(player.transform.position.x, player.transform.position.y + yOffset, -10);
        //Pour pouvoir changer la postition d'un objet, il faut absolument creer un vector3
        if (player.transform.localScale.x > 0)
            playerPosition = new Vector3(playerPosition.x + xOffset, playerPosition.y, playerPosition.z);
        else
        {
            playerPosition = new Vector3(playerPosition.x - xOffset, playerPosition.y, playerPosition.z);
        }
        if(rb.velocity.y < 0)
        {
            playerPosition = new Vector3(playerPosition.x, playerPosition.y - fallOffset, -10);
        }
        
        //Time.deltaTime permet d'avoir le meme rendement peu importe le fps et le type d'ordinateur que la personne utilise.
        transform.position = Vector3.Lerp(transform.position, playerPosition, timerOffset * Time.deltaTime);
        //-----------------------------------------------------------------------------------------------
    }
}
