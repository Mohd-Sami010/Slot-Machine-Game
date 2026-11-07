using UnityEngine;
using TMPro;
using System;

public class WalletManager : MonoBehaviour
{
    public static WalletManager Instance { get; private set; }

    [Header("Wallet Settings")]
    [SerializeField] private int startingBalance = 500;

    public int Balance { get; private set; }
    public int Score { get; private set;}

    public event Action<int> OnBalanceChanged;
    public event Action OnCoinsAdded;

    private void Awake()
    {
        Instance = this;

        Balance = startingBalance;
        Score = Balance;
        UpdateBalanceUI();
    }

    public bool TrySpend(int amount)
    {
        if (amount <= 0 || Balance < amount) return false;

        Balance -= amount;
        UpdateBalanceUI();

        return true;
    }

    public void AddCoins(int amount)
    {
        if (amount <= 0) return;

        Balance += amount;

        if (Balance > Score) Score = Balance;
        if (Balance > PlayerPrefs.GetInt("HighScore", 0)) PlayerPrefs.SetInt("HighScore", Balance);
        UpdateBalanceUI();
        OnCoinsAdded?.Invoke();
    }

    private void UpdateBalanceUI()
    {
        OnBalanceChanged?.Invoke(Balance);
    }
}
