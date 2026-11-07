
using UnityEngine;

public class Lever : MonoBehaviour
{
    [Header("Lever Sprites")]
    [SerializeField] private SpriteRenderer leverSpriteRederer;
    [SerializeField] private Sprite leverUpSprite;
    [SerializeField] private Sprite leverDownSprite;

    [SerializeField] private Color leverHighlightColor = Color.yellow;

    private bool isBusy;

    private void Start()
    {
        SlotMachineController.Instance.OnSpin += HandleSpinStarted;
        SlotMachineController.Instance.OnSpinComplete += HandleSpinComplete;

        leverSpriteRederer.sprite = leverUpSprite;
    }
    private void OnMouseEnter()
    {
        if (isBusy) return;
        leverSpriteRederer.color = leverHighlightColor;
    }
    private void OnMouseExit()
    {
        if (isBusy) return;
        leverSpriteRederer.color = Color.white;
    }

    private void OnMouseDown()
    {
        if (isBusy) return;

        leverSpriteRederer.color = Color.white;
        SlotMachineController.Instance.Spin();
    }

    private void HandleSpinStarted()
    {
        isBusy = true;

        leverSpriteRederer.sprite = leverDownSprite;

        float leverResetDelay = 0.4f;
        Invoke(nameof(ResetLever), leverResetDelay);
    }

    private void ResetLever()
    {
        leverSpriteRederer.sprite = leverUpSprite;
    }

    private void HandleSpinComplete()
    {
        isBusy = false;
        ResetLever();
    }
}