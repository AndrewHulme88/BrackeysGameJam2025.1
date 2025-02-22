using UnityEngine;

public class SlidingPlatform : MonoBehaviour
{
    [SerializeField] AudioClip slide1Sound;
    [SerializeField] AudioClip slide2Sound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();    
    }

    public void PlaySound1()
    {
        audioSource?.PlayOneShot(slide1Sound);
    }

    public void PlaySound2()
    {
        audioSource?.PlayOneShot(slide2Sound);
    }
}
