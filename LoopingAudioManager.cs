using UnityEngine;

public class LoopingAudioManager : MonoBehaviour
{
    [SerializeField] AudioClip audioClip;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
    }

    public void PlaySound()
    {
        if(!audioSource.isPlaying)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }

    public void StopSound()
    {
        audioSource.Stop();
    }
}
