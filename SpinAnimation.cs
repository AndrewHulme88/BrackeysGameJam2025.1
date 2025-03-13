using UnityEngine;

public class SpinAnimation : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 50f;

    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
