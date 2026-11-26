using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Player
{
	public class PlayerBodyRotator : MonoBehaviour
	{
		[FormerlySerializedAs("_Aim")] [SerializeField] private PlayerAim _aim;

		private float _zRotationOffsetDeg;
		
		private void Awake()
		{
			_aim.OnAimPositionUpdated += UpdateRotation;
			_zRotationOffsetDeg = transform.eulerAngles.z;
		}

		private void UpdateRotation(Vector2 _)
		{
			transform.eulerAngles = new(0, 0, _aim.GetRotationDegTowardsTarget() + _zRotationOffsetDeg);
		}
	}
}
