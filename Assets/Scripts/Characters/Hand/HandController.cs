using Unity.Mathematics.Geometry;
using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Hand
{
	[RequireComponent(typeof(BoxCollider2D))]
	class HandController : MonoBehaviour
	{
		private BoxCollider2D _boxTrigger;	
		
		[FormerlySerializedAs("_Target")] [SerializeField] private Transform _target;
		[FormerlySerializedAs("_MinTargetDistanceToCenterForAggro")] [SerializeField] private float _minTargetDistanceToCenterForAggro;
		
		[FormerlySerializedAs("_RestDistance")] [SerializeField] private float _restDistance;
		[FormerlySerializedAs("_Hand")] [SerializeField] private Hand _hand;
		private bool _isTargetInTrigger;

		private void Awake()
		{
			_boxTrigger = GetComponent<BoxCollider2D>();
		}

		private void ApplyLayoutBox(MinMaxAABB box)
		{
			Vector2 offset = (Vector3)box.Center - transform.position;
			_boxTrigger.offset = offset;
			_boxTrigger.size = new Vector2(box.Extents.x, box.Extents.y);
		}
		
		private void Update()
		{
			bool isTargetFarEnoughFromCenter = _target.position.magnitude > _minTargetDistanceToCenterForAggro;
			if (_isTargetInTrigger && isTargetFarEnoughFromCenter)
			{
				_hand.MoveTo(_target.position);
			}
			else
			{
				Vector2 restPosition = _hand.transform.position.normalized * _restDistance;
				_hand.MoveSlowlyTo(restPosition);
			}
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			bool isTarget = other.gameObject == _target.gameObject;
			if (isTarget)
				_isTargetInTrigger = true;
		}
		
		private void OnTriggerExit2D(Collider2D other)
		{
			bool isTarget = other.gameObject == _target.gameObject;
			if (isTarget)
				_isTargetInTrigger = false;
		}
	}
}
