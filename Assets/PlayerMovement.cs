using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla o movimento do jogador e a interação com outros objetos na fase.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    public float moveTime = 0.2f;  // Tempo de movimento entre células
    private bool isMoving;
    private ActionManager actionManager;
    public LayerMask rayCastLayer;

    private void Start()
    {
        // Referência ao ActionManager na cena
        actionManager = FindObjectOfType<ActionManager>();
        transform.position = SnapToGrid(transform.position);
    }

    private void Update()
    {
        // Só permite movimento se o jogador não estiver se movendo
        if (!isMoving)
        {
            // Controle de movimento do jogador com teclas
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                TryMove(Vector3.up);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                TryMove(Vector3.down);
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                TryMove(Vector3.left);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                TryMove(Vector3.right);
            }

            // Desfaz a última ação ao apertar "R"
            if (Input.GetKeyDown(KeyCode.R))
            {
                actionManager.UndoLastAction();
            }
        }
    }

    /// <summary>
    /// Tenta mover o jogador e outros objetos afetados.
    /// </summary>
    /// <param name="direction">Direção para onde o jogador quer se mover.</param>
    private void TryMove(Vector3 direction)
    {
        // Grava o estado atual dos artefatos antes do movimento
        List<GameObject> allArtifacts = GetAllArtifacts();
        actionManager.RecordAction(allArtifacts);

        // Verifica se há uma parede na direção
        if (CheckWall(direction))
        {
            // Se houver uma parede, não faz o movimento
            Debug.Log("Parede bloqueando o movimento!");
            return;
        }

        // Verifica se há algum objeto empurrável na direção do movimento
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, LayerMask.GetMask("Default"));

        if (hit.collider != null && hit.collider.CompareTag("Pushable"))
        {
            PushableObject pushable = hit.collider.GetComponent<PushableObject>();
            if (pushable != null && !pushable.isBeingPushed)
            {
                StartCoroutine(pushable.Move(direction));  // Inicia o movimento do objeto empurrável
                return;
            }
        }
        
        // Move o jogador
        StartCoroutine(MovePlayer(direction));
    }

    /// <summary>
    /// Move o jogador de uma célula para outra.
    /// </summary>
    /// <param name="direction">Direção do movimento.</param>
    private IEnumerator MovePlayer(Vector3 direction)
    {
        isMoving = true;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + direction;

        float elapsedTime = 0;

        // Movimenta o jogador de forma suave entre as posições
        while (elapsedTime < moveTime)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Alinha o jogador à grid
        transform.position = SnapToGrid(endPosition);
        isMoving = false;
    }

    /// <summary>
    /// Retorna uma lista de todos os objetos na fase, incluindo o jogador.
    /// </summary>
    private List<GameObject> GetAllArtifacts()
    {
        List<GameObject> artifacts = new List<GameObject>();
        artifacts.Add(gameObject); // Adiciona o próprio player

        // Adiciona todos os objetos empurráveis
        PushableObject[] pushableObjects = FindObjectsOfType<PushableObject>();
        foreach (PushableObject obj in pushableObjects)
        {
            artifacts.Add(obj.gameObject);
        }

        return artifacts;
    }

    /// <summary>
    /// Alinha a posição do objeto ao grid.
    /// </summary>
    private Vector3 SnapToGrid(Vector3 position)
    {
        float gridSize = 1f;  // Tamanho das células da grid
        float snappedX = Mathf.Round(position.x / gridSize) * gridSize;
        float snappedY = Mathf.Round(position.y / gridSize) * gridSize;
        return new Vector3(snappedX, snappedY, position.z);
    }

    /// <summary>
    /// Verifica se há uma parede na direção especificada.
    /// </summary>
    private bool CheckWall(Vector3 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, rayCastLayer);
        if (hit.collider != null && hit.collider.CompareTag("Wall"))
        {
            Debug.Log("Encontrou parede");
            return true;
        }
        
        return false;
    }
}