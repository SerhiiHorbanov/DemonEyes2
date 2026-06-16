using Characters.Player;
using UnityEngine;

namespace Characters.Alpha
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class AlphaMovement : PlayerMovement
	{
		[SerializeField] private float _Speed;
		private Rigidbody2D _rigidBody;

		private void Awake()
		{
			_rigidBody = GetComponent<Rigidbody2D>();
		}
		
		private void FixedUpdate()
		{
			_rigidBody.linearVelocity = MoveDirection * _Speed;
		}
	}
}
