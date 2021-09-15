using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject tiro;
    
    private Rigidbody2D rdb;
    private CapsuleCollider2D capsule;
    public bool isFather;
    


    private void Start()
    {
        capsule = GetComponent<CapsuleCollider2D>();
        rdb = GetComponent<Rigidbody2D>();  
        StartCoroutine(Timer());
    }

    private void Update()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player" || collision.tag == "Ground")
        {

            for (int i = 0; i < 10; i++)
            {
                int randX = Random.Range(-500, 500);
                int randY = Random.Range(-500, 500);
                GameObject preFab = Instantiate(tiro);
                preFab.GetComponent<Rigidbody2D>().AddForce(new Vector2(randX, randY));
                preFab.transform.position = transform.position;

            }
            Destroy(this.gameObject);
        }
    }
   

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(3);
        rdb.gravityScale = 1;
        capsule.isTrigger = true;
    }
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

}
