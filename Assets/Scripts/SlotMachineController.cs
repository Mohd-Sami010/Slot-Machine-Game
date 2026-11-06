using UnityEngine;

public class SlotMachineController : MonoBehaviour
{
    public static SlotMachineController Instance { get; private set; }

    public event System.Action OnSpin;
    public event System.Action<int, int, int> OnResult;

    private void Awake()
    {
        Instance = this;
    }
    public void Spin()
    {
        Debug.Log("Spinning the slot machine!");
        OnSpin?.Invoke();
        Invoke(nameof(ShowResult), 2f);
    }
    private void ShowResult()
    {
        int slot1 = Random.Range(0, 4);
        int slot2 = Random.Range(0, 4);
        int slot3 = Random.Range(0, 4);

        OnResult?.Invoke(slot1, slot2, slot3);
    }
}
