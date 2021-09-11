using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sapao : MonoBehaviour
{
    private Move move;
    private Animator animator;
    public float speed;
    public float speedAtual;
    public Transform groundCheckS;
    public float distance;
    public bool isRight;
    public GameObject acid;
    public GameObject acidE;
    public Transform spawnAcid;
    private float fireNext;
    public float fireRate;
    public bool teste;
    public float timeAtk;
    public bool atacando;
    public GameObject spawnBullet;
    public GameObject doideira;

    void Start()
    {
        animator = GetComponent<Animator>();
        speed = -2;

        move = FindObjectOfType(typeof(Move)) as Move;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D ground = Physics2D.Raycast(groundCheckS.position, Vector2.down, distance);

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
        //LadoTiro();
        timeAtk += Time.deltaTime;
        if (timeAtk <= 5)
        {

            speedAtual = speed;
            MoveS();
        }
        if (timeAtk >= 5)
        {

            //atacando = true;
            speed = 0;
            if (Time.time > fireNext)
            {
                animator.SetInteger("Move", 2);
                if (!isRight)
                {
                    Instantiate(acid, spawnAcid.position, Quaternion.identity);
                    
                }
                else
                {
                    Instantiate(acidE, spawnAcid.position, Quaternion.identity);
                }
                fireNext = fireRate + Time.time;
            }
        }
        if (timeAtk >= 8)
        {
            //atacando = false;
            speed = speedAtual;
            timeAtk = 0;
        }


        //if(Time.time > fireNext)
        //{
        //    animator.SetInteger("Move", 2);
        //    Instantiate(acid, spawnAcid.position, Quaternion.identity);
        //    fireNext = fireRate + Time.time;
        //}


        // transform.Translate(Vector2.right * speed * Time.deltaTime);

        
    }
    void MoveS()
    {
        animator.SetInteger("Move", 1);
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    //void LadoTiro()
    //{
    //    if (spawnBullet.transform.position.x < doideira.transform.position.x)
    //    {
    //        move.speedBullet *= -1;
    //    }
    //}
    //IEnumerator AtkAcid()
    //{
    //    Instantiate(acid, spawnAcid.position, Quaternion.identity);
    //    yield return new WaitForSeconds(4);
    //    Instantiate(acid, spawnAcid.position, Quaternion.identity);
    //}
}
