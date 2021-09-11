using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] RectTransform shown;
    [SerializeField] RectTransform hidden;
    [SerializeField] RectTransform status;

    [SerializeField] 
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            StartCoroutine(ShowStatus());
            
        }
    }

    IEnumerator ShowStatus()
    {
        status.transform.position = shown.transform.position;
        yield return new WaitForSeconds(2);
        status.transform.position = hidden.transform.position;
    }
}
