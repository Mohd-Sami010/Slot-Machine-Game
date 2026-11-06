
using UnityEngine;

public class Lever : MonoBehaviour
{
    [Header("Lever Sprites")]
    [SerializeField] private SpriteRenderer leverUpSprite;
    [SerializeField] private SpriteRenderer leverDownSprite;

    private bool isBusy;

    private void Start()
    {
        SlotMachineController.Instance.OnSpin += HandleSpinStarted;
        SlotMachineController.Instance.OnSpinComplete += HandleSpinComplete;

        leverUpSprite.enabled = true;
        leverDownSprite.enabled = false;
    }

    private void OnMouseDown()
    {
        if (isBusy) return;

        SlotMachineController.Instance.Spin();
    }

    private void HandleSpinStarted()
    {
        isBusy = true;

        leverUpSprite.enabled = false;
        leverDownSprite.enabled = true;

        float leverResetDelay = 0.4f;
        Invoke(nameof(ResetLever), leverResetDelay);
    }

    private void ResetLever()
    {
        leverUpSprite.enabled = true;
        leverDownSprite.enabled = false;
    }

    private void HandleSpinComplete()
    {
        isBusy = false;
        ResetLever();
    }
}