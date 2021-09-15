using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tartoga : MonoBehaviour
{

    [Header("Idle")]
    [SerializeField] float idleMoveSpeed;
    [SerializeField] Vector2 idleMoveDirection;
    [SerializeField] Animator animator;

    [Header("AttackNDown")]
    [SerializeField] float attackMoveSpeed;
    [SerializeField] Vector2 attackMoveDirection;

    [Header("AttackPlayer")]
    [SerializeField] float attackPlayerSpeed;
    [SerializeField] Transform player;

    [Header("Other")]
    [SerializeField] Transform attackCheck;
    [SerializeField] Transform attackRangedCheck;
    [SerializeField] Transform groundCheckDown;
    [SerializeField] Transform groundCheckWall;

    [SerializeField] float groundCheckRadious;
    [SerializeField] float rangedAttackRadious;
    [SerializeField] Vector3 attackMelee;
    [SerializeField] Vector3 attackRanged;
    [SerializeField] LayerMask playerLayer;

    [SerializeField] private bool isWalking = true;
    [SerializeField] private bool isMelee, isRanged;

    private bool goingUp = true;
    private bool OD = true;
    private bool canAttack = true;

    private Rigidbody2D enemyRB;

    [Header("Bullets")]
    [SerializeField] GameObject bulletDownB;
    [SerializeField] GameObject bulletDownF;
    [SerializeField] GameObject bulletUpF;
    [SerializeField] GameObject bulletUpB;
    [SerializeField] Collider2D attackCollider;
    [SerializeField] Transform frontShot;
    [SerializeField] Transform backShot;
    [SerializeField] float bulletSpeedx;
    [SerializeField] float bulletSpeedy;
    [SerializeField] float timer;
    public bool isAlive;
    private Gatilho gatilho;
    private void Start()
    {
        isAlive = true;
        idleMoveDirection.Normalize();
        attackMoveDirection.Normalize();
        enemyRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gatilho = FindObjectOfType(typeof(Gatilho)) as Gatilho;
    }
    private void Update()
    {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
       
            
        
        if (gatilho.startBoss)
        {
           
            AudioController.instance.sing.enabled = false;
            AudioController.instance.boss.enabled = true;

            isMelee = Physics2D.OverlapCircle(attackCheck.position, groundCheckRadious, playerLayer);
            isRanged = Physics2D.OverlapCircle(attackRangedCheck.position, rangedAttackRadious, playerLayer);


            IdleState();
            if (bulletSpeedx > 0)
            {
                bulletDownB.transform.localScale *= -1;
                bulletDownF.transform.localScale *= -1;
                bulletUpB.transform.localScale *= -1;
                bulletUpF.transform.localScale *= -1;
            }
            else
            {
                bulletDownB.transform.localScale *= -1;
                bulletDownF.transform.localScale *= -1;
                bulletUpB.transform.localScale *= -1;
                bulletUpF.transform.localScale *= -1;
            }
            if(idleMoveSpeed > 0 || idleMoveSpeed < 0)
            {
                isWalking = true;
            }
        }
       



    }
    void IdleState()
    {
        if (isMelee)
        {

            StartCoroutine(MeleeAttack());
            isMelee = false;
        }
        if (isRanged)
        {

            ShootDown();

        }
        if (isWalking == true)
        {
            animator.SetInteger("Move", 1);
        }

        if (GameObject.FindGameObjectWithTag("Player").transform.position.x > transform.position.x)
        {
            if (OD)
            {
                Flip();
            }
        }
        if (GameObject.FindGameObjectWithTag("Player").transform.position.x < transform.position.x)
        {
            if (!OD)
            {
                Flip();
            }
        }
        enemyRB.velocity = idleMoveSpeed * idleMoveDirection;
    }
    IEnumerator MeleeAttack()
    {
        idleMoveSpeed = 1;
        animator.SetInteger("Move", 8);
        attackCollider.enabled = true;
        float holdSpeed = idleMoveSpeed;
        idleMoveSpeed = 0;
        yield return new WaitForSeconds(3);
        isMelee = true;
        idleMoveSpeed = holdSpeed;
    }
    void ShootDown()
    {
        timer += Time.deltaTime;
        if (timer >= 2 && isRanged)
        {
            isWalking = false;
            isRanged = false;
            StartCoroutine(Shoot());
            timer = 0;
        }
        if (timer >= 8)
        {
            timer = 0;
            isRanged = true;
        }
    }
    IEnumerator Shoot()
    {
        if (GameObject.FindGameObjectWithTag("Player").transform.position.y <= transform.position.y)
        {
            idleMoveSpeed = 1;
            float holdSpeed = idleMoveSpeed;
            idleMoveSpeed = 0;
            animator.SetInteger("Move", 5);
            yield return new WaitForSeconds(0.5f);

            GameObject BS = Instantiate(bulletDownF);
            BS.transform.position = backShot.transform.position;
            BS.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletSpeedx, 0));

            GameObject FS = Instantiate(bulletDownB);
            FS.transform.position = frontShot.transform.position;
            FS.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletSpeedx, 0));

            yield return new WaitForSeconds(1);
            isWalking = true;
            idleMoveSpeed = holdSpeed;
        }
        else
        {
            idleMoveSpeed = 1;
            float holdSpeed = idleMoveSpeed;
            idleMoveSpeed = 0;
            animator.SetInteger("Move", 5);
            yield return new WaitForSeconds(0.5f);

            GameObject BS = Instantiate(bulletUpF);

            BS.transform.position = backShot.transform.position;
            BS.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletSpeedx, bulletSpeedy));

            GameObject FS = Instantiate(bulletUpB);

            FS.transform.position = frontShot.transform.position;
            FS.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletSpeedx, bulletSpeedy));

            yield return new WaitForSeconds(1);
            isWalking = true;
            idleMoveSpeed = holdSpeed;
        }

    }


    void Flip()
    {
        OD = !OD;
        idleMoveDirection.x *= -1;
        attackMoveDirection.x *= -1;
        transform.Rotate(0, 180, 0);
        attackPlayerSpeed *= -1;
        bulletSpeedx *= -1;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(attackCheck.position, attackMelee);
        Gizmos.DrawWireSphere(attackRangedCheck.position, rangedAttackRadious);
        Gizmos.DrawWireSphere(groundCheckWall.position, groundCheckRadious);

    }





}
