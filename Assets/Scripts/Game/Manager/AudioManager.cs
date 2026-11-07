using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource machineSpinAudioSource;
    [SerializeField] private AudioSource leverPullAudioSource;
    [SerializeField] private AudioSource jackpotAudioSource;
    [SerializeField] private AudioSource betSelectAudioSource;
    [SerializeField] private AudioSource gameOverAudioSource;
    

    private void Start()
    {
        SlotMachineController.Instance.OnSpin += PlayMachineSpinSound;
        SlotMachineController.Instance.OnGameOver += () => PlaySound(gameOverAudioSource);
        BetManager.Instance.OnBetChanged += (_) => PlaySound(betSelectAudioSource);
        WalletManager.Instance.OnCoinsAdded += () => PlaySound(jackpotAudioSource, false);
    }
    private void PlayMachineSpinSound()
    {        
        PlaySound(machineSpinAudioSource, false);
        PlaySound(leverPullAudioSource);
    }
    private void PlaySound(AudioSource audioSource, bool randomPitch = true)
    {
        if (audioSource == null) return;

        if (randomPitch)
        {
            float pitch = Random.Range(0.9f, 1.1f);
            audioSource.pitch = pitch;
        }
        audioSource.Play();
    }
}
