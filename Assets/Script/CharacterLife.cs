using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLife : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private float health;
    //[HideInInspector] public bool isDead = false;
    public bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            anim.SetTrigger("Hurt");
            health -= damage;
            if (health <= 0)
            {
                anim.SetTrigger("Death");
                isDead = true;
                Destroy(this.gameObject);
            }
        }
    }
}
