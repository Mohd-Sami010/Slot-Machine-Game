using UnityEngine;
using System;

public class SlotMachineController : MonoBehaviour
{
    public static SlotMachineController Instance { get; private set; }

    public event Action OnSpin;
    public event Action OnSpinComplete;
    public event Action OnInsufficientFunds;
    public event Action OnGameOver;

    [SerializeField] private Sprite[] symbols;
    [SerializeField] private ReelController[] reels;

    [Header("Payout Configuration")]
    [SerializeField] private PayoutManager payoutManager;

    public int CurrentSpinBet { get; private set; }
    private bool isSpinning = false;
    private int reelsStopped = 0;
    
    int[] results;

    private void Awake()
    {
        Instance = this;
        results = new int[reels.Length];
    }
    
    public void Spin()
    {
        if (isSpinning) return;
        int bet = BetManager.Instance.SelectedBet;

        // Deduct bet BEFORE starting the spin.
        if (bet > WalletManager.Instance.Balance || !WalletManager.Instance.TrySpend(bet))
        {
            Debug.Log("Insufficient balance!");
            OnInsufficientFunds?.Invoke();
            return;
        }

        CurrentSpinBet = bet;
        isSpinning = true;
        reelsStopped = 0;

        for (int i = 0; i < reels.Length; i++)
        {
            results[i] = UnityEngine.Random.Range(0, symbols.Length);
        }
        OnSpin?.Invoke();

        // Start reel animations.
        for (int i = 0; i < reels.Length; i++)
        {
            float spinDuration = 0.8f + (i * 0.4f);
            reels[i].PlaySpin(symbols, symbols[results[i]], spinDuration);
        }
    }

    public void OnReelStopped()
    {
        reelsStopped++;
        if (reelsStopped >= reels.Length)
        {
            reelsStopped = 0;
            CompleteSpin();
        }
    }
    
    private void CompleteSpin()
    {
        isSpinning = false;

        // Convert result indices into their corresponding sprites.
        Sprite[] finalCombination = new Sprite[results.Length];

        for (int i = 0; i < results.Length; i++)
        {
            finalCombination[i] = symbols[results[i]];
        }

        int payout = payoutManager.CalculatePayout(finalCombination, CurrentSpinBet);

        if (payout > 0) WalletManager.Instance.AddCoins(payout);
        try
        {
            OnSpinComplete?.Invoke();
        }
        finally
        {
            if (WalletManager.Instance.Balance < BetManager.Instance.Bet1Cost &&
                WalletManager.Instance.Balance < BetManager.Instance.Bet2Cost &&
                WalletManager.Instance.Balance < BetManager.Instance.Bet3Cost)
            {
                OnGameOver?.Invoke();
            }
        }
    }
}
