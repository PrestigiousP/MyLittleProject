using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 playerPosition;
    public float Offset;
    public float timerOffset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //-------------------------permet a la camera de suivre le player-----------------------------
        playerPosition = new Vector3(player.transform.position.x, transform.position.y, -10);
        //Pour pouvoir changer la postition d'un objet, il faut absolument creer un vector3
        if (player.transform.localScale.x > 0)
            playerPosition = new Vector3(playerPosition.x + Offset, playerPosition.y, playerPosition.z);
        else
        {
            playerPosition = new Vector3(playerPosition.x - Offset, playerPosition.y, playerPosition.z);
        }
        //Time.deltaTime permet d'avoir le meme rendement peu importe le fps et le type d'ordinateur que la personne utilise.
        transform.position = Vector3.Lerp(transform.position, playerPosition, timerOffset * Time.deltaTime);
        //-----------------------------------------------------------------------------------------------
    }
}
