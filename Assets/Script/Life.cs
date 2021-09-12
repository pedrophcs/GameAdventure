using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
   
    public Sprite[] Lives;
    public Image Display;
    void Start()
    {
        
    }
    public void UpdateLives(int currentLives)
    {
       
        Display.sprite = Lives[currentLives];
    }
}
