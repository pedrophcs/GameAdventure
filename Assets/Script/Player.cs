using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    private float h, v;
    private bool playerMove = true;
    public float speed, JumpForce, currentSpeed;
    private bool OD;
    public bool isJump;
    public float timer;
    public bool walk, craft;

    private DanoCentopeia danoCentopeia;

    [Header("GroundCheck")]
    public Transform groundCheckP;
    [SerializeField] private bool isGrounded;
    public LayerMask groundLayer;


    [Header("Atack")]
    [SerializeField] private BoxCollider2D colliderAtk;
    [SerializeField] private BoxCollider2D colliderStrongAtk1, colliderStrongAtk2;

    [Header("Componentes")]
    public Rigidbody2D rdb;
    private Animator animator;
    private CapsuleCollider2D mainCollider;

    [Header("Ladder")]
    public float climbingSpeed = 3;
    public LayerMask ladderMask;
    public bool climbing;
    public float checkRadius = 0.3f;

    public Text runes;
    public int runesQtd = 0,runeSpeed = 500;
    public GameObject runeAtk;
    public Transform runeSpawn;
    void Start()
    {
        //rdb = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        //groundCheckP = GameObject.Find("GroundCheck").transform;
        danoCentopeia = FindObjectOfType(typeof(DanoCentopeia)) as DanoCentopeia;
        walk = true;
        colliderAtk.enabled = false;
        colliderStrongAtk1.enabled = false;
        colliderStrongAtk2.enabled = false;
       // runes.text = GameObject.FindGameObjectWithTag("runeText");
    }


    void Update()
    {
        if ((h > 0 && OD) || (h < 0 && !OD))
            Flip();
        rdb.velocity = new Vector2(h * speed, rdb.velocity.y);
        isGrounded = Physics2D.OverlapCircle(groundCheckP.position, 0.02f, groundLayer);
        if (isGrounded)
        {
            timer = 0;
        }
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        PlayerMoves();
        ClimbLadder();
        runes.text = runesQtd.ToString();
       
    }

    private void PlayerMoves()
    {
        isJump = isJump || Input.GetButtonDown("Jump");
        //ANDAR----------------------------------------------------
        if (!playerMove)
            return;
        if (craft == false && h != 0 && isGrounded)
        {
            if (walk == true)
            {
                animator.SetInteger("Move", 1);
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    animator.SetTrigger("WalkinAtk");
                }
            }
            else
            {
                animator.SetInteger("Move", 2);
            }
            //rdb.velocity = new Vector2(h * speed, rdb.velocity.y);

        }
        else
        {
            animator.SetInteger("Move", 0);
        }
        //CORRER----------------------------------------------------
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (walk == true)
            {
                walk = false;

                speed = 4;
            }
            else
            {
                walk = true;
                speed = 2;
            }
        }

        //PULAR----------------------------------------------------

        if (climbing)
            return;
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJump = true;
            rdb.AddForce(new Vector2(0, JumpForce));
            //animator.SetBool("Ground", true);
            isGrounded = false;
            //rdb.velocity = new Vector2(h * speed, rdb.velocity.y);
        }
        else if (!isGrounded)
        {
            animator.SetInteger("Move", 3);
            timer += 1 * Time.deltaTime;
            isJump = false;
            //animator.SetBool("Ground", false);

        }

        //NORMAL ATK----------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Q) && h == 0)
        {
            NormalAtk();
            StartCoroutine("ColliderNormalAtk");
        }
        //STRONG ATK----------------------------------------------------
        if (Input.GetKeyDown(KeyCode.W) && h == 0)
        {
            StrongAtk();
            StartCoroutine("ColliderStrongAtk");
        }
        //CRIAR ITEM----------------------------------------------------
        if (isGrounded && Input.GetKeyDown(KeyCode.R))
        {
            currentSpeed = speed;
            animator.SetInteger("Move", 4);
            craft = true;
            speed = 0;
        }
        else if(craft == true)
        {
            craft = false;
           speed = currentSpeed;
        }
        //JOGA RUNAS
        if(Input.GetKeyDown(KeyCode.E))
        {
            ThrowRunes();
        }
    }

    void Flip()
    {
        OD = !OD;
        Vector3 v3 = transform.localScale;
        v3.x *= -1;
        transform.localScale = v3;
        runeSpeed *= -1;
    }
    public void StrongAtk()
    {
        animator.SetTrigger("StrongAtk");
        AudioController.instance.PlaySounds(Sound.at2);
    }
    public void NormalAtk()
    {
        animator.SetTrigger("NormalAtk");
        AudioController.instance.PlaySounds(Sound.at1);
    }
    bool TouchingLadder()
    {
        return mainCollider.IsTouchingLayers(ladderMask);
    }
    public void ClimbLadder()
    {
        bool up = Physics2D.OverlapCircle(transform.position, checkRadius, ladderMask);
        bool down = Physics2D.OverlapCircle(transform.position + new Vector3(0, -1), checkRadius, ladderMask);
        if (v != 0 && TouchingLadder())
        {
            climbing = true;
            rdb.isKinematic = true;
        }
        if (climbing)
        {
            if (!up && v >= 0)
            {
                FinishClimb();
                return;
            }

            if (!down && v <= 0)
            {
                FinishClimb();
                return;
            }

            float y = v * climbingSpeed;
            rdb.velocity = new Vector2(0, y);

            animator.SetFloat("ClimbingSpeed", v);

            if (isJump)
            {
                mainCollider.isTrigger = true;
                FinishClimb();
                playerMove = false;

                float x = h;
                if (h != 0)
                    x = h > 0 ? 1 : -1;
                if (x * h < 0)
                {
                    Flip();
                }
                animator.SetBool("Climbing", false);
                animator.SetInteger("Move", 3);
                rdb.AddForce(new Vector2(2 * h, 5), ForceMode2D.Impulse);
            }
        }
        animator.SetBool("Climbing", climbing);
    }
    public void FinishClimb()
    {

        climbing = false;
        rdb.isKinematic = false;
        playerMove = true;
        Invoke("ResetClimbing", 0.1f);
        animator.SetBool("Climbing", false);
    }
    public void ResetClimbing()
    {
        playerMove = true;
        if (mainCollider.IsTouchingLayers(groundLayer))
        {
            Invoke("ResetClimbing", 0.1f);
        }
        else
        {
            mainCollider.isTrigger = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, checkRadius);
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, -1), checkRadius);
    }
    void ThrowRunes()
    {
        if(runesQtd > 0)
        {
            GameObject prefab = Instantiate(runeAtk);
            prefab.transform.position = runeSpawn.position;
            prefab.GetComponent<Rigidbody2D>().AddForce(new Vector2(runeSpeed, 100));
            runesQtd--;
        }
    }
    private void FixedUpdate()
    {
        if (danoCentopeia.acertou)
        {

            if (transform.position.x > danoCentopeia.transform.position.x)
            {
                if (danoCentopeia.timerCent < 2)
                {
                    rdb.AddForce(Vector2.right * 500);
                }

            }
            else
            {
                if (danoCentopeia.timerCent < 2)
                {
                    rdb.AddForce(Vector2.left * 500);
                }
            }
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {

        switch (col.gameObject.tag)
        {
            case "PlataformaMovel":
                transform.parent = col.gameObject.transform;
                break;
        }

    }
    void OnCollisionExit2D(Collision2D col)
    {
        switch (col.gameObject.tag)
        {
            case "PlataformaMovel":
                transform.parent = null;
                break;
        }
    }
    IEnumerator Craft()
    {
        craft = true;
        animator.SetInteger("Move", 4);
        yield return new WaitForSeconds(2);
        craft = false;
    }
    IEnumerator ColliderNormalAtk()
    {
        colliderAtk.enabled = false;
        yield return new WaitForSeconds(0.2f);
        colliderAtk.enabled = true;
        yield return new WaitForSeconds(0.2f);
        colliderAtk.enabled = false;
    }
    IEnumerator ColliderStrongAtk()
    {
        yield return new WaitForSeconds(0.15f);
        colliderStrongAtk1.enabled = true;
        yield return new WaitForSeconds(0.3f);
        colliderStrongAtk2.enabled = true;
        colliderStrongAtk1.enabled = false;

        yield return new WaitForSeconds(0.3f);
        colliderStrongAtk2.enabled = false;
    }
}
