using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Player
{
	public class Crosshair : MonoBehaviour
	{
		[FormerlySerializedAs("_Aim")] [SerializeField] private PlayerAim _aim;

		private void Awake()
		{
			_aim.OnAimPositionUpdated += UpdatePositionFromUpdateAimPosition;
		}

		private void UpdatePositionFromUpdateAimPosition(Vector2 relativeAimPosition)
		{
			transform.localPosition = relativeAimPosition;
		}
	}
}
