using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkDetect : MonoBehaviour
{
    public bool ataque;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag =="Player" )
        {
            ataque = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ataque = false;
        }
    }
}
