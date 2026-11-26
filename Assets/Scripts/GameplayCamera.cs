using UnityEngine;
using UnityEngine.Serialization;

public class CameraMovement : MonoBehaviour
{
    [FormerlySerializedAs("_Target")] [SerializeField] private Transform _target;
    [FormerlySerializedAs("_TargetedPositionMultiplier")] [SerializeField] private Vector2 _targetedPositionMultiplier;
    private Vector2 _targetPosition;
    
    [FormerlySerializedAs("_DistanceMultiplierOverSecond")] [Range(0, 1)] [SerializeField] private float _distanceMultiplierOverSecond;

    private void LateUpdate()
    {
        Vector2 targetPosition = _target.position * _targetedPositionMultiplier;
        
        float t = 1 - Mathf.Pow(_distanceMultiplierOverSecond, Time.deltaTime);
        Vector2 newPosition = Vector2.Lerp(transform.position, targetPosition, t);
        
        transform.position = new(newPosition.x, newPosition.y, transform.position.z);
    }
}