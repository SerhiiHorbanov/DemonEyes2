using Characters.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Alpha
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class AlphaMovement : PlayerMovement
	{
		[FormerlySerializedAs("_Speed")] [SerializeField] private float _speed;
		private Rigidbody2D _rigidBody;

		private void Awake()
		{
			_rigidBody = GetComponent<Rigidbody2D>();
		}
		
		private void FixedUpdate()
		{
			_rigidBody.linearVelocity = MoveDirection * _speed;
		}
	}
}
