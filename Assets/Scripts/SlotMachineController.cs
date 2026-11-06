using UnityEngine;

public class SlotMachineController : MonoBehaviour
{
    public static SlotMachineController Instance { get; private set; }

    public event System.Action OnSpin;
    public event System.Action OnSpinComplete;

    [SerializeField] private Sprite[] symbols;
    [SerializeField] private ReelController[] reels;

    private bool isSpinning = false;
    private int reelsStopped = 0;
    
    int[] results = new int[3];

    private void Awake()
    {
        Instance = this;
    }
    public void Spin()
    {
        if (isSpinning) return;

        isSpinning = true;
        OnSpin?.Invoke();

        for (int i = 0; i < reels.Length; i++)
        {
            results[i] = Random.Range(0, symbols.Length);
        }
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

        if (results[0] == results[1] && results[1] == results[2])
        {
            Debug.Log("JACKPOT! YOU WIN!");
        }
        else
        {
            Debug.Log("Better luck next time!");
        }
        OnSpinComplete?.Invoke();
    }
}
