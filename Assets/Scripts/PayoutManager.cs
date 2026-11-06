
using UnityEngine;

public class PayoutManager : MonoBehaviour
{
    [System.Serializable]
    public class SymbolPayout
    {
        public Sprite symbol;
        [Min(0)] public int multiplier = 1;
    }

    [Header("Symbol Payout Configuration")]
    [SerializeField] private SymbolPayout[] symbolPayouts;

    public int CalculatePayout(Sprite winningSymbol, int betAmount)
    {
        foreach (SymbolPayout payout in symbolPayouts)
        {
            if (payout.symbol == winningSymbol)
            {
                return betAmount * payout.multiplier;
            }
        }

        Debug.LogWarning($"No payout configured for {winningSymbol.name}");
        return 0;
    }
}
