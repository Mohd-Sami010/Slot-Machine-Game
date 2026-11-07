using UnityEngine;

public class MainMenuAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource buttonClickAudioSource;
    [SerializeField] private AudioSource leverPullAudioSource;

    public void PlayButtonClickSound()
    {
        PlaySound(buttonClickAudioSource);
    }
    public void PlayLeverPullSound()
    {
        PlaySound(leverPullAudioSource, false);
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
