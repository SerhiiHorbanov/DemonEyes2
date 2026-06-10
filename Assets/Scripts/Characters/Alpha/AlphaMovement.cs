using Characters.Player;
using UnityEngine;

namespace Characters.Alpha
{
	public class AlphaMovement : PlayerMovement
	{
		[SerializeField] private float _Speed;
		
		private void FixedUpdate()
		{
			transform.position += (Vector3)(MoveDirection * (_Speed * Time.fixedDeltaTime));
		}
	}
}
