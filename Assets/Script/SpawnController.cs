using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] GameObject player,pref;
    [SerializeField] Transform deathPoint;
    [SerializeField] Transform respawnPoint;
    [SerializeField] Transform checkpoint;

    [SerializeField] Renderer rend;
    [SerializeField] bool isAlive;

    private void Start()
    {
        
        StartCoroutine(FadeOut());
    }
    void Update()
    {
       
        if (player == null)
        {
            Instantiate(pref);
            player = GameObject.FindGameObjectWithTag("Player");
            Death();
        }
        if (player.transform.position.y < deathPoint.position.y && isAlive == true)
        {
            Death();
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            respawnPoint.position = this.transform.position;
        }
    }
    void Death()
    {
        StartCoroutine(FadeIn());
        player.transform.position = respawnPoint.position;
        StartCoroutine(FadeOut());
    }
    IEnumerator FadeOut()
    {

        for (float f = 1; f >= 0; f -= 0.01f)
        {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return null;
        }
        yield return new WaitForSeconds(1);


    }
    IEnumerator FadeIn()
    {
        for (float f = 0; f <= 1; f += 0.01f)
        {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return null;
        }
        yield return new WaitForSeconds(1);




    }
}
