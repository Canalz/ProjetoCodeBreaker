using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gerencia o histórico de ações da fase para permitir o desfazer de movimentos.
/// </summary>
public class ActionManager : MonoBehaviour
{
    // Pilha que armazena o histórico de ações, cada uma contendo um dicionário com o objeto e sua posição.
    private Stack<Dictionary<GameObject, Vector3>> actionHistory = new Stack<Dictionary<GameObject, Vector3>>();

    /// <summary>
    /// Grava o estado atual de todos os artefatos da fase.
    /// </summary>
    /// <param name="artifacts">Lista de todos os objetos a serem gravados (incluindo o player).</param>
    public void RecordAction(List<GameObject> artifacts)
    {
        Dictionary<GameObject, Vector3> actionSnapshot = new Dictionary<GameObject, Vector3>();

        // Para cada artefato, grava sua posição atual.
        foreach (GameObject artifact in artifacts)
        {
            actionSnapshot[artifact] = artifact.transform.position;
        }

        // Adiciona o snapshot à pilha de ações.
        actionHistory.Push(actionSnapshot);
    }

    /// <summary>
    /// Desfaz a última ação registrada, restaurando as posições dos artefatos.
    /// </summary>
    public void UndoLastAction()
    {
        if (actionHistory.Count > 0)
        {
            // Recupera o último estado dos artefatos.
            Dictionary<GameObject, Vector3> lastAction = actionHistory.Pop();

            // Restaura a posição de cada artefato.
            foreach (KeyValuePair<GameObject, Vector3> entry in lastAction)
            {
                entry.Key.transform.position = entry.Value;
                entry.Key.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Nenhuma ação para desfazer.");
        }
    }
}