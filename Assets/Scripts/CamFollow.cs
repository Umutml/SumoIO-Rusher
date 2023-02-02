using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float lerpValue;


    private void LateUpdate()
    {
        var targetPos = target.position;
        transform.position = Vector3.Lerp(transform.position, targetPos + offset, lerpValue * Time.deltaTime);
    }
}