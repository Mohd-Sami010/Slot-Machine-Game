using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private SpriteRenderer leverUpSprite;
    [SerializeField] private SpriteRenderer leverDownSprite;

    private void Start()
    {
        SlotMachineController.Instance.OnSpin += () =>
        {
            leverUpSprite.enabled = false;
            leverDownSprite.enabled = true;
            Invoke(nameof(ResetLever), 0.5f);
        };
        leverDownSprite.enabled = false;
    }
    private void ResetLever()
    {
        leverUpSprite.enabled = true;
        leverDownSprite.enabled = false;
    }
}
