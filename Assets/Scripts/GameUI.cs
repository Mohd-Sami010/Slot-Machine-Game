using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject clickLeverPrompt;
    [SerializeField] private GameObject betButtonsPanel;
    [Header("UI Text")]
    [SerializeField] private TextMeshProUGUI balanceText;
    [SerializeField] private TextMeshProUGUI betAmountText;

    [Header("Bet Buttons")]
    [SerializeField] private Button bet1Button;
    [SerializeField] private Button bet2Button;
    [SerializeField] private Button bet3Button;

    [Header("Bet Button Texts")]
    [SerializeField] private TextMeshProUGUI bet1Text;
    [SerializeField] private TextMeshProUGUI bet2Text;
    [SerializeField] private TextMeshProUGUI bet3Text;

    [Header("Animators")]
    [SerializeField] private Animator balanceTextAnimator;
    [SerializeField] private Animator betButtonsAnimator;

    private void Start()
    {
        SlotMachineController.Instance.OnSpin += OnSpinStarted;
        SlotMachineController.Instance.OnSpinComplete += OnSpinComplete;

        WalletManager.Instance.OnBalanceChanged += UpdateBalanceUI;
        WalletManager.Instance.OnCoinsAdded += () =>
        {
            balanceTextAnimator.SetTrigger("CoinsAdded");   
        };

        BetManager.Instance.OnBetChanged += UpdateBetUI;

        UpdateBalanceUI(WalletManager.Instance.Balance);
        UpdateBetUI(BetManager.Instance.SelectedBet);

        bet1Text.text = BetManager.Instance.Bet1Cost + "G";
        bet2Text.text = BetManager.Instance.Bet2Cost + "G";
        bet3Text.text = BetManager.Instance.Bet3Cost + "G";

        bet1Button.onClick.AddListener(() => {
            BetManager.Instance.SelectBet(BetManager.Instance.Bet1Cost);
            clickLeverPrompt.SetActive(true);
        });
        bet2Button.onClick.AddListener(() => {
            BetManager.Instance.SelectBet(BetManager.Instance.Bet2Cost);
            clickLeverPrompt.SetActive(true);
        });
        bet3Button.onClick.AddListener(() => {
            BetManager.Instance.SelectBet(BetManager.Instance.Bet3Cost);
            clickLeverPrompt.SetActive(true);
        });
    }
    private void OnSpinStarted()
    {
        clickLeverPrompt.SetActive(false);
        
        betButtonsAnimator.SetTrigger("Hide");
        Invoke(nameof(DeactiveBetButtons), 0.2f);
    }
    private void DeactiveBetButtons()
    {
        betButtonsPanel.SetActive(false);
    }
    private void OnSpinComplete()
    {
        betButtonsPanel.SetActive(true);
        betButtonsAnimator.SetTrigger("Show");
    }

    private void UpdateBalanceUI(int newBalance)
    {
        balanceText.text = newBalance + "G";
    }

    private void UpdateBetUI(int newBet)
    {
        betAmountText.text = newBet + "G";
    }
}