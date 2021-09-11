using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Morcego : MonoBehaviour
{

    public float speed;
    [SerializeField] private float playerCheckRadious;

    private Transform target;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private GameObject Obj;
    [SerializeField] private int rota;
    [SerializeField] public Transform A, B, Destino;
    private bool OD;
    private bool isAttacking;
    [SerializeField] float groundCheckRadious;
    [SerializeField] private Transform attackCheck;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Vector2 attackRange;
    [SerializeField] private Vector3 size;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update()
    {
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            
        }
    }
}
