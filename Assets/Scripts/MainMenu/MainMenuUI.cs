using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private MainMenuAudioManager audioManager;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private GameObject loadingScreen;

    [Header("Lever")]
    [SerializeField] private Image leverImage;
    [SerializeField] private Sprite leverDownSprite;

    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button itchButton;
    [SerializeField] private Button githubButton;
    [SerializeField] private Button linkedinButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        loadingScreen.SetActive(false);
        playButton.onClick.AddListener(() => {
            audioManager.PlayLeverPullSound();
            leverImage.sprite = leverDownSprite;
            loadingScreen.SetActive(true);
            Invoke(nameof(LoadGameScene), 0.5f);
        });

        itchButton.onClick.AddListener(() => {
            Application.OpenURL("https://samicode-games.itch.io/");
            audioManager.PlayButtonClickSound();
        });
        githubButton.onClick.AddListener(() => {
            Application.OpenURL("https://github.com/Mohd-Sami010");
            audioManager.PlayButtonClickSound();
        });
        linkedinButton.onClick.AddListener(() => {
            Application.OpenURL("https://www.linkedin.com/in/mohd-sami-04ab43377/");
            audioManager.PlayButtonClickSound();
        });
        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });

        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0) + "G";
    }
    
    private void LoadGameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);            
    }
}
