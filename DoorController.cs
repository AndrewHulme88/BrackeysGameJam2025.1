using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] AudioClip openSound;
    [SerializeField] AudioClip closeSound;

    private Animator anim;
    private AudioSource audioSource;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void OpenDoor()
    {
        audioSource.PlayOneShot(openSound);
        anim.SetTrigger("open");
    }

    public void CloseDoor()
    {
        audioSource.PlayOneShot(closeSound);
        anim.SetTrigger("close");
    }
}
