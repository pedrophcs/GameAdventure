using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rdbenemy;
    private void Start()
    {
        rdbenemy = GetComponent<Rigidbody2D>();
    }
    //GENIALIDADE
}
