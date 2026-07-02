using Tools;
using UnityEngine;

namespace Characters.Boids
{
	[RequireComponent(typeof(BoidFactory))]
	public class BoidSpawner : MonoBehaviour
	{
		[SerializeField] private float _DistanceFromCenter;
		private BoidFactory _boidFactory;
		
		private void Awake()
		{
			_boidFactory = GetComponent<BoidFactory>();
			EventBus<BoidSpawnEvent>.Event += Spawn;
		}

		private void Spawn(BoidSpawnEvent _)
		{
			Boid boid = _boidFactory.InstantiateAndInitializeBoid();
			Transform boidTransform = boid.transform;
			
			float angleRad = Random.Range(0f, Mathf.PI * 2f);
			Vector2 position = new(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
			
			boidTransform.position = position * _DistanceFromCenter;
		}
	}
}
