using UnityEngine;
using System.Collections;

public class ReelController : MonoBehaviour
{    
    [Header("Symbols Strip")]
    [SerializeField] private GameObject highlightedSymbolBackground;
    [SerializeField] private SpriteRenderer topSymbol;
    [SerializeField] private SpriteRenderer middleSymbol;
    [SerializeField] private SpriteRenderer bottomSymbol;

    [Header("Background Color")]
    [SerializeField] private Color defaultBackgroundColor = Color.white;
    [SerializeField] private Color highlightBackgroundColor = Color.yellow;

    [Header("Animation Settings")]
    [SerializeField] private float stepDuration = 0.08f;
    [SerializeField] private float finalStepDuration = 0.32f;
    [SerializeField] private int decelerationSteps = 8;

    public bool IsSpinning { get; private set; } = false;

    private Sprite[] availableSymbolSprites;

    private Vector3 topPosition;
    private Vector3 middlePosition;
    private Vector3 bottomPosition;

    private float symbolSpacing;

    private void Awake()
    {
        topPosition = topSymbol.transform.localPosition;
        middlePosition = middleSymbol.transform.localPosition;
        bottomPosition = bottomSymbol.transform.localPosition;

        symbolSpacing = topPosition.y - middlePosition.y;
    }
    private void Start()
    {
        WalletManager.Instance.OnCoinsAdded += () =>
        {
            highlightedSymbolBackground.GetComponent<SpriteRenderer>().color = highlightBackgroundColor;
        };
    }
    public void PlaySpin(Sprite[] sysmbols, Sprite finalSymbol, float fastSpinDuration)
    {
        if (IsSpinning) return;

        highlightedSymbolBackground.GetComponent<SpriteRenderer>().color = defaultBackgroundColor;
        highlightedSymbolBackground.SetActive(false);
        IsSpinning = true;
        availableSymbolSprites = sysmbols;

        StartCoroutine(SpinRoutine(finalSymbol, fastSpinDuration));
    }
    private IEnumerator SpinRoutine(Sprite finalResult, float fastSpinDuration)
    {
        IsSpinning = true;

        // Calculate how many fast spinning steps to perform.
        int fastSteps = Mathf.CeilToInt(
            fastSpinDuration / stepDuration
        );

        // Phase 1: Fast spinning.
        for (int i = 0; i < fastSteps; i++)
        {
            yield return AnimateStep(stepDuration);
        }

        // Phase 2: Gradually slow down.
        for (int i = 0; i < decelerationSteps; i++)
        {
            float t = (float)i / (decelerationSteps - 1);

            float duration = Mathf.Lerp(stepDuration, finalStepDuration, t);

            // The final symbol must land in the middle.
            if (i == decelerationSteps - 1) topSymbol.sprite = finalResult;
            yield return AnimateStep(duration);
        }

        IsSpinning = false;
        SlotMachineController.Instance.OnReelStopped();
        highlightedSymbolBackground.SetActive(true);
        highlightedSymbolBackground.GetComponent<SpriteRenderer>().color = defaultBackgroundColor;
    }
    private IEnumerator AnimateStep(float duration)
    {
        SpriteRenderer oldTop = topSymbol;
        SpriteRenderer oldMiddle = middleSymbol;
        SpriteRenderer oldBottom = bottomSymbol;

        Vector3 topStart = oldTop.transform.localPosition;
        Vector3 middleStart = oldMiddle.transform.localPosition;
        Vector3 bottomStart = oldBottom.transform.localPosition;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / duration);

            // Smooth movement between symbol positions.
            oldTop.transform.localPosition =
                Vector3.Lerp(topStart, topStart - Vector3.up * symbolSpacing, t);

            oldMiddle.transform.localPosition =
                Vector3.Lerp(middleStart, middleStart - Vector3.up * symbolSpacing, t);

            oldBottom.transform.localPosition =
                Vector3.Lerp(bottomStart, bottomStart - Vector3.up * symbolSpacing, t);

            yield return null;
        }

        // Reposition the symbols after one complete step.
        oldMiddle.transform.localPosition = bottomPosition;
        oldBottom.transform.localPosition = topPosition;
        oldTop.transform.localPosition = middlePosition;

        // Rotate the references.
        topSymbol = oldBottom;
        middleSymbol = oldTop;
        bottomSymbol = oldMiddle;

        // Give the recycled bottom symbol a new random sprite.
        topSymbol.sprite = availableSymbolSprites[
            Random.Range(0, availableSymbolSprites.Length)
        ];
    }
}
