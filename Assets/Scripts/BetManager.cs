
using System;
using UnityEngine;

public class BetManager : MonoBehaviour
{
    public static BetManager Instance { get; private set; }

    [Header("Bet Configuration")]
    [SerializeField] private int bet1Cost = 5;
    [SerializeField] private int bet2Cost = 20;
    [SerializeField] private int bet3Cost = 50;

    public int Bet1Cost => bet1Cost;
    public int Bet2Cost => bet2Cost;
    public int Bet3Cost => bet3Cost;

    public int SelectedBet { get; private set; }

    public event Action<int> OnBetChanged;

    private void Awake()
    {
        Instance = this;
        SelectedBet = 5;
    }
    private void Start()
    {
        SlotMachineController.Instance.OnSpinComplete += () =>
        {
            if (WalletManager.Instance.Balance < SelectedBet)
            {
                if (WalletManager.Instance.Balance >= Bet1Cost) SelectBet(Bet1Cost);
                else if (WalletManager.Instance.Balance >= Bet2Cost) SelectBet(Bet2Cost);
                else if (WalletManager.Instance.Balance >= Bet3Cost) SelectBet(Bet3Cost);
            }
        };
    }

    public void SelectBet(int amount)
    {
        if (SelectedBet == amount) return;

        SelectedBet = amount;
        OnBetChanged?.Invoke(SelectedBet);
    }
}