using UnityEngine;

public class rotateObject : MonoBehaviour
{
    [SerializeField] private float speed = 90f;
    [SerializeField] private Vector3 rotationAxis = Vector3.up;

    void Update()
    {
        transform.Rotate(rotationAxis, speed * Time.deltaTime);
    }
}
