using UnityEngine;

namespace Characters.Player
{
	public class PlayerBodyRotator : MonoBehaviour
	{
		[SerializeField] private PlayerAim _Aim;

		private float _zRotationOffsetDeg;
		
		private void Awake()
		{
			_Aim.OnAimPositionUpdated += UpdateRotation;
			_zRotationOffsetDeg = transform.eulerAngles.z;
		}

		private void UpdateRotation(Vector2 _)
		{
			transform.eulerAngles = new(0, 0, _Aim.GetRotationDegTowardsTarget() + _zRotationOffsetDeg);
		}
	}
}
