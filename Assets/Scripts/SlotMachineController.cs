using UnityEngine;

public class SlotMachineController : MonoBehaviour
{
    public static SlotMachineController Instance { get; private set; }

    public event System.Action OnSpin;

    [SerializeField] private Sprite[] symbols;
    [SerializeField] private ReelController[] reels;

    private bool isSpinning = false;

    private void Awake()
    {
        Instance = this;
    }
    public void Spin()
    {
        if (isSpinning) return;

        isSpinning = true;
        Debug.Log("Spinning the slot machine!");
        OnSpin?.Invoke();
        Invoke(nameof(ShowResult), 1f);
    }
    private void ShowResult()
    {
        int[] results = new int[reels.Length];

        for (int i = 0; i < reels.Length; i++)
        {
            results[i] = Random.Range(0, symbols.Length);
            reels[i].DisplaySymbol(symbols[results[i]]);
        }

        isSpinning = false;

        if (results[0] == results[1] && results[1] == results[2])
        {
            Debug.Log("JACKPOT! YOU WIN!");
        }
        else
        {
            Debug.Log("Better luck next time!");
        }
    }
}
