using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Boids
{
	public class BoidFactory : MonoBehaviour
	{
		[FormerlySerializedAs("_BoidPrefab")] [SerializeField] private GameObject _boidPrefab; 
		[FormerlySerializedAs("_Target")] [SerializeField] private Transform _target;
		[FormerlySerializedAs("_Arena")] [SerializeField] private ArenaArea _arena;

		public Boid InstantiateAndInitializeBoid()
		{
			GameObject boidGo = Instantiate(_boidPrefab, transform.position, Quaternion.identity);
			Boid boid = boidGo.GetComponent<Boid>();
			
			if (boid == null)
			{
				Debug.LogError("Boid component was not found on a prefab used in BoidFactory");
				return null;
			}
			
			boid.Initialize(_target, _arena);
			return boid;
		}
	}
}
