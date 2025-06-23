using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    private Transform cameraTransform;
    [SerializeField] private Transform player;

    [SerializeField] private Vector3 offset;

    private void Awake()
    {
        cameraTransform = transform;
    }

    private void LateUpdate()
    {
        cameraTransform.position = player.transform.position + offset;
    }


}
