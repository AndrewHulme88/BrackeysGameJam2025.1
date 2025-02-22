using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip audioClip;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        audioSource.PlayOneShot(audioClip);
    }

    public void PlaySpecificSound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
