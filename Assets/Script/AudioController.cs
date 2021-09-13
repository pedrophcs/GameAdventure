using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum Sound
{
    atk1,atk2,
}
public class AudioController : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioSource sing;
    public static AudioController instance;
    public int count;
    [SerializeField] GameObject[] fases; 
    void Start()
    {
        instance = this;
    }

    
    void Update()
    {
        if (count == 0)
        {
           
        }
        else if (count == 1)
        {
            
        }
        else if(count == 2)
        {
          
            
        }
    }

    public static void PlaySounds(Sound currentSound)
    {
        //switch(currentSound)
        //{
        //    case Sound.C:
        //        break;
        //}
    }

    public void ButtonStart()
    {
        fases[0].SetActive(false);
        fases[1].SetActive(true);
    }
}
