using UnityEngine;

public class Smasher : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;
    [SerializeField] ScreenShake screenShake;
    [SerializeField] AudioClip thudSound;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ground")
        {
            if(thudSound != null)
            {
                audioSource.PlayOneShot(thudSound);
            }

            if(particles != null)
            {
                particles.Play();
            }

            if(screenShake != null)
            {
                screenShake.TriggerShake();
            }
        }
    }
}
