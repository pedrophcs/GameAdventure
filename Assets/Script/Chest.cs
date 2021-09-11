using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private GameObject item;
    [SerializeField] Transform spawn;
    private int count = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

   
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if(collision.gameObject.tag == "Player" && count==0)
        {
            animator.SetTrigger("Open");

            GameObject tempPrefab = Instantiate(item) ;
            tempPrefab.transform.position = spawn.position;
            tempPrefab.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 75));
            count ++;
        }
    }
}
