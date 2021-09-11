using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centipede : MonoBehaviour
{
    public Animator anim;
    public GameObject batD, batE, batC;
    private Player player;
    public LayerMask groundLayer;
    private Gatilho gatilho;
    public float speedCen;
    private Rigidbody2D rdb;
    public Transform wallCheck;
    public float distance;
    public bool isRight;
    public bool OD;
    public bool isWall;
    public float timerMorcegos;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType(typeof(Player)) as Player;
        gatilho = FindObjectOfType(typeof(Gatilho)) as Gatilho;
        rdb = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (gatilho.startBoss)
        {
            speedCen = 1;
            timerMorcegos += Time.deltaTime;
            if(timerMorcegos > 8)
            {
                speedCen = 1;
                timerMorcegos = 0;
            }
            
            Bat();
            transform.Translate(Vector2.right * speedCen * Time.deltaTime);
            isWall = Physics2D.OverlapCircle(wallCheck.position, 0.02f, groundLayer);
            
            if (isWall)
            {

                if (isRight == true)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    isRight = false;

                }
                else
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    isRight = true;

                }
            }
        }
        
    }
    void Bat()
    {
        if(player.transform.position.x > batD.transform.position.x || player.transform.position.x < batE.transform.position.x || player.transform.position.y > batC.transform.position.y && timerMorcegos>=8)
        {
            print("solta morcego");
            
        }
    }
}
