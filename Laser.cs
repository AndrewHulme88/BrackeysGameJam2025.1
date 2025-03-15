using UnityEngine;

public class Laser : MonoBehaviour
{
    private Transform laserObject;
    private BoxCollider2D laserCollider;

    void Start()
    {
        laserObject = transform.GetChild(0);
        laserCollider = laserObject.GetComponent<BoxCollider2D>();
    }

    public void DisableCollider()
    {
        laserCollider.enabled = false;
    }

    public void EnableCollider()
    {
        laserCollider.enabled = true;
    }
}
