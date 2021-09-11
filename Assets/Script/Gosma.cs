using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gosma : MonoBehaviour
{
    public float timeAtk;
    private float speed;
    public float distance;
    bool isRight = true;
    public float checkRadius = 0.3f;
    public LayerMask playerMask;
    public bool atkD, atkE;
    public Transform groundCheck;
    private Animator animator;
    public BoxCollider2D Atk;
    private Rigidbody2D rdb;
    private Damage damage;

    //ATAQUE
    private float nextAtk;
    [SerializeField] private float atkRate;


    void Start()
    {
        rdb = GetComponent<Rigidbody2D>();
        damage = FindObjectOfType(typeof(Damage)) as Damage;
        animator = GetComponent<Animator>();
        speed = -0.5f;
        
    }
    void Update()
    {
        Atack();

        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D ground = Physics2D.Raycast(groundCheck.position, Vector2.down, distance);
        animator.SetInteger("Move", 1);
        if (ground.collider == false)
        {

            if (isRight == true)
            {

                transform.eulerAngles = new Vector3(0, 0, 0);
                isRight = false;
            }
            else
            {

                transform.eulerAngles = new Vector3(0, 180, 0);
                isRight = true;
            }
        }
        if (atkD || atkE)
        {
            timeAtk += Time.deltaTime;
            speed = 0;
            if (timeAtk > 2 && timeAtk < 3)
            {
                animator.SetBool("Atk", true);
                Atk.enabled = true;

            }
        }
        else
        {
            animator.SetBool("Atk", false);
            Atk.enabled = false;
            speed = -0.5f;
        }
        if (timeAtk > 3)
        {
            atkD = false;
            atkE = false;
            timeAtk = 0;
        }
    }
    //private void FixedUpdate()
    //{
    //    if(damage.impulsoE)
    //    {
    //        rdb.AddForce(Vector2.right * 500);
    //    }
    //}

    void Atack()
    {
        nextAtk = atkRate + Time.time;
        bool E = Physics2D.OverlapCircle(transform.position + new Vector3(-0.6f, 0), checkRadius, playerMask);
        bool D = Physics2D.OverlapCircle(transform.position + new Vector3(0.5f, 0), checkRadius, playerMask);
        if (D)
        {
            atkD = true;
            atkE = false;
            transform.eulerAngles = new Vector3(0, 180, 0);
            //speed = 0;
            //if (timeAtk > 2 && timeAtk <3)
            //{
            //    animator.SetBool("Atk", true);
            //    Atk.enabled = true;
                
            //}
        }
        //else
        //{
        //    animator.SetBool("Atk", false);
        //    Atk.enabled = false;
        //    speed = -0.5f;
        //}
        if (E)
        {
            atkD = false;
            atkE = true;
            transform.eulerAngles = new Vector3(0, 0, 0);
            //speed = 0;
            //if (timeAtk > 2 && timeAtk < 3)
            //{
            //    animator.SetBool("Atk", true);
            //    Atk.enabled = true;
                
            //}
            
            //StartCoroutine("CDAtk");
        }
        //else
        //{
        //    animator.SetBool("Atk", false);
        //    Atk.enabled = false;
        //    speed = -0.5f;
        //}

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position + new Vector3(-0.6f, 0), checkRadius);
        Gizmos.DrawWireSphere(transform.position + new Vector3(0.5f, 0), checkRadius);
    }
    IEnumerator CDAtk()
    {
        speed = 0;
        yield return new WaitForSeconds(1);
        animator.SetBool("Atk", true);
        Atk.enabled = true;
        yield return new WaitForSeconds(1);
        animator.SetBool("Atk", false);
        Atk.enabled = false;
        speed = -0.5f;
    }

}
