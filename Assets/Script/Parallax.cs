using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//------------------------------------------------SCRIPT PARA MOVER O FUNDO DO JOGO
public class Parallax : MonoBehaviour
{
    private float lenght;  //Variável para pegar a largura do sprite
    private float startPos;  //Variável para pegar a posição inicial do Background

    private Transform cam;  //Variável para pegar a MainCamera

    [SerializeField] private float parallaxSpeed;  //Variável para determinar a velocidade em que os sprites se movem
    void Start()
    {
        startPos = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
        cam = Camera.main.transform;
    }
    void Update()
    {
        //Reposicionar a imagem depois que ela acabar (Scroll infinito)
        float repos = cam.transform.position.x * (1 - parallaxSpeed);

        //Movimentação das imagens
        float distance = cam.transform.position.x * parallaxSpeed;
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        //Scroll infinito
        if (repos > startPos + lenght)
        {
            startPos += lenght;
        }
        else if (repos < startPos - lenght)
        {
            startPos -= lenght;
        }
    }
}
