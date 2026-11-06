using UnityEngine;
using System;

public class SlotMachineController : MonoBehaviour
{
    public static SlotMachineController Instance { get; private set; }

    public event Action OnSpin;
    public event Action OnSpinComplete;
    public event Action OnInsufficientFunds;

    [SerializeField] private Sprite[] symbols;
    [SerializeField] private ReelController[] reels;

    [Header("Payout Configuration")]
    [SerializeField] private PayoutManager payoutManager;

    public int CurrentSpinBet { get; private set; }
    private bool isSpinning = false;
    private int reelsStopped = 0;
    
    int[] results = new int[3];

    private void Awake()
    {
        Instance = this;
    }
    public void Spin()
    {
        if (isSpinning || CurrentSpinBet > WalletManager.Instance.Balance) return;

        isSpinning = true;
        OnSpin?.Invoke();

        for (int i = 0; i < reels.Length; i++)
        {
            results[i] = UnityEngine.Random.Range(0, symbols.Length);
        }
        for (int i = 0; i < reels.Length; i++)
        {
            float spinDuration = 0.8f + (i * 0.4f);
            reels[i].PlaySpin(symbols, symbols[results[i]], spinDuration);
        }
        int bet = BetManager.Instance.SelectedBet;

        if (!WalletManager.Instance.TrySpend(bet))
        {
            Debug.Log("Insufficient balance!");
            OnInsufficientFunds?.Invoke();
            return;
        }

        CurrentSpinBet = bet;
        isSpinning = true;
        reelsStopped = 0;
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

        if (CheckWin())
        {
            Sprite winningSymbol = symbols[results[0]];

            int payout = payoutManager.CalculatePayout(winningSymbol, CurrentSpinBet);

            WalletManager.Instance.AddCoins(payout);

            Debug.Log($"WIN! Symbol: {winningSymbol.name}");
            Debug.Log($"Payout: {payout}G");
        }
        else Debug.Log("Better luck next time!");

        OnSpinComplete?.Invoke();
    }
    private bool CheckWin()
    {
        if (results == null || results.Length == 0) return false;

        for (int i = 1; i < results.Length; i++){
            if (results[i] != results[0]) return false;
        }
        return true;
    }
}
