using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fogo : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Se o objeto que colidir for o jogador
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.SetActive(false);

            Debug.Log("O jogador foi derrotado! \n");
            Debug.Log("Game Over!! Renicaindo a fase...");
            RestartLevel();
            
        }

        //AguaBalde agua = collision.GetComponent<AguaBalde>();
        
        if (collision.GetComponent<AguaBalde>() != null)
        {
            Debug.Log("Fogo foi apagado pela água!");
            collision.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }

    private void RestartLevel(){
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}