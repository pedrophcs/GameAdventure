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
    public bool isAlive;
    public float timerMorcegos;
    public GameObject morcego;
    public Transform batSpawner;
    
    void Start()
    {
        isAlive = true;
        anim = GetComponent<Animator>();
        player = FindObjectOfType(typeof(Player)) as Player;
        gatilho = FindObjectOfType(typeof(Gatilho)) as Gatilho;
        rdb = GetComponent<Rigidbody2D>();
    }
    
   
    void Update()
    {
        if (!isAlive)
        {
            gatilho.startBoss = false;
            anim.SetBool("Death", true);
        }
        if (gatilho.startBoss)
        {
            AudioController.instance.sing.enabled = false;
            AudioController.instance.boss.enabled = true;

            speedCen = 1;
            timerMorcegos += Time.deltaTime;
            if(timerMorcegos > 8)
            {
                speedCen = 1;
                Bat();
                timerMorcegos = 0;
            }
            
           
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

        AudioController.instance.PlaySounds(Sound.batSpa);
        GameObject pref = Instantiate(morcego);
            pref.transform.position = batSpawner.position;
          
        
    }
}
