using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afogado : MonoBehaviour
{
    public float speed;
    public float distance;

    bool isRight = true;
    private bool isAttacking = false;
    public Transform groundCheck;
    private Animator animator;
    [SerializeField] float groundCheckRadious;
    [SerializeField] private Transform attackCheck;
    [SerializeField] private LayerMask playerLayer;
    private Player player;
    public BoxCollider2D boxAtk;
    public BoxCollider2D triggerAtk;
    private bool OD;
    void Start()
    {
        player = FindObjectOfType(typeof(Player)) as Player;
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        isAttacking = Physics2D.OverlapCircle(attackCheck.position, groundCheckRadious, playerLayer);
        RaycastHit2D ground = Physics2D.Raycast(groundCheck.position, Vector2.down, distance);
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (isAttacking)
        {
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
        else
        {
            if (ground.collider == false)
            {

                if (isRight == true)
                {
                    animator.SetInteger("Move", 1);
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    isRight = false;
                }
                else
                {
                    animator.SetInteger("Move", 1);
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    isRight = true;
                }

            }
        }

        if (speed != 0)
        {
            animator.SetInteger("Move", 1);
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(attackCheck.position, groundCheckRadious);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(attack());

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GameObject.FindGameObjectWithTag("Player").transform.position.x < transform.position.x)
        {
            speed = -1;
        }
        if (GameObject.FindGameObjectWithTag("Player").transform.position.x > transform.position.x)
        {
            speed = 1;
        }
    }

    void Flip()
    {
        OD = !OD;
        Vector3 v3 = transform.localScale;
        speed *= -1;
        v3.x *= -1;
        transform.localScale = v3;
    }
    IEnumerator attack()
    {
        float currentSpeed = speed;
        speed = 0;
        animator.SetInteger("Move", 2);
        boxAtk.enabled = true;
        yield return new WaitForSeconds(1);
        speed = currentSpeed;


    }


}
