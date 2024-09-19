using UnityEngine;
using UnityEngine.SceneManagement; // Para recarregar a fase
using UnityEngine.UI; // Para controlar a UI

public class VictoryTrigger : MonoBehaviour
{
    public GameObject victoryPanel;  // Painel da UI que será ativado ao vencer
    public Text victoryText;         // Texto que aparecerá na tela de vitória

    private bool playerHasWon = false; // Variável para controlar se o jogador venceu

    private void Start()
    {
        // Certifique-se de que o painel de vitória está desativado no início
        victoryPanel.SetActive(false);
    }

    // Detecta colisão com o player
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Ativa o painel de vitória e exibe a mensagem
            victoryPanel.SetActive(true);
            victoryText.text = "Parabéns! Você venceu a fase!";
            playerHasWon = true; // Marca que o jogador venceu
            Time.timeScale = 0; // Pausa o jogo
        }
    }

    // Função para repetir a fase, mas só se o jogador já venceu
    public void RestartLevel()
    {
        if (playerHasWon) 
        {
            Time.timeScale = 1; // Despausa o jogo
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recarrega a fase atual
        }
    }

    // Função para sair do jogo (você pode mudar isso para carregar um menu principal, por exemplo)
    public void QuitGame()
    {
        if (playerHasWon)
        {
            Time.timeScale = 1; // Despausa o jogo
            Application.Quit(); // Fecha o jogo (não funciona no editor da Unity)
        }
    }
}