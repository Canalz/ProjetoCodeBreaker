using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gerencia a geração e visualização de uma grid 2D no jogo.
/// </summary>
public class GridManager : MonoBehaviour
{
    public int gridWidth = 10;   // Largura da grid em células
    public int gridHeight = 10;  // Altura da grid em células
    public float cellSize = 1f;  // Tamanho de cada célula da grid

    private void Start()
    {
        GenerateGrid();  // Gera a grid ao iniciar
    }

    /// <summary>
    /// Gera uma grid visual para ajudar no posicionamento dos objetos.
    /// </summary>
    void GenerateGrid()
    {
        // Desenha as linhas verticais e horizontais da grid.
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 cellPosition = new Vector3(x * cellSize, y * cellSize, 0);
                Debug.DrawLine(cellPosition, cellPosition + Vector3.right * cellSize, Color.white, 100f);
                Debug.DrawLine(cellPosition, cellPosition + Vector3.up * cellSize, Color.white, 100f);
            }
        }

        // Desenha as bordas da grid.
        Vector3 bottomRight = new Vector3(gridWidth * cellSize, 0, 0);
        Vector3 topLeft = new Vector3(0, gridHeight * cellSize, 0);
        Debug.DrawLine(bottomRight, topLeft + Vector3.up * gridHeight, Color.white, 100f);
        Debug.DrawLine(topLeft, bottomRight + Vector3.right * gridWidth, Color.white, 100f);
    }
}