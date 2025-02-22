using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] float delayTime = 2f;

    private ScreenTransition transition;

    void Start()
    {
        transition = FindFirstObjectByType<ScreenTransition>();
        StartCoroutine(StartSplashScreen());
    }

    IEnumerator StartSplashScreen()
    {
        yield return new WaitForSeconds(delayTime);
        transition.LoadScene("MainMenu");
    }
}
