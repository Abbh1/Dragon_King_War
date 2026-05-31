using UnityEngine;

public class MusicController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] clips;
    public static MusicController instance;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        instance = this;
    }
    public void PlayFightMusic()
    {
        if (audioSource.clip != clips[2])
        {
            audioSource.clip = clips[2];
            audioSource.Play();
        }
    }
    public void PlayIntroMusic()
    {
        if (audioSource.clip != clips[1])
        {
            audioSource.clip = clips[1];
            audioSource.Play();
        }
    }
    public void PlayNormalMusic()
    {
        if (audioSource.clip != clips[0])
        {
            audioSource.clip = clips[0];
            audioSource.Play();
        }
    }
    public void SetVolume(float value)
    {
        audioSource.volume = value;
    }
}
