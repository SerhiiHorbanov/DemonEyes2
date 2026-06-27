using Characters.Boids;
using UnityEngine;

namespace Characters.Towers
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class Tower : MonoBehaviour
	{
		private Vector2 _targetPosition;
		public float _Speed;
		
		[SerializeField] private ArenaArea _Arena;
		
		[SerializeField] private GameObject _BoidPrefab;
		[SerializeField] private Transform _Target;
		
		private Rigidbody2D _rigidBody;

		private void Awake()
		{
			_rigidBody = GetComponent<Rigidbody2D>();
		}
		
		public void SpawnBoid()
		{
			GameObject boidGO = Instantiate(_BoidPrefab, transform.position, Quaternion.identity);
			Boid boid = boidGO.GetComponent<Boid>();
			boid._Target = _Target;
			boid._Arena = _Arena;
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
