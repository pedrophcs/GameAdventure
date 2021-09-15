using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public enum Sound
{
    at1,at2,chest,runes,mobD, batSpa,Fade,playerDa,jumpTur,w
}
public class AudioController : MonoBehaviour
{
    public AudioClip atk1, atk2, bau, runas, mobDeath, batSpawn, fade, playerDamage, jumpTurtle, win;
    public AudioSource sing, boss, victory;
    public static AudioController instance;
    public int count;
    void Start()
    {
        instance = this;
    }

    
    void Update()
    {
        ButtonStart();
    }

    public void PlaySounds(Sound currentSound)
    {
        switch (currentSound)
        {
            case Sound.at1:
                instance.sing.PlayOneShot(instance.atk1);
                instance.boss.PlayOneShot(instance.atk1);
                break;
            case Sound.at2:
                instance.sing.PlayOneShot(instance.atk2);
                instance.boss.PlayOneShot(instance.atk2);
                break;
            case Sound.chest:
                instance.sing.PlayOneShot(instance.bau);
                instance.boss.PlayOneShot(instance.bau);
                break;
            case Sound.runes:
                instance.sing.PlayOneShot(instance.runas);
                instance.boss.PlayOneShot(instance.runas);
                break;
            case Sound.mobD:
                instance.sing.PlayOneShot(instance.mobDeath);
                instance.boss.PlayOneShot(instance.mobDeath);
                break;
               
            case Sound.batSpa:
                instance.sing.PlayOneShot(instance.batSpawn);
                instance.boss.PlayOneShot(instance.batSpawn);
                break;
            case Sound.Fade:
                instance.sing.PlayOneShot(instance.fade);
                instance.boss.PlayOneShot(instance.fade);
                break;
            case Sound.playerDa:
                instance.sing.PlayOneShot(instance.playerDamage);
                instance.boss.PlayOneShot(instance.playerDamage);
                break;
            case Sound.jumpTur:
                instance.sing.PlayOneShot(instance.jumpTurtle);
                instance.boss.PlayOneShot(instance.jumpTurtle);
                break;
            case Sound.w:
                instance.sing.PlayOneShot(instance.win);
                instance.boss.PlayOneShot(instance.win);
                break;
        }
    }

    public void ButtonStart()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.Period))
        {
            SceneManager.LoadScene(2);
        }
        
    }


}
