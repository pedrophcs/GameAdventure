using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float timerP;
    public float timerE;
    private float impulsoHorizontal = 3;
    public int damage;
    private float impulsoPadrao;
    private Player player;
    public bool impulsoP;
    public bool impulsoE;
    public bool inimigo;
    private Enemy enemy;
    private void Start()
    {
        player = FindObjectOfType(typeof(Player)) as Player;
        impulsoPadrao = impulsoHorizontal;
        enemy = FindObjectOfType(typeof(Enemy)) as Enemy;
    }
    private void Update()
    {
        if (impulsoP)
        {
            timerP += Time.deltaTime;
        }
        if (timerP > 1)
        {
            impulsoP = false;
            timerP = 0;
        }
    }
    private void FixedUpdate()
    {
        if (impulsoP)
        {
            if (player.transform.position.x > transform.position.x)
            {
                if (timerP < 1)
                {
                    player.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 50);
                }
            }
            else
            {
                if (timerP < 1)
                {
                    player.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 50);
                }
            }
        }
        //if(impulsoE)
        //{

        //    enemy.rdbenemy.GetComponent<Rigidbody2D>()
            
        //}

    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        CharacterLife character = col.GetComponent<CharacterLife>();
        if (character)
        {
            character.TakeDamage(damage);

        }
        if (col.gameObject.CompareTag("Player"))
        {
            impulsoP = true;
        }
        else if (col.gameObject.CompareTag("Enemy"))
        {
            impulsoE = true;
            //if (col.transform.position.x > transform.position.x)
            //{
            //    if (timerP < 1)
            //    {
            //        col.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 3);
            //    }
            //}
            //else
            //{
            //    if (timerP < 1)
            //    {
            //        col.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 3);
            //    }
            //}
        }
    }
}
