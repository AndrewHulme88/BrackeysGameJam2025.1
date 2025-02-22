using UnityEngine;

public class Credits : MonoBehaviour
{
    private ScreenTransition transition;

    void Start()
    {
        transition = FindFirstObjectByType<ScreenTransition>();
    }

    public void MainMenu()
    {
        transition.LoadScene("MainMenu");
    }
}
