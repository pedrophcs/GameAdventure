using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLife : MonoBehaviour
{
    private Animator anim;
    public int health;
    //[HideInInspector] public bool isDead = false;
    public bool isDead = false;
    // Start is called before the first frame update
    private Life Life;
    private Player Player;
    void Start()
    {
        anim = GetComponent<Animator>();
        Life = FindObjectOfType(typeof(Life)) as Life;
        Player = FindObjectOfType(typeof(Player)) as Player;
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
        if(gameObject.tag == "Player")
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
            
            if (health <= 0)
            {
                anim.SetTrigger("Death");
                isDead = true;
                Destroy(this.gameObject);
            }
        }
    }
}
