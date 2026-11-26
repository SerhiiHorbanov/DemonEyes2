using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Towers
{
	[RequireComponent(typeof(Tower))]
	public class TowerController : MonoBehaviour
	{
		private Vector2 _targetPosition;
		
		private int _sidesAmount;
		private float _ngonInnerAngleDeg;
		private float _currentReachedAngleDeg;
		
		private const int MinSidesAmount = 4;
		[FormerlySerializedAs("_MaxSidesAmountExclusive")] [SerializeField] private int _maxSidesAmountExclusive;
		[FormerlySerializedAs("_NgonRadius")] [SerializeField] private float _ngonRadius;
		[FormerlySerializedAs("_TimePerSide")] [SerializeField] private float _timePerSide;
		
		[FormerlySerializedAs("_Arena")] [SerializeField] private ArenaArea _arena;
		private Tower _tower;

		private void Awake()
		{
			_tower = GetComponent<Tower>();
		}
		
		private void Start()
		{
			if (_maxSidesAmountExclusive <= MinSidesAmount)
			{
				_sidesAmount = MinSidesAmount;
				Debug.LogWarning("Max sides amount is less than min sides amount. Setting max sides amount to min sides amount.");
			}
			else
			{
				_sidesAmount = Random.Range(MinSidesAmount, _maxSidesAmountExclusive);
			}
			
			_ngonInnerAngleDeg = 360f / _sidesAmount;
			_currentReachedAngleDeg = _ngonInnerAngleDeg * -0.5f;
			float sideLength = _ngonRadius * Mathf.Tan(_ngonInnerAngleDeg * Mathf.Deg2Rad * 0.5f) * 2;
			_tower.Speed = sideLength / _timePerSide;

			float targetAngleRad = (_currentReachedAngleDeg + _ngonInnerAngleDeg) * Mathf.Deg2Rad;
			_targetPosition = _arena.MapFromUnitCircleToWorld(new(Mathf.Cos(targetAngleRad), Mathf.Sin(targetAngleRad)));
			_tower.MoveTowards(_targetPosition);
		}

		private void FixedUpdate()
		{
			/*Vector2 positionOnArena = transform.position - _Arena.transform.position;
			float angleOnArena = Vector2.Angle(positionOnArena, Vector2.right);
			float movedAngleForSideOfNgon = Mathf.Abs(Mathf.DeltaAngle(_currentReachedAngleDeg, angleOnArena));

			if (movedAngleForSideOfNgon < _ngonInnerAngleDeg - 0.1f)
				return;
			
			_currentReachedAngleDeg += _ngonInnerAngleDeg;*/
			
			Vector2 relativeTargetPosition = _targetPosition - (Vector2)transform.position;
			if (relativeTargetPosition.sqrMagnitude > 0.5f)
				return;

			_currentReachedAngleDeg += _ngonInnerAngleDeg;
			float targetAngleRad = (_currentReachedAngleDeg + _ngonInnerAngleDeg) * Mathf.Deg2Rad;
			_targetPosition = new Vector2(Mathf.Cos(targetAngleRad), Mathf.Sin(targetAngleRad)) * _ngonRadius;
			_tower.MoveTowards(_targetPosition);
			_tower.SpawnBoid();
		}
	}
}
