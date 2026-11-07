
using UnityEngine;

public class PayoutManager : MonoBehaviour
{
    [System.Serializable]
    public class SymbolCombination
    {
        public Sprite[] symbols = new Sprite[3];
        public int multiplier = 1;
    }

    [Header("Winning Combinations")]
    [SerializeField] private SymbolCombination[] combinations;

    public int CalculatePayout(Sprite[] results, int betAmount)
    {
        foreach (SymbolCombination combination in combinations)
        {
            if (combination.symbols == null || combination.symbols.Length != results.Length) continue;

            bool isMatch = true;

            for (int i = 0; i < results.Length; i++)
            {
                if (combination.symbols[i] != results[i])
                {
                    isMatch = false;
                    break;
                }
            }

            if (isMatch) return betAmount * combination.multiplier;
        }
        return 0;
    }
}
