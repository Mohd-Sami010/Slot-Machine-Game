
using System;
using UnityEngine;

public class BetManager : MonoBehaviour
{
    public static BetManager Instance { get; private set; }

    [Header("Bet Configuration")]
    [SerializeField] private int bet1Cost = 5;
    [SerializeField] private int bet2Cost = 20;
    [SerializeField] private int bet3Cost = 50;

    [Header("Default Selection")]
    [SerializeField, Range(1, 3)] private int startingBetOption = 1;

    public int Bet1Cost => bet1Cost;
    public int Bet2Cost => bet2Cost;
    public int Bet3Cost => bet3Cost;

    public int SelectedBet { get; private set; }

    public event Action<int> OnBetChanged;

    private void Awake()
    {
        Instance = this;
        SelectedBet = GetBetCost(startingBetOption);
    }

    public void SelectBet(int amount)
    {
        if (SelectedBet == amount) return;

        SelectedBet = amount;
        OnBetChanged?.Invoke(SelectedBet);
    }

    private int GetBetCost(int option)
    {
        switch (option)
        {
            case 1: return bet1Cost;
            case 2: return bet2Cost;
            case 3: return bet3Cost;
            default: return bet1Cost;
        }
    }
}