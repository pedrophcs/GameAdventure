using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Itens : MonoBehaviour
{

    public static Itens itens;
    private Player Player;
    private Rigidbody2D rdb;
    private CircleCollider2D circle;
    [SerializeField] GameObject runes;

    private void Start()
    {
        Player = FindObjectOfType(typeof(Player)) as Player;
        circle = GetComponent<CircleCollider2D>();
        rdb = GetComponent<Rigidbody2D>();
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            Player.runesQtd++;
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            for (int i = 0; i < 20; i++)
            {
                int randX = Random.Range(-500, 500);
                int randY= Random.Range(-500, 500);
                GameObject preFab = Instantiate(runes);
                preFab.GetComponent<Rigidbody2D>().AddForce(new Vector2(randX,randY));
                preFab.transform.position = transform.position;
            }

            Destroy(this.gameObject);
        }


    }
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

}
