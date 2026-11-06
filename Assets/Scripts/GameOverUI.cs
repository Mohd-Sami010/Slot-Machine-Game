using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private Button restartButton;

    private void Start()
    {
        SlotMachineController.Instance.OnGameOver += ShowGameOverUI;
        restartButton.onClick.AddListener(RestartGame);
        gameObject.SetActive(false);
    }
    private void ShowGameOverUI()
    {
        gameObject.SetActive(true);
        scoreText.text = "Score: " + WalletManager.Instance.Score + "G";
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0) + "G";
    }
    private void RestartGame()
    {
        restartButton.interactable = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
