using UnityEngine;

namespace Characters.Player
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class PlayerProjectile : MonoBehaviour
	{
		[SerializeField] private float _Speed;
		private Rigidbody2D _rigidBody;
		
		private void Awake()
		{
			_rigidBody = GetComponent<Rigidbody2D>();
			_rigidBody.linearVelocity = transform.right * _Speed;
		}
	}
}
