using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] string nextSceneName;

    private ScreenTransition screenTransition;

    private void Start()
    {
        screenTransition = FindFirstObjectByType<ScreenTransition>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            screenTransition.LoadScene(nextSceneName);
        }
    }
}
