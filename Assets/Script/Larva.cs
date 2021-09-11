using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Larva : MonoBehaviour
{
    public float speed;
    public float distance;

    bool isRight = true;

    public Transform groundCheck;
    private Animator animator;

    void Start()
    {

        animator = GetComponent<Animator>();
    }
    void Update()
    {
        
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        animator.SetInteger("Move", 1);
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

}