using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;

    private void Start()
    {
        SlotMachineController.Instance.OnGameOver += ShowGameOverUI;
        restartButton.onClick.AddListener(() =>
        {
            restartButton.interactable = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   
        });
        menuButton.onClick.AddListener(() =>
        {
            menuButton.interactable = false;
            SceneManager.LoadScene(0);
        });
        gameObject.SetActive(false);
    }
    private void ShowGameOverUI()
    {
        gameObject.SetActive(true);
        scoreText.text = "Score: " + WalletManager.Instance.Score + "G";
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0) + "G";
    }
}
