using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    private Player player;
    public float speedCam;
    public Transform E;
    public Transform D;
    public Transform B, C;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        player = FindObjectOfType(typeof(Player)) as Player;
        float playerPositionY = player.transform.position.y;
        float playerPositionX = player.transform.position.x;
        if(playerPositionX > E.transform.position.x && playerPositionX < D.transform.position.x)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x, transform.position.y, transform.position.z), speedCam);
        }
        if(transform.position.x >E.transform.position.x && transform.position.x <D.transform.position.x)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x, transform.position.y, transform.position.z), speedCam);
        }
        if (playerPositionY > B.transform.position.y && playerPositionY < C.transform.position.y)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z), speedCam);
        }
        if (transform.position.x > B.transform.position.x && transform.position.x < C.transform.position.x)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x, transform.position.y, transform.position.z), speedCam);
        }

    }
}
