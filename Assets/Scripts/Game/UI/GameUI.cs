using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
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
        });
        bet2Button.onClick.AddListener(() => {
            BetManager.Instance.SelectBet(BetManager.Instance.Bet2Cost);
        });
        bet3Button.onClick.AddListener(() => {
            BetManager.Instance.SelectBet(BetManager.Instance.Bet3Cost);
        });
    }
    private void OnSpinStarted()
    {
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

        EnableOrDisableBetButtons(BetManager.Instance.SelectedBet);
        DisableOutOfBalanceBetButtons();
    }

    private void UpdateBalanceUI(int newBalance)
    {
        balanceText.text = newBalance + "G";
    }

    private void UpdateBetUI(int newBet)
    {
        betAmountText.text = newBet + "G";

        EnableOrDisableBetButtons(newBet);
        DisableOutOfBalanceBetButtons();
    }
    private void EnableOrDisableBetButtons(int newBet)
    {
        if (newBet == BetManager.Instance.Bet1Cost)
        {
            bet1Button.interactable = false;
            bet2Button.interactable = true;
            bet3Button.interactable = true;
        }
        else if (newBet == BetManager.Instance.Bet2Cost)
        {
            bet1Button.interactable = true;
            bet2Button.interactable = false;
            bet3Button.interactable = true;
        }
        else if (newBet == BetManager.Instance.Bet3Cost)
        {
            bet1Button.interactable = true;
            bet2Button.interactable = true;
            bet3Button.interactable = false;
        }
    }
    private void DisableOutOfBalanceBetButtons()
    {
        bet1Text.color = Color.white;
        bet2Text.color = Color.white;
        bet3Text.color = Color.white;

        if (BetManager.Instance.Bet1Cost > WalletManager.Instance.Balance)
        {
            bet1Button.interactable = false;
            bet1Text.color = Color.red;
        }
        if (BetManager.Instance.Bet2Cost > WalletManager.Instance.Balance)
        {
            bet2Button.interactable = false;
            bet2Text.color = Color.red;
        }
        if (BetManager.Instance.Bet3Cost > WalletManager.Instance.Balance)
        {
            bet3Button.interactable = false;
            bet3Text.color = Color.red;
        }
    }
}