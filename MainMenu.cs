using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] AudioClip clickSound;

    private ScreenTransition screenTransition;
    private AudioSource audioSource;

    private void Start()
    {
        screenTransition = FindFirstObjectByType<ScreenTransition>();
        audioSource = GetComponent<AudioSource>();
    }

    public void StartNewGame()
    {
        if(clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }

        screenTransition.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }

        Application.Quit();
        Debug.Log("Game has been quit");
    }
}
