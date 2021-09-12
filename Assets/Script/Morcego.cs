using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Morcego : MonoBehaviour
{

    public float speed;
    [SerializeField] private float playerCheckRadious;
    [SerializeField] Transform target;
    [SerializeField] private Transform playerCheck;
    private bool OD;
    private bool isAttacking;
    [SerializeField] float groundCheckRadious;
    [SerializeField] private Transform attackCheck;
    [SerializeField] private LayerMask playerLayer;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
       
    }
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        isAttacking = Physics2D.OverlapCircle(attackCheck.position, groundCheckRadious, playerLayer);
        if(isAttacking)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            if (GameObject.FindGameObjectWithTag("Player").transform.position.x < transform.position.x)
            {
                if (OD)
                {
                    Flip();
                }
            }
            if (GameObject.FindGameObjectWithTag("Player").transform.position.x > transform.position.x)
            {
                if (!OD)
                {
                    Flip();
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(attackCheck.position, groundCheckRadious);
    }
    void Flip()
    {
        OD = !OD;
        Vector3 v3 = transform.localScale;
        v3.x *= -1;
        transform.localScale = v3;
    }


}
