using Characters.Boids;
using UnityEngine;

namespace Characters.Towers
{
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(BoidFactory))]
	public class Tower : MonoBehaviour
	{
		private Vector2 _targetPosition;
		public float _Speed;
		
		private Rigidbody2D _rigidBody;
		private BoidFactory _boidFactory;

		private void Awake()
		{
			_rigidBody = GetComponent<Rigidbody2D>();
			_boidFactory = GetComponent<BoidFactory>();
		}
		
		public void SpawnBoid()
		{
			_boidFactory.InstantiateAndInitializeBoid();
		}

		private void FixedUpdate()
		{
			Vector2 deltaPosition = _targetPosition - (Vector2)transform.position;
			Vector2 direction = deltaPosition.normalized;
			
			_rigidBody.linearVelocity = direction * _Speed;
		}

		public void MoveTowards(Vector2 target)
		{
			_targetPosition = target;
		}
	}
}
