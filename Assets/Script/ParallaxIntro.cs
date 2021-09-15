using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParallaxIntro : MonoBehaviour
{

    public Sprite bg;
    
    public float velocidade;
    private void Start()
    {
        bg = GetComponent<Sprite>();
    }
    void Update()
    {
        transform.position = new Vector3(transform.position.x - velocidade
        * Time.deltaTime * 1, 0, 0);
        
    }
}
