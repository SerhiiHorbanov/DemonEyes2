using Characters.Player;
using UnityEngine;

namespace Characters.Alpha
{
	public class AlphaMovement : PlayerMovement
	{
		[SerializeField] float Speed;
		
		private void FixedUpdate()
		{
			transform.position += (Vector3)(MoveDirection * Time.fixedDeltaTime);
		}
	}
}
