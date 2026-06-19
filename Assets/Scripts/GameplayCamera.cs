using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _Target;
    [SerializeField] private Vector2 _TargetedPositionMultiplier;
    private Vector2 _targetPosition;
    
    [Range(0, 1)] [SerializeField] private float _DistanceMultiplierOverSecond;

    private void LateUpdate()
    {
        Vector2 targetPosition = _Target.position * _TargetedPositionMultiplier;
        
        float t = 1 - Mathf.Pow(_DistanceMultiplierOverSecond, Time.deltaTime);
        Vector2 newPosition = Vector2.Lerp(transform.position, targetPosition, t);
        
        transform.position = new(newPosition.x, newPosition.y, transform.position.z);
    }
}