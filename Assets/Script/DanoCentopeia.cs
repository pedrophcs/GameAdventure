using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanoCentopeia : MonoBehaviour
{
    public float timerCent;
    private Player player;
    private Centipede centipede;
    public bool acertou;
    private AtkDetect atkDetect;
    private BoxCollider2D atkCollider;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType(typeof(Player)) as Player;
        atkDetect = FindObjectOfType(typeof(AtkDetect)) as AtkDetect;
        atkCollider = GetComponent<BoxCollider2D>();
        centipede = FindObjectOfType(typeof(Centipede)) as Centipede;
    }

    // Update is called once per frame
    void Update()
    {
        if (acertou)
        {
            timerCent += Time.deltaTime;
        }
        if (timerCent > 2)
        {
            acertou = false;
            timerCent = 0;
        }
    }
    private void FixedUpdate()
    {
        if (atkDetect.ataque)
        {
            
            centipede.anim.SetBool("Atk1", true);
            atkCollider.enabled = true;
        }
        else
        {
            centipede.anim.SetBool("Atk1", false);
            atkCollider.enabled = false;
        }
    }
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            acertou = true;
        }
    }

}
