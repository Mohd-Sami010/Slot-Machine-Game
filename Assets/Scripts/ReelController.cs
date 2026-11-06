using Unity.VisualScripting;
using UnityEngine;

public class ReelController : MonoBehaviour
{
    [SerializeField] private Sprite[] symbols;
    
    [Header("Reels")]
    [SerializeField] private SpriteRenderer reel1;
    [SerializeField] private SpriteRenderer reel2;
    [SerializeField] private SpriteRenderer reel3;

    private void Start()
    {
        SlotMachineController.Instance.OnResult += ShowResult;
    }
    private void ShowResult(int slot1, int slot2, int slot3)
    {
        reel1.sprite = symbols[slot1];
        reel2.sprite = symbols[slot2];
        reel3.sprite = symbols[slot3];
    }
}
