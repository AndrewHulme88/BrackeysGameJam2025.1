using Unity.VisualScripting;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] DoorController doorController;
    [SerializeField] GameObject objectToApplyGravity;
    [SerializeField] float gravityToSet = 3f;
    [SerializeField] GameObject objectToDisappear;
    [SerializeField] GameObject objectToAppear;
    [SerializeField] ScreenShake screenShake;
    [SerializeField] AudioClip clickSound;
     
    private bool isPressed = false;
    private bool isPlayerInRange = false;
    private Animator anim;
    private AudioSource audioSource;
    private AudioManager audioManager;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioManager = FindFirstObjectByType<AudioManager>();
    }

    private void Update()
    {

        if(isPlayerInRange && Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) && !isPressed)
        {
            if(!isPressed)
            {
                audioSource.PlayOneShot(clickSound);
                PressButton();
                ApplyGravity();
                SwapObjects();
                ShakeScreen();
            }
        }

        anim.SetBool("up", !isPressed);
    }

    void PressButton()
    {
        isPressed = true;
        doorController?.OpenDoor();
    }

    void ApplyGravity()
    {
        if(objectToApplyGravity != null)
        {
            Rigidbody2D rb = objectToApplyGravity.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                rb.gravityScale = gravityToSet;
                rb.constraints = RigidbodyConstraints2D.None;
            }
        }
    }

    void SwapObjects()
    {
        if(objectToDisappear != null)
        {
            objectToDisappear.SetActive(false);
            objectToAppear.SetActive(true);
        }
    }

    void ShakeScreen()
    {
        if(screenShake != null)
        {
            screenShake.TriggerShake();
        }

        if(audioManager != null)
        {
            audioManager.PlaySound();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
