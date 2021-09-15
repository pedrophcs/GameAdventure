using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tartaruga : MonoBehaviour
{
    private float speed, speedMain;
    public float distance;
    public float jumpForce;
    private Player player;
    bool isRight = true;

    public Transform groundCheck;
    private Animator animator;

    void Start()
    {
        player = FindObjectOfType(typeof(Player)) as Player;
        animator = GetComponent<Animator>();
        jumpForce = 500;
        speed = -0.5f;

    }
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        
        RaycastHit2D ground = Physics2D.Raycast(groundCheck.position, Vector2.down, distance);

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
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            AudioController.instance.PlaySounds(Sound.jumpTur);
            speedMain = speed;
            if (player.timer >= 0.6f)
            {
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce * 1.5f));
            }
            else
            {
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
            }
            StartCoroutine("TempLev");
        }
    }
    IEnumerator TempLev()
    {
        speed = 0;
        transform.Rotate(new Vector3(0, 0, -180));
        yield return new WaitForSeconds(5);
        transform.Rotate(new Vector3(0, 0, 180));
        speed = speedMain;
    }
}
