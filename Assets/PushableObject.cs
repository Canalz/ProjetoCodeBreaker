using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Define o comportamento de objetos empurráveis no jogo.
/// </summary>
public class PushableObject : MonoBehaviour
{
    public bool isBeingPushed = false;  // Indica se o objeto está sendo empurrado
    public float moveTime = 0.2f;       // Tempo de movimento do objeto

    private void Start()
    {
        transform.position = SnapToGrid(transform.position);
    }

    /// <summary>
    /// Desfaz o movimento, retornando o objeto à sua posição anterior.
    /// </summary>
    /// <param name="oldPosition">A posição para onde o objeto deve retornar.</param>
    public void UndoMove(Vector3 oldPosition)
    {
        StopAllCoroutines();  // Para qualquer movimento em andamento
        transform.position = SnapToGrid(oldPosition);
        isBeingPushed = false;
    }

    /// <summary>
    /// Move o objeto na direção especificada.
    /// </summary>
    /// <param name="direction">Direção do movimento.</param>
    public IEnumerator Move(Vector3 direction)
    {
        isBeingPushed = true;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + direction;

        float elapsedTime = 0;

        // Movimenta o objeto de forma suave entre as posições
        while (elapsedTime < moveTime)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = SnapToGrid(endPosition); // Garante que o bloco termine alinhado à grid
        isBeingPushed = false;
    }

    /// <summary>
    /// Alinha a posição do objeto ao grid, garantindo que ele se encaixe corretamente nas células.
    /// </summary>
    /// <param name="position">A posição atual do objeto.</param>
    /// <returns>A posição ajustada para o grid.</returns>
    private Vector3 SnapToGrid(Vector3 position)
    {
        float gridSize = 1f;  // Tamanho das células da grid
        float snappedX = Mathf.Round(position.x / gridSize) * gridSize;
        float snappedY = Mathf.Round(position.y / gridSize) * gridSize;
        return new Vector3(snappedX, snappedY, position.z);
    }
}