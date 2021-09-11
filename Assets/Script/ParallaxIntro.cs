using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParallaxIntro : MonoBehaviour
{

    public Image bg;
    
    public float velocidade;
    private void Start()
    {
        bg = GetComponent<Image>();
    }
    void Update()
    {
        transform.position = new Vector3(transform.position.x - velocidade
        * Time.deltaTime * 1, 0, 0);
        if (transform.localPosition.x >= bg.preferredWidth)
        {
            transform.localPosition = new Vector3(transform.localPosition.x -
            bg.preferredWidth * 2, 0, 0);
        }
        else if (transform.localPosition.x <= -bg.preferredWidth)
        {
            transform.localPosition = new Vector3(transform.localPosition.x +
            bg.preferredWidth * 2, 0, 0);
        }
    }
}
