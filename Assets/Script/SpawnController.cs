using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnController : MonoBehaviour
{
    [SerializeField] GameObject player,text;
    [SerializeField] Transform deathPoint;
    [SerializeField] Transform respawnPoint;
    [SerializeField] Transform checkpoint;
    [SerializeField] GameObject[] bats;
    [SerializeField] Renderer rend, rendB;
    CharacterLife lif;
    public bool bossDeath;
    Tartoga tar;
   

    private void Start()
    {
        lif = FindObjectOfType(typeof(CharacterLife)) as CharacterLife;
        tar = FindObjectOfType(typeof(Tartoga)) as Tartoga;
        bossDeath = false;
        StartCoroutine(FadeOut());
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        bats = GameObject.FindGameObjectsWithTag("Enemy");
       // PlayerRecall();
        if (player.transform.position.y < deathPoint.position.y)
        {
            Death();
        }
        if(lif.isAlive == false && Input.GetKeyDown(KeyCode.Escape))
        {
            for(int i = 0; i< bats.Length;i++)
            {
                if(bats[i] != bats[27])
                {
                    Destroy(bats[i]);
                }
                
            }
            StartCoroutine(FadeIn());
            text.SetActive(true);
            StartCoroutine(trocafase(1));
        }
        if(tar.isAlive == false && Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(FadeIn());
            text.SetActive(true);
            StartCoroutine(trocafase(2));
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            respawnPoint.position = this.transform.position;
        }
    }
   public void Death()
    {
        AudioController.instance.PlaySounds(Sound.Fade);
       
        player.transform.position = respawnPoint.position;
        StartCoroutine(FadeOut());
    }
 

    public IEnumerator FadeOut()
    {

        for (float f = 1; f >= 0; f -= 0.01f)
        {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            rendB.material.color = c;
            yield return null;
        }


    }
   
    IEnumerator trocafase(int a)
    {
        yield return new WaitForSeconds(3);
        if (a == 1)
        {
           
            SceneManager.LoadScene(2);
        }
        if(a == 2)
        {
           
            SceneManager.LoadScene(0);
        }
       
    }
    public IEnumerator FadeIn()
    {
        for (float f = 0; f <= 1; f += 0.01f)
        {
            Color c = rendB.material.color;
            c.a = f;
            rendB.material.color = c;
            yield return null;
        }
        yield return new WaitForSeconds(1);




    }
}
