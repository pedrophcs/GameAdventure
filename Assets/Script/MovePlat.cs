using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlat : MonoBehaviour
{

    [Header("Plataformas que se movem")]
    private float Speed = 1;
    private int rota;
    [SerializeField]
    private GameObject Obj;
    public Transform A, B, C, D, Destino;
    Transform originalPosition;
    public Collider2D Solo;
    public bool chestsPlat;

    [Header("Plataformas que caem")]
    public bool canFall;
    private Rigidbody2D rdb;

    void Start()
    {
        originalPosition.position = transform.position;
        rdb = GetComponent<Rigidbody2D>();
        GameObject Obj = GetComponent<GameObject>();
    }

    void Update()
    {
        //if(canFall)
        //{

        //}
        Go();
       
    }

    void Go()
    {
        float step = Speed * Time.deltaTime;
        Obj.transform.position = Vector3.MoveTowards(Obj.transform.position, Destino.position, step);
        if (!chestsPlat)
        {
            if (Obj.transform.position == Destino.position)
            {
                if (rota == 0)
                {
                    Destino.position = A.position;
                    rota = 1;
                }
                else if (rota == 1)
                {
                    Destino.position = B.position;
                    rota = 0;
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (canFall && collision.gameObject.tag == "Player")
        {
            StartCoroutine(fallTime());
        }

    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (chestsPlat && collision.gameObject.tag == "Player")
        {

            if (Obj.transform.position == Destino.position)
            {
                if (rota == 0)
                {
                    Destino.position = A.position;
                    rota = 1;
                }
                else if (rota == 1)
                {
                    Destino.position = B.position;
                    rota = 2;
                }
                else if (rota == 2)
                {
                    Destino.position = C.position;
                    rota = 3;
                }
                else if (rota == 3)
                {
                    Destino.position = D.position;
                    rota = 0;
                }
            }
        }

    }

    IEnumerator fallTime()
    {
        yield return new WaitForSeconds(0.3f);
        rdb.constraints = RigidbodyConstraints2D.None;
        yield return new WaitForSeconds(5);
        transform.position = originalPosition.position;
        rdb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

}
