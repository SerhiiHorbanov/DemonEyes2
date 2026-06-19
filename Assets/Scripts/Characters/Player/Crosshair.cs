using UnityEngine;

namespace Characters.Player
{
	public class Crosshair : MonoBehaviour
	{
		[SerializeField] private PlayerAim _Aim;

		private void Awake()
		{
			_Aim.OnAimPositionUpdated += UpdatePositionFromUpdateAimPosition;
		}

		private void UpdatePositionFromUpdateAimPosition(Vector2 relativeAimPosition)
		{
			transform.localPosition = relativeAimPosition;
		}
	}
}
