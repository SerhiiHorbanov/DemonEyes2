using System;
using UnityEngine;

namespace Characters.Hand
{
	public class HandController : MonoBehaviour
	{
		[SerializeField] private Transform _Target;
		[SerializeField] private float _MinTargetDistanceToCenterForAggro;
		
		[SerializeField] private float _RestDistance;
		[SerializeField] private Hand _Hand;
		private bool _isTargetInTrigger;
		
		private void Update()
		{
			bool isTargetFarEnoughFromCenter = _Target.position.magnitude > _MinTargetDistanceToCenterForAggro;
			if (_isTargetInTrigger && isTargetFarEnoughFromCenter)
			{
				_Hand.MoveTo(_Target.position);
			}
			else
			{
				Vector2 restPosition = _Hand.transform.position.normalized * _RestDistance;
				_Hand.MoveSlowlyTo(restPosition);
			}
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			bool isTarget = other.gameObject == _Target.gameObject;
			if (isTarget)
				_isTargetInTrigger = true;
		}
		
		private void OnTriggerExit2D(Collider2D other)
		{
			bool isTarget = other.gameObject == _Target.gameObject;
			if (isTarget)
				_isTargetInTrigger = false;
		}
	}
}
