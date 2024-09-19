using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public GameObject baldeAguaPrefab;  // Prefab do objeto BaldeAgua que será clonado
    public Transform spawnPoint;  // Ponto onde as cópias do BaldeAgua serão instanciadas
    public int quantidadeDeCopias = 1;  // Quantidade de cópias a serem criadas

    // Detecta quando algo entra no colisor da chave
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto que colidiu é o bloco correto
        if (other.gameObject.CompareTag("Key"))
        {
            Debug.Log("Parabéns! O bloco foi arrastado até a chave!");

            // Cria cópias do BaldeAgua no ponto especificado
            for (int i = 0; i < quantidadeDeCopias; i++)
            {
                Instantiate(baldeAguaPrefab, spawnPoint.position, Quaternion.identity);
            }
        }
    }
}
