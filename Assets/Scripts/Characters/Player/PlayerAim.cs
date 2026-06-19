using System;
using UnityEngine;

namespace Characters.Player
{
	public class PlayerAim : MonoBehaviour
	{
		private Vector2 _relativeAimPosition;
		public Action<Vector2> OnAimPositionUpdated;
		
		public Vector2 GetGlobalTargetedPosition()
			=> _relativeAimPosition + (Vector2)transform.position;
		
		public float GetRotationDegTowardsTarget()
			=> Mathf.Atan2(_relativeAimPosition.y, _relativeAimPosition.x) * Mathf.Rad2Deg;

		public void SetRelativeAimPoint(Vector2 relativeAimPoint)
		{
			_relativeAimPosition = relativeAimPoint;
			OnAimPositionUpdated?.Invoke(_relativeAimPosition);
		}
	}
}
