using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AguaBalde : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Fogo>() != null)
        {
            Debug.Log("Água apagou o fogo!");
            collision.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}