using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speedBullet = 3;
    
    private Rigidbody2D rdb;
    
   
    // Start is called before the first frame update
    void Start()
    {
        
        rdb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        rdb.velocity = transform.right * speedBullet;
        
    }
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject)
        {
            Destroy(this.gameObject);
        }
    }
}
