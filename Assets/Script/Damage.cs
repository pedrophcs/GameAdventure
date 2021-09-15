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
        if (impulsoP || impulsoE)
        {
            timerP += Time.deltaTime;
        }
        if (timerP > 2)
        {
            impulsoP = false;
            impulsoE = false;
            timerP = 0;
        }
    }
    private void FixedUpdate()
    {
        if (impulsoP)
        {
            if (player.transform.position.x > transform.position.x)
            {
                if (timerP < 2)
                {
                    player.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 50);
                }
            }
            else
            {
                if (timerP < 2)
                {
                    player.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 50);
                }
            }
        }

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
            AudioController.instance.PlaySounds(Sound.playerDa);
        }
        else if (col.gameObject.CompareTag("Enemy"))
        {
            AudioController.instance.PlaySounds(Sound.mobD);
            impulsoE = true;
            if (col.transform.position.x > transform.position.x)
            {
                if (timerP < 2)
                {
                    col.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 100);
                }
            }
            else
            {
                if (timerP < 2)
                {
                    col.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 100);
                }
            }
        }
    }
}
