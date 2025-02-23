using TMPro;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class Dialog : MonoBehaviour
{
    [SerializeField] float typingSpeed = 0.2f;
    [SerializeField] AudioClip clickSound;
    [SerializeField] GameObject dialogBox;
    [SerializeField] bool isSceneChanging = false;
    [SerializeField] string sceneName;
    [SerializeField] bool isEndScene = false;
    [SerializeField] GameObject smasher;

    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private PlayerController playerController;
    private ScreenTransition transition;

    private int index;
    private AudioSource audioSource;
    private bool isTypingComplete = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerController = FindFirstObjectByType<PlayerController>();
        transition = FindFirstObjectByType<ScreenTransition>();
        StartCoroutine(Type());
        StartCoroutine(DelayedFreeze());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && isTypingComplete)
        {
            NextSentence();
        }
    }

    IEnumerator DelayedFreeze()
    {
        yield return new WaitForEndOfFrame(); 
        playerController.FreezePlayer(true);
    }

    IEnumerator Type()
    {
        isTypingComplete = false;
        textDisplay.text = "";

        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTypingComplete = true;
    }

    public void NextSentence()
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }

        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            if(isSceneChanging)
            {
                transition.LoadScene(sceneName);
            }

            textDisplay.text = "";
            dialogBox?.SetActive(false);
            playerController.FreezePlayer(false);

            if (isEndScene)
            {
                smasher.SetActive(true);
            }
        }
    }
}
