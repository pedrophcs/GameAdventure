using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLife : MonoBehaviour
{
    private Animator anim;
    public int health;
    //[HideInInspector] public bool isDead = false;
    public bool isDead = false;
    public bool isAlive;
    public bool boss, player;
    // Start is called before the first frame update
    private Life Life;
    private Player Player;
    public static CharacterLife chara;
    private SpawnController SpawnController;
    private Tartoga tar;
    private Centipede cen;
    public bool cent, tart;
    private Gatilho gatilho;
    void Start()
    {
        isAlive = true;
        SpawnController = FindObjectOfType(typeof(SpawnController)) as SpawnController;
        tar = FindObjectOfType(typeof(Tartoga)) as Tartoga;
        anim = GetComponent<Animator>();
        Life = FindObjectOfType(typeof(Life)) as Life;
        Player = FindObjectOfType(typeof(Player)) as Player;
        gatilho = FindObjectOfType(typeof(Gatilho)) as Gatilho;

    }

    // Update is called once per frame
    void Update()
    {
        LifeLoss();
        if (Player.craft == true && health < 6)
        {
            if (gameObject.tag == "Player")
            {
                health++;
            }
        }
       
    }

    void LifeLoss()
    {
        if (gameObject.tag == "Player")
        {
            Life.UpdateLives(health);
        }

    }
    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            anim.SetTrigger("Hurt");
            health -= damage;
            if(!player)
            {
                if (health <= 0 && !boss)
                {
                    isDead = true;
                    Destroy(this.gameObject);
                }
                if (health <= 0 && boss)
                {

                    isAlive = false;
                }
            }         
            else if(player && health <= 0)
            {
                SpawnController.Death();
                health = 6;
            }

        }
      
        if (isAlive == false)
        {
           
            anim.SetBool("Death", true);
            gatilho.startBoss = false;
            SpawnController.bossDeath = true;
            Fim();
        }
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Rune")
        {
            if (gameObject.tag != "Player")
            {
                anim.SetTrigger("Hurt");
                health -= 6;
            }
            if (health <= 0 && boss)
            {

                isAlive = false;
            }
            if (isAlive == false)
            {
                StartCoroutine(SpawnController.FadeOut());
                anim.SetBool("Death", true);
                gatilho.startBoss = false;
                SpawnController.bossDeath = true;
                Fim();
            }
        }
    }
    void Fim()
    {
        

        AudioController.instance.boss.enabled = false;
        AudioController.instance.victory.enabled = true;
        tar.isAlive = false;
    }
}
