using UnityEngine;

namespace Characters.Boids
{
	public class BoidFactory : MonoBehaviour
	{
		[SerializeField] private GameObject _BoidPrefab; 
		[SerializeField] private Transform _Target;
		[SerializeField] private ArenaArea _Arena;

		public Boid InstantiateAndInitializeBoid()
		{
			GameObject boidGO = Instantiate(_BoidPrefab, transform.position, Quaternion.identity);
			Boid boid = boidGO.GetComponent<Boid>();
			
			if (boid == null)
			{
				Debug.LogError("Boid component was not found on a prefab used in BoidFactory");
				return null;
			}
			
			boid.Initialize(_Target, _Arena);
			return boid;
		}
	}
}
