using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Player
{
	public class PlayerProjectileFactory : MonoBehaviour
	{
		[FormerlySerializedAs("_ProjectilePrefab")] [SerializeField] private GameObject _projectilePrefab;
		
		public void SpawnProjectile(float directionDeg)
		{
			Quaternion rotation = Quaternion.Euler(0, 0, directionDeg);
			GameObject projectile = Instantiate(_projectilePrefab, transform.position, rotation);
		}
	}
}
