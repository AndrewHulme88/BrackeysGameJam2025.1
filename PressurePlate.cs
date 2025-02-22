using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] DoorController doorController;
    [SerializeField] AudioClip clickSound;

    private Animator anim;
    private AudioSource audioSource;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("up", true);
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" ||  collision.tag == "Box")
        {
            audioSource.PlayOneShot(clickSound);
            anim.SetBool("up", false);
            doorController?.OpenDoor();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Box")
        {
            audioSource.PlayOneShot(clickSound);
            anim.SetBool("up", true);
            doorController?.CloseDoor();
        }
    }
}
